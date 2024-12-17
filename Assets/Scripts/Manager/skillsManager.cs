using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class skillsManager : MonoBehaviour
{
    [SerializeField] private Transform skillPassive, skillActivation, showSkillInfor;
    void Start()
    {
        renderPassive();
        renderActivation();
    }

    private void OnEnable()
    {
        renderPassive();
        renderActivation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void renderPassive()
    {
        if(skillPassive != null)
        {
            skillPassive.GetChild(0).Find("level").GetComponent<TextMeshProUGUI>().text = "[ + " + userPointManager.UserPoint.fireBallLV + " ]";
            skillPassive.GetChild(0).Find("expen").GetComponent<TextMeshProUGUI>().text = (userPointManager.UserPoint.fireBallLV * 10 + 10).ToString();
            skillPassive.GetChild(0).Find("upgradeBtn").GetComponent<Button>().onClick.RemoveAllListeners();
            skillPassive.GetChild(0).Find("upgradeBtn").GetComponent<Button>().onClick.AddListener(() =>
            {
                if(userPointManager.UserPoint.coint >= (userPointManager.UserPoint.fireBallLV * 10 + 10))
                {
                    userPointManager.UserPoint.coint -= (userPointManager.UserPoint.fireBallLV * 10 + 10);
                    userPointManager.UserPoint.fireBallLV++;
                    renderPassive();
                    renderActivation();
                }
            });
            skillPassive.GetChild(0).Find("upgradeBtn").GetChild(1).gameObject.SetActive(userPointManager.UserPoint.coint < (userPointManager.UserPoint.fireBallLV * 10 + 10));
            skillPassive.GetChild(0).Find("upgradeBtn").GetComponent<Button>().interactable = userPointManager.UserPoint.coint >= (userPointManager.UserPoint.fireBallLV * 10 + 10);

            ///
            skillPassive.GetChild(1).Find("level").GetComponent<TextMeshProUGUI>().text = "[ + " + userPointManager.UserPoint.shieldLV + " ]";
            skillPassive.GetChild(1).Find("expen").GetComponent<TextMeshProUGUI>().text = (userPointManager.UserPoint.shieldLV * 10 + 12).ToString();
            skillPassive.GetChild(1).Find("upgradeBtn").GetComponent<Button>().onClick.RemoveAllListeners();
            skillPassive.GetChild(1).Find("upgradeBtn").GetComponent<Button>().onClick.AddListener(() =>
            {
                if (userPointManager.UserPoint.coint >= (userPointManager.UserPoint.shieldLV * 10 + 12))
                {
                    userPointManager.UserPoint.coint -= (userPointManager.UserPoint.shieldLV * 10 + 12);
                    userPointManager.UserPoint.shieldLV++;
                    renderPassive();
                    renderActivation();
                }
            });
            skillPassive.GetChild(1).Find("upgradeBtn").GetChild(1).gameObject.SetActive(userPointManager.UserPoint.coint < (userPointManager.UserPoint.shieldLV * 10 + 12));
            skillPassive.GetChild(1).Find("upgradeBtn").GetComponent<Button>().interactable = userPointManager.UserPoint.coint >= (userPointManager.UserPoint.shieldLV * 10 + 12);
        }
    }

    void renderActivation()
    {
        if(skillActivation != null)
        {
            for(int i = 0; i < 3; i++)
            {
                Transform sub = skillActivation.GetChild(i);
                int lvSkillTemp = userPointManager.UserPoint.skillLevel[i];
                if(lvSkillTemp <= 0)
                {
                    Transform firstUD = sub.Find("firstUpgrade");
                    firstUD.gameObject.SetActive(true);
                    firstUD.GetComponentInChildren<TextMeshProUGUI>().text = (100 + i * 20).ToString();
                    Button tempBtn = firstUD.GetComponent<Button>();
                    tempBtn.interactable = userPointManager.UserPoint.coint >= (100 + i * 20);
                    tempBtn.onClick.RemoveAllListeners();
                    tempBtn.onClick.AddListener(() =>
                    {
                        if (userPointManager.UserPoint.coint >= 100 + sub.GetSiblingIndex() * 20)
                        {
                            Debug.Log(userPointManager.UserPoint.skillLevel.ToString());
                            userPointManager.UserPoint.coint -= 100 + sub.GetSiblingIndex() * 20;
                            userPointManager.UserPoint.skillLevel[sub.GetSiblingIndex()]++;
                            renderActivation();
                            renderPassive();
                        }
                    });
                }
                else
                {
                    sub.Find("firstUpgrade").gameObject.SetActive(false);
                    sub.Find("level").GetComponent<TextMeshProUGUI>().text = "[ + " + lvSkillTemp + " ]";
                    sub.Find("expen").GetComponent<TextMeshProUGUI>().text = (lvSkillTemp * 10 + 20).ToString();
                    Button upgradeBtn = sub.Find("upgradeBtn").GetComponent<Button>();
                    upgradeBtn.interactable = userPointManager.UserPoint.coint >= (lvSkillTemp * 10 + 20);
                    sub.Find("upgradeBtn").GetChild(1).gameObject.SetActive(userPointManager.UserPoint.coint < (lvSkillTemp * 10 + 20));
                    upgradeBtn.onClick.RemoveAllListeners();
                    upgradeBtn.onClick.AddListener(() =>
                    {
                        if (userPointManager.UserPoint.coint >= (lvSkillTemp * 10 + 20))
                        {
                            userPointManager.UserPoint.coint -= (lvSkillTemp * 10 + 20);
                            userPointManager.UserPoint.skillLevel[sub.GetSiblingIndex()]++;
                            renderActivation();
                            renderPassive();
                        }
                    });
                }
            }
        }
    }

    public void ShowSkill(int n)
    {
        if(showSkillInfor != null)
        {
            showSkillInfor.gameObject.SetActive(true);
        }
    }
}
