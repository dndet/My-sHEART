using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class skillsInPlayManager1 : MonoBehaviour
{
    [SerializeField] private Transform skillPassive, skillActivation, subTowerLevel;
    [SerializeField] private TextMeshProUGUI _showCoint;
    void Start()
    {
        if (_showCoint != null) _showCoint.text = userPointManager.UserPoint.coint.ToString();
        renderPassive();
        renderActivation();
        renderSubTowerLevel();
    }

    private void OnEnable()
    {
        if (_showCoint != null) _showCoint.text = userPointManager.UserPoint.coint.ToString();
        renderPassive();
        renderActivation();
        renderSubTowerLevel();
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
        if (_showCoint != null) _showCoint.text = userPointManager.UserPoint.coint.ToString();
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
                            renderSubTowerLevel();
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
                            renderSubTowerLevel();
                        }
                    });
                }
            }
        }
        if (_showCoint != null) _showCoint.text = userPointManager.UserPoint.coint.ToString();
    }

    void renderSubTowerLevel()
    {
        List<uint> listSubTower = userPointManager.UserPoint.towerLV[PlayerControl.instance.currentTower].levelSubTower;
        for(int i = 0; i < 3; i++)
        {
            Transform sub = subTowerLevel.GetChild(i);
            if(i<listSubTower.Count && subTowerLevel != null)
            {
                sub.gameObject.SetActive(true);
                sub.Find("level").GetComponent<TextMeshProUGUI>().text = "[ + " + listSubTower[i] + " ]";
                sub.Find("expen").GetComponent<TextMeshProUGUI>().text = (listSubTower[i] * 15 + 52).ToString();
                sub.Find("timeDelay").GetComponent<TextMeshProUGUI>().text = (6.5f - listSubTower[i] * 0.025f) + "";
                sub.Find("power").GetComponent<TextMeshProUGUI>().text = ((listSubTower[i] == 0 ? 0 : 1) + (int) (listSubTower[i] / 4)).ToString();

                Button upgradeBtn = sub.Find("upgradeBtn").GetComponent<Button>();
                upgradeBtn.interactable = userPointManager.UserPoint.coint >= (int) (listSubTower[i] * 15 + 52);
                sub.Find("upgradeBtn").GetChild(1).gameObject.SetActive(userPointManager.UserPoint.coint < (int) (listSubTower[i] * 15 + 52));
                upgradeBtn.onClick.RemoveAllListeners();
                upgradeBtn.onClick.AddListener(() =>
                {
                    int getIndex = sub.GetSiblingIndex();
                    if (userPointManager.UserPoint.coint >= (int)(listSubTower[getIndex] * 15 + 52) && userPointManager.UserPoint.towerLV[PlayerControl.instance.currentTower].levelSubTower[getIndex] <= 200)
                    {
                        userPointManager.UserPoint.coint -= (int) (listSubTower[getIndex] * 15 + 52);
                        userPointManager.UserPoint.towerLV[PlayerControl.instance.currentTower].levelSubTower[getIndex]++;
                        renderActivation();
                        renderPassive();
                        renderSubTowerLevel();
                    }
                });
            }
            else
            {
                sub.gameObject.SetActive(false);
            }
        }
    }
}
