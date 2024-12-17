using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemsUseControl : MonoBehaviour
{
    private List<delegateManager.VoidFunc> listItemsActive;

    void Start()
    {
        renderItemsUse();
        listItemsActive = new List<delegateManager.VoidFunc> {
            () =>
            {
                Debug.Log("Use Items! 0");
                PlayerControl.instance._heart.Recover(10);
            },
            () =>
            {
                Debug.Log("Use Items! 1");
                PlayerControl.instance._heart.Recover(0.5f);
            },
            () =>
            {
                Debug.Log("Use Items! 2");
                PlayerControl.instance._heart.upShiel(5);
            },
            () =>
            {
                Debug.Log("Use Items! 3");
            },
            () =>
            {
                Debug.Log("Use Items! 4");
            },
            () =>
            {
                Debug.Log("Use Items! 5");
            },
        };
    }

    void Update()
    {
        if(userPointManager.UserPoint.itemBackpack.Count != transform.childCount)
        {
            this.renderItemsUse();
        }
    }

    void renderItemsUse()
    {
        for(int i = 0; i < 3; i++)
        {
            if(i < userPointManager.UserPoint.itemBackpack.Count)
            {
                Transform temp = transform.GetChild(i);
                temp.gameObject.SetActive(true);
                temp.Find("icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("IconItems/" + userPointManager.shopInfor[userPointManager.UserPoint.itemBackpack[i]].name);
                temp.GetComponent<Button>().onClick.RemoveAllListeners();
                temp.GetComponent<Button>().onClick.AddListener(() =>
                {
                    int g = temp.GetSiblingIndex();
                    listItemsActive[userPointManager.UserPoint.itemBackpack[g]]();
                    userPointManager.UserPoint.itemBackpack.RemoveAt(g);
                    renderItemsUse();
                });
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
