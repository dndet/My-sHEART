using UnityEngine;

public class gamePlayControl : MonoBehaviour
{
    [SerializeField] private SliderSub _fpsSlider, _qualitySlider;

    void Start()
    {
        Application.targetFrameRate = settingManager.settingJson.targetFps;
        QualitySettings.SetQualityLevel(settingManager.settingJson.qualityIndex);
        _fpsSlider.value = (settingManager.settingJson.targetFps) / _fpsSlider.fillIndex;
        _qualitySlider.value = settingManager.settingJson.qualityIndex;

        _fpsSlider.onValueChanged.AddListener((value) =>
        {
            settingManager.settingJson.targetFps = (int) (_fpsSlider.value * _fpsSlider.fillIndex);
            Application.targetFrameRate = settingManager.settingJson.targetFps;
            QualitySettings.SetQualityLevel(settingManager.settingJson.qualityIndex);
        });

        _qualitySlider.onValueChanged.AddListener((value) =>
        {
            settingManager.settingJson.qualityIndex = (int)_qualitySlider.value;
            Application.targetFrameRate = settingManager.settingJson.targetFps;
            QualitySettings.SetQualityLevel(settingManager.settingJson.qualityIndex);
        });
    }
}
