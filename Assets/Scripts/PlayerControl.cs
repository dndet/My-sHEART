using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl instance;
    [HideInInspector] public Transform _targetPos;
    public Transform _heartTrans, _shieldHeart;

    [SerializeField] public GameObject _fireBall;
    [SerializeField] public GameObject fireBallSkill, clearZone;
    [SerializeField] private GameObject _cointPrefab;
    [SerializeField] private AudioListener audioListener;
    public GameObject cointPrefab { get { return _cointPrefab; } }
    public heartScript _heart;
    public int currentTower = 0;
    public bool isDie;

    public GameObject powerBoom;
    public int enermySpawning = 0;
    public readonly float upExpPrinc = 95.0f;

    [SerializeField] private SpawnEnermyScript[] scriptEnermy;

    private int _upLevelEner;
    public int upLevelEner
    {
        get
        {
            return _upLevelEner;
        }
    }

    private int _cointTower;
    public int getCointinTower
    {
        get
        {
            return _cointTower;
        }
    }

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        isDie = false;
    }

    private void Start()
    {
        if(audioListener != null)
        {
            audioListener.enabled = settingManager.settingJson.isOnSound;
        }
        _upLevelEner = 48 + userPointManager.UserPoint.towerLV[currentTower].enermyLevel * 2 + currentTower * 3;
    }

    public void takeCoint(int c)
    {
        if (c >= 0)
        {
            userPointManager.UserPoint.coint += c;
            userPointManager.UserPoint.exp += (c * userPointManager.UserPoint.towerLV[currentTower].enermyLevel);
            _cointTower += c;
            UIPlayManager.instance.CheckExp();
        }
        UIPlayManager.instance.textCoint.text = userPointManager.UserPoint.coint.ToString();
    }

    public async void takeReward()
    {
        foreach(SpawnEnermyScript i in  scriptEnermy)
        {
            i.StopPawn();
        }
        Time.timeScale = 0.5f;
        await Task.Delay(1500);
        int eLV = userPointManager.UserPoint.towerLV[currentTower].enermyLevel;
        this.takeCoint(25 + (eLV + currentTower) * 5);
        Time.timeScale = 0;
        UIPlayManager.instance.uiRewardNotifi?.SetActive(true);
        try
        {
            UIPlayManager.instance.uiRewardNotifi.transform.Find("Notifi").Find("cointNun").GetComponent<TextMeshProUGUI>().text = (25 + (eLV + currentTower) * (eLV % 5 == 0 ? 7 : 5)).ToString();
        }
        catch { }
        if (eLV % 5 == 0)
        {
            UIPlayManager.instance.uiTakeReward?.SetActive(true);
            if(UIPlayManager.instance.takeItemReaward != null)
            {
                int rewardIndex = Random.Range(0, userPointManager.shopInfor.Count - 1);
                UIPlayManager.instance.takeItemReaward.sprite = Resources.Load<Sprite>("IconItems/" + userPointManager.shopInfor[rewardIndex].name);
                userPointManager.UserPoint.itemInvent.Add(rewardIndex);
                this.takeCoint((eLV + currentTower) * 2);
            }
        }
        else
        {
            UIPlayManager.instance.uiTakeReward?.SetActive(false);
            //UIPlayManager.instance.uiRewardNotifi?.SetActive(false);
        }
        userPointManager.UserPoint.towerLV[PlayerControl.instance.currentTower].enermyLevel++;
        _upLevelEner = 48 + userPointManager.UserPoint.towerLV[currentTower].enermyLevel * 2 + currentTower * 3;
        userPointManager.UserPoint.towerLV[PlayerControl.instance.currentTower].timeSpawn = 0;
    }

    public void resetSpawner()
    {
        foreach (SpawnEnermyScript i in scriptEnermy)
        {
            i.Spawning();
        }
    }

    void SavePoint()
    {
        userPointManager.UserPoint.towerLV[currentTower].currentTime += (int)UIPlayManager.instance.deltaTime;
        if (userPointManager.UserPoint.towerLV[currentTower].currentTime < 0) userPointManager.UserPoint.towerLV[currentTower].currentTime = 0;
        userPointManager.UserPoint.towerLV[currentTower].currentPoint += (int)userPointManager.UserPoint.exp + userPointManager.UserPoint.towerLV[currentTower].enermyLevel;
        if (userPointManager.UserPoint.towerLV[currentTower].currentPoint < 0) userPointManager.UserPoint.towerLV[currentTower].currentPoint = 0;
        PlayerPrefs.SetString("GameData", userPointManager.toJsonCode(userPointManager.UserPoint));
    }

    private void OnDestroy()
    {
        SavePoint();
        Time.timeScale = 1.0f;
    }

    private void OnApplicationPause()
    {
        SavePoint();
        Time.timeScale = 1.0f;
    }

    private void OnApplicationQuit()
    {
        SavePoint();
        Time.timeScale = 1.0f;
    }
}
