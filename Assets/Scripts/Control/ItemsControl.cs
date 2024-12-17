using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemsControl : MonoBehaviour
{
    [SerializeField] private GameObject itemsPrefab;
    [SerializeField] private Transform contentShop, contentInvent, contentBP;

    [SerializeField] private Button _shopBtn, _inventBtn;
    [SerializeField] private TextMeshProUGUI _showCointinShop;

    void Start()
    {
        renderUIShop();
        showInvent();
        if (_shopBtn != null)
        {
            _shopBtn.onClick.AddListener(() =>
            {
                renderUIShop();
            });
        }
        if (_inventBtn != null)
        {
            _inventBtn.onClick.AddListener(() =>
            {
                renderInvent();
                renderBackPack();
            });
        }
        if(_showCointinShop != null) _showCointinShop.text = userPointManager.UserPoint.coint.ToString();
    }

    void Update()
    {
        if (contentShop != null)
        {
            for (int i = 0; i < contentShop.childCount; i++)
            {
                if (contentShop.GetChild(i).localScale.x != 1)
                {
                    contentShop.GetChild(i).localScale = Vector3.one;
                }
            }
        }

        if (contentInvent != null)
        {
            for (int i = 0; i < contentInvent.childCount; i++)
            {
                if (contentInvent.GetChild(i).localScale.x != 1)
                {
                    contentInvent.GetChild(i).localScale = Vector3.one;
                }
            }
        }


        if (_showCointinShop != null) _showCointinShop.text = userPointManager.UserPoint.coint.ToString();
    }

    private void renderUIShop(int indexBought = -1)
    {
        if (contentShop != null)
        {
            for (int i = 0; i < contentShop.childCount; i++)
            {
                Destroy(contentShop.GetChild(i).gameObject);
            }
            foreach (items i in userPointManager.shopInfor)
            {
                Transform v = Instantiate(itemsPrefab).transform;
                v.SetParent(contentShop);
                v.GetChild(0).GetComponent<TextMeshProUGUI>().text = i.title;
                v.GetChild(1).GetComponent<TextMeshProUGUI>().text = i.des;
                try
                {
                    v.GetChild(2).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("IconItems/" + i.name);
                }
                catch { }
                v.GetChild(3).gameObject.SetActive(false);//.GetComponent<TextMeshProUGUI>().text = i.expense.ToString();
                v.GetChild(4).GetComponentInChildren<TextMeshProUGUI>().text = i.expense.ToString();
                v.GetChild(4).GetChild(1).gameObject.SetActive(userPointManager.UserPoint.coint < i.expense);
                v.GetChild(4).GetComponent<Button>().onClick.RemoveAllListeners();
                v.GetChild(4).GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (userPointManager.UserPoint.coint >= i.expense)
                    {
                        userPointManager.UserPoint.itemInvent.Add(userPointManager.shopInfor.IndexOf(i));
                        userPointManager.UserPoint.coint -= i.expense;

                        renderUIShop(userPointManager.shopInfor.IndexOf(i));
                    }
                });
                if (indexBought != -1 && userPointManager.shopInfor.IndexOf(i) == indexBought)
                {
                    v.GetChild(5).GetComponent<Animator>().SetTrigger("show");
                }
            }
        }
    }

    private void renderInvent()
    {
        if (contentInvent != null)
        {
            for (int i = 0; i < contentInvent.childCount; i++)
            {
                Destroy(contentInvent.GetChild(i).gameObject);
            }
            foreach (int i in userPointManager.UserPoint.itemInvent)
            {
                Transform v = Instantiate(itemsPrefab).transform;
                v.parent = contentInvent;
                v.GetChild(0).GetComponent<TextMeshProUGUI>().text = userPointManager.shopInfor[i].title;
                v.GetChild(1).GetComponent<TextMeshProUGUI>().text = userPointManager.shopInfor[i].des;
                try
                {
                    v.GetChild(2).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("IconItems/" + userPointManager.shopInfor[i].name);
                }
                catch { }
                v.GetChild(3).GetComponent<TextMeshProUGUI>().text = "";
                v.GetChild(3).gameObject.SetActive(false);
                v.GetChild(4).GetComponentInChildren<TextMeshProUGUI>().text = "Add";
                v.GetChild(4).GetComponentInChildren<Button>().onClick.RemoveAllListeners();
                v.GetChild(4).GetComponentInChildren<Button>().onClick.AddListener(() =>
                {
                    if (userPointManager.UserPoint.itemBackpack.Count < 3)
                    {
                        userPointManager.UserPoint.itemBackpack.Add(i);
                        userPointManager.UserPoint.itemInvent.Remove(i);

                        renderInvent();
                        renderBackPack();
                    }
                });
            }
        }
    }

    private void renderBackPack()
    {
        if (contentBP != null)
        {
            for (int i = 0; i < contentBP.childCount; i++)
            {
                contentBP.GetChild(i).GetChild(1).gameObject.SetActive(true);
            }

            for (int i = 0; i < 3; i++)
            {
                Transform temp = contentBP.GetChild(i);
                if (i < userPointManager.UserPoint.itemBackpack.Count)
                {
                    int item = userPointManager.UserPoint.itemBackpack[i];
                    temp.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("IconItems/" + userPointManager.shopInfor[item].name);
                    temp.GetChild(1).gameObject.SetActive(false);

                    temp.GetComponent<Button>().interactable = true;
                    temp.GetComponent<Button>().onClick.RemoveAllListeners();
                    temp.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        int iDB = temp.GetSiblingIndex();
                        int dBP = userPointManager.UserPoint.itemBackpack[iDB];
                        userPointManager.UserPoint.itemBackpack.Remove(dBP);
                        userPointManager.UserPoint.itemInvent.Add(dBP);

                        renderInvent();
                        renderBackPack();
                    });
                }
                else
                {
                    temp.GetComponent<Button>().onClick.RemoveAllListeners();
                    temp.GetComponent<Button>().interactable = false;
                }
            }
        }
    }

    public void showInvent()
    {
        this.renderInvent();
        this.renderBackPack();
    }
}
