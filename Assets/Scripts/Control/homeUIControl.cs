using System;
using System.Threading.Tasks;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class homeUIControl : MonoBehaviour
{
    [SerializeField] private Button _playBtn, _shopBtn, _settingBtn, _helpBtn, _confirmQuitBrn;
    [SerializeField] private GameObject _home, _playUi, _shopUi, _settingUi, _helpUi, _quitUi
        , uiLoading
        ;

    [SerializeField] private Transform _levelList, _showTower;
    [SerializeField] private AudioSource _soundEf, _soundBG;

    private const string dataName = "GameData";

    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.DeleteKey(dataName);
        if (_home != null) _home.SetActive(PlayerPrefs.GetString("SaveSetting") != "");
        if (_playUi != null) _playUi.SetActive(false);
        if (_shopUi != null) _shopUi.SetActive(false);
        if (_settingUi != null) _settingUi.SetActive(false);
        if (_helpUi != null) _helpUi.SetActive(PlayerPrefs.GetString("SaveSetting") == "");
        if (_quitUi != null) _quitUi.SetActive(false);
        if (PlayerPrefs.GetString("SaveSetting") == "")
        {
            PlayerPrefs.SetString("SaveSetting", settingManager.settingJson.toJson());
        }
        else
        {
            settingManager.settingJson = SettingJson.fromJson(PlayerPrefs.GetString("SaveSetting"));
        }

        string gameData = PlayerPrefs.GetString(dataName);
        if (gameData == "")
        {
            PlayerPrefs.SetString(dataName, userPointManager.toJsonCode(userPointManager.UserPoint));
        }
        else
        {
            userPointManager.UserPoint = userPointManager.getJsonCode(gameData);
        }
    }

    void Start()
    {
        Time.timeScale = 1.0f;
        Application.targetFrameRate = settingManager.settingJson.targetFps;
        QualitySettings.SetQualityLevel(settingManager.settingJson.qualityIndex);


        if (_confirmQuitBrn != null)
        {
            _confirmQuitBrn.onClick.AddListener(this.QuitGame);
        }

        for (int i = 0; i < 3; i++)
        {
            towerLevelScript tl = userPointManager.UserPoint.towerLV[i];
            if (_levelList != null)
            {
                _levelList.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = tl.sceneName;
                _levelList.GetChild(i).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = tl.enermyLevel.ToString();
                _levelList.GetChild(i).GetChild(2).GetComponent<Button>().interactable = (i == 0 || userPointManager.UserPoint.towerLV[i - 1].enermyLevel >= (8 * i));
                _levelList.GetChild(i).GetChild(3).gameObject.SetActive(i > 0 && userPointManager.UserPoint.towerLV[i - 1].enermyLevel < (8 * i));
                _levelList.GetChild(i).GetChild(2).GetComponent<Button>().onClick.AddListener(() =>
                {
                    _showTower.gameObject.SetActive(true);
                    _showTower.GetChild(0).GetComponent<TextMeshProUGUI>().text = tl.sceneName;
                    _showTower.GetChild(1).GetComponent<TextMeshProUGUI>().text = tl.enermyLevel.ToString();

                    TimeSpan timeSpan = TimeSpan.FromSeconds(tl.currentTime);
                    string formattedTime = string.Format("{0:D2}:{1:D2}:{2:D2}",
                                                         timeSpan.Hours,
                                                         timeSpan.Minutes,
                                                         timeSpan.Seconds);
                    _showTower.GetChild(2).GetComponent<TextMeshProUGUI>().text = formattedTime;
                    _showTower.GetChild(3).GetComponent<TextMeshProUGUI>().text = tl.currentPoint.ToString();

                    _showTower.GetChild(4).GetComponent<Image>().sprite = Resources.Load<Sprite>("IconLevel/" + tl.sceneName);
                    _showTower.GetChild(6).GetComponent<Button>().onClick.RemoveAllListeners();
                    _showTower.GetChild(6).GetComponent<Button>().onClick.AddListener(async () =>
                    {
                        AsyncOperation scene = SceneManager.LoadSceneAsync(tl.sceneName);
                        if (scene != null)
                        {
                            scene.allowSceneActivation = false;
                            if (uiLoading != null) uiLoading.SetActive(true);
                            await Task.Delay(2000);
                            if (uiLoading != null) uiLoading.SetActive(false);
                            scene.allowSceneActivation = true;
                        }
                    });
                });
            }
        }
        _soundEf.volume = settingManager.settingJson.soundSF;
        _soundBG.volume = settingManager.settingJson.soundBG;
    }

    private void OnDestroy()
    {
        userPointManager.SaveData();
        PlayerPrefs.SetString("SaveSetting", settingManager.settingJson.toJson());
    }

    private void OnApplicationQuit()
    {
        userPointManager.SaveData();
        PlayerPrefs.SetString("SaveSetting", settingManager.settingJson.toJson());
    }

    private void OnApplicationPause()
    {
        userPointManager.SaveData();
        PlayerPrefs.SetString("SaveSetting", settingManager.settingJson.toJson());
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

#if UNITY_EDITOR
    [MenuItem("Tools/Clear PlayerPrefs")]
    public static void ClearAllPlayerPrefs()
    {
        // Xóa toàn bộ dữ liệu trong PlayerPrefs
        PlayerPrefs.DeleteAll();
        // Lưu thay đổi
        PlayerPrefs.Save();
    }
#endif
}
