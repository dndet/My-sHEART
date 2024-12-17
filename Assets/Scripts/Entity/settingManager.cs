using System;
using UnityEngine;

[Serializable]
public class SettingJson
{
    public float soundSF, soundBG;
    public int qualityIndex;
    public int targetFps;
    public bool isOnSound;

    public SettingJson()
    {
        this.soundSF = 0.15f;
        this.soundBG = 0.5f;
        this.qualityIndex = 3;
        this.targetFps = 40;
        this.isOnSound = true;
    }

    public string toJson()
    {
        return JsonUtility.ToJson(this);
    }

    public static SettingJson fromJson(string str)
    {
        return JsonUtility.FromJson<SettingJson>(str);
    }
}

public static class settingManager
{
    public static SettingJson settingJson = new SettingJson();
    public static readonly string fileName = "SaveSetting";
}
