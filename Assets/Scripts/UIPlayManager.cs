using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;

public class UIPlayManager : MonoBehaviour
{
    public static UIPlayManager instance;
    [SerializeField] private GameObject _uiDead,
        _uiPause,
        _uiPlay;
    [SerializeField] private TextMeshProUGUI _textTime, _textCoint,
        _cointAtD, _expAtD, _timeAtD, _heartLevel,
        _showCointinSkill
        ;
    [SerializeField] public TextMeshProUGUI _enermyCount;
    [SerializeField] private Button pauseBtn, rePlayBtn;
    private Animator _mainCameraAnimation;
    public Animator mainCameraAnimation
    {
        get
        {
            return _mainCameraAnimation;
        }
    }
    private float _startTime;

    public TextMeshProUGUI _levlShiel;
    public Image takeItemReaward;

    public float deltaTime
    {
        get
        {
            return UnityEngine.Time.time - _startTime;
        }
    }
    public GameObject uiDead
    {
        get
        {
            return _uiDead;
        }
    }

    public GameObject uiPause
    {
        get
        {
            return _uiPause;
        }
    }

    public GameObject uiPlay
    {
        get
        {
            return _uiPlay;
        }
    }

    public TextMeshProUGUI textCoint
    {
        get
        {
            return _textCoint;
        }
    }

    public GameObject uiTakeReward, uiRewardNotifi;


    private void PauseCtr()
    {
        UIPlayManager.instance.uiDead.SetActive(false);
        UIPlayManager.instance.uiPlay.SetActive(false);
        UIPlayManager.instance.uiPause.SetActive(true);
        Time.timeScale = 0;
    }

    private void RePlayCtr()
    {
        UIPlayManager.instance.uiDead.SetActive(false);
        UIPlayManager.instance.uiPlay.SetActive(true);
        UIPlayManager.instance.uiPause.SetActive(false);
        Time.timeScale = 1;
        PlayerControl.instance.isDie = false;
    }

    public void DeadCtr()
    {
        uiDead.SetActive(true);
        uiPlay.SetActive(false);
        uiPause.SetActive(false);
        _cointAtD.text = PlayerControl.instance.getCointinTower.ToString();
        _expAtD.text = (PlayerControl.instance.getCointinTower * userPointManager.UserPoint.towerLV[PlayerControl.instance.currentTower].enermyLevel).ToString();
        TimeSpan timeSpan = TimeSpan.FromSeconds(UnityEngine.Time.time - _startTime);
        string formattedTime = string.Format("{0:D2}:{1:D2}:{2:D2}",
                                             timeSpan.Hours,
                                             timeSpan.Minutes,
                                             timeSpan.Seconds);
        _timeAtD.text = formattedTime;
        Time.timeScale = 0;
    }

    public Slider uiShowExp;

    public async void toHome()
    {
        AsyncOperation scene = SceneManager.LoadSceneAsync("Home");
        if (scene != null)
        {
            scene.allowSceneActivation = false;
            await Task.Delay(1000);
            scene.allowSceneActivation = true;
        }
    }

    public void closeRewardUI()
    {
        PlayerControl.instance.resetSpawner();
    }

    public void ResetTimeScale()
    {
        Time.timeScale = 1;
    }

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        _startTime = UnityEngine.Time.time;
    }

    private void Start()
    {
        this.RePlayCtr();
        if (pauseBtn != null) pauseBtn.onClick.AddListener(() =>
        {
            this.PauseCtr();
        });
        if (rePlayBtn != null) rePlayBtn.onClick.AddListener(() =>
        {
            this.RePlayCtr();
        });
        CheckExp();
        _heartLevel.text = userPointManager.UserPoint.heartLV.ToString();
        _mainCameraAnimation = transform.parent.GetComponent<Animator>();
    }

    private void Update()
    {
        if(_textTime != null)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(UnityEngine.Time.time - _startTime);
            string formattedTime = string.Format("{0:D2}:{1:D2}:{2:D2}",
                                                 timeSpan.Hours,
                                                 timeSpan.Minutes,
                                                 timeSpan.Seconds);
            _textTime.text = formattedTime;
        }
        if (_enermyCount != null) _enermyCount.text = (Mathf.Clamp(PlayerControl.instance.upLevelEner - userPointManager.UserPoint.towerLV[PlayerControl.instance.currentTower].timeSpawn, 0, int.MaxValue)).ToString();
    }

    public void CheckExp()
    {
        while ((userPointManager.UserPoint.exp - (PlayerControl.instance.upExpPrinc * (userPointManager.UserPoint.heartLV - 1))) >= (PlayerControl.instance.upExpPrinc * (userPointManager.UserPoint.heartLV)))
        {
            userPointManager.UserPoint.heartLV++;
            _heartLevel.text = userPointManager.UserPoint.heartLV.ToString();
        }
        //Debug.Log((userPointManager.UserPoint.exp - (PlayerControl.instance.upExpPrinc * (userPointManager.UserPoint.heartLV - 1))) + " / " + (PlayerControl.instance.upExpPrinc * (userPointManager.UserPoint.heartLV)));
        if (uiShowExp != null)
            uiShowExp.value = ((userPointManager.UserPoint.exp - (PlayerControl.instance.upExpPrinc * (userPointManager.UserPoint.heartLV - 1))) / (PlayerControl.instance.upExpPrinc * (userPointManager.UserPoint.heartLV)));
    }
}
