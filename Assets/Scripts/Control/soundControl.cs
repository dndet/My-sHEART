using UnityEngine;
using UnityEngine.UI;

public class soundControl : MonoBehaviour
{
    [SerializeField] private AudioSource _soundEf, _soundBG;
    [SerializeField] private SliderSub _sEfSlider, _sBgSlider;
    [SerializeField] private Toggle _soundToggle;
    [SerializeField] private AudioListener _listener;

    void Start()
    {
        try
        {
            _soundEf.volume = settingManager.settingJson.soundSF;
            _soundBG.volume = settingManager.settingJson.soundBG;
            _sBgSlider.value = settingManager.settingJson.soundBG;
            _sEfSlider.value = settingManager.settingJson.soundSF;
            _listener.enabled = settingManager.settingJson.isOnSound;
            _soundToggle.isOn = settingManager.settingJson.isOnSound;

            _sBgSlider.onValueChanged.AddListener((float value) =>
            {
                try
                {
                    _soundToggle.isOn = true;
                    _sBgSlider.value = value;
                }
                catch { }
                settingManager.settingJson.soundBG = value;
                _soundBG.volume = settingManager.settingJson.soundBG;
            });

            _sEfSlider.onValueChanged.AddListener((float value) =>
            {
                try
                {
                    _soundToggle.isOn = true;
                    _sEfSlider.value = value;
                }
                catch { }
                settingManager.settingJson.soundSF = value;
                _soundEf.volume = settingManager.settingJson.soundSF;
            });
            _soundToggle.onValueChanged.AddListener((bool value) =>
            {
                settingManager.settingJson.isOnSound = value;
                try
                {
                    _sBgSlider.value = 0;
                    _sEfSlider.value = 0;
                    _soundToggle.isOn = value;
                }
                catch { }
                _listener.enabled = settingManager.settingJson.isOnSound;
            });
        }
        catch { }
    }
}
