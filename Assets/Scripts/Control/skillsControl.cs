using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class skillsControl : MonoBehaviour
{
    class SkillItem
    {
        public string name;
        public delegateManager.VoidFunc skill;
        public float timeDelay;

        public SkillItem(string name, delegateManager.VoidFunc skill, float timeDelay)
        {
            this.name = name;
            this.skill = skill;
            this.timeDelay = timeDelay;
        }
    }

    private List<SkillItem> listSkill;
    [SerializeField] private int skillIndex = 0, timeSkill = 10;

    private float _timeCount;
    private Button _btn;


    void FireBallSkill()
    {
        if (PlayerControl.instance.fireBallSkill != null) PlayerControl.instance.fireBallSkill.SetActive(true);
    }

    void OnShield()
    {
        
        if (PlayerControl.instance._heart != null) PlayerControl.instance._heart.setImmortality();
    }

    async void PauseEnermy()
    {
        PlayerControl.instance.powerBoom.SetActive(true);
        skeletonsScript[] scriptEnermy = FindObjectsByType<skeletonsScript>(FindObjectsSortMode.None);
        foreach (skeletonsScript script in scriptEnermy)
        {
            script.beStop(userPointManager.UserPoint.skillLevel[2] * 0.5f + 1, userPointManager.UserPoint.skillLevel[2]);
        }
        await Task.Delay((int)(1000));// * (userPointManager.UserPoint.skillLevel[2] * 0.5f + 1)));
        PlayerControl.instance.powerBoom.SetActive(false);
    }

    void Start()
    {
        _btn = GetComponent<Button>();
        listSkill = new List<SkillItem>{
            new SkillItem( "fireBal",  FireBallSkill, Mathf.Clamp(timeSkill - userPointManager.UserPoint.skillLevel[this.skillIndex] * 0.5f, 0, timeSkill) ),
            new SkillItem( "shield",  OnShield, Mathf.Clamp(timeSkill - userPointManager.UserPoint.skillLevel[this.skillIndex] * 0.5f, 0, timeSkill) ),
            new SkillItem( "pushedBack",  PauseEnermy, Mathf.Clamp(timeSkill - userPointManager.UserPoint.skillLevel[this.skillIndex] * 0.5f, 0, timeSkill) )
        };

        if (userPointManager.UserPoint.skillLevel[skillIndex] <= 0)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
        _timeCount = 0;
        _btn.onClick.RemoveAllListeners();
        _btn.onClick.AddListener(() =>
        {
            if (userPointManager.UserPoint.skillLevel[skillIndex] > 0)
            {
                _timeCount = listSkill[skillIndex].timeDelay;
                listSkill[skillIndex].skill();
            }
        });
    }

    void Update()
    {
        if(_timeCount > 0)
        {
            _timeCount -= Time.deltaTime;
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(1).GetComponent<Image>().fillAmount = _timeCount / listSkill[skillIndex].timeDelay;
            _btn.interactable = false;
        }
        else
        {
            _btn.interactable = true;
        }
    }
}
