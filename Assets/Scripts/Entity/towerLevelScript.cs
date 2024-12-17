using System.Collections.Generic;
using System;

[Serializable]
public class towerLevelScript
{
    public int enermyLevel = 1;
    public int timeSpawn = 0;
    public int currentTime = 0;
    public int currentPoint = 0;
    public string sceneName = "";
    public List<uint> levelSubTower;

    public towerLevelScript(string sceneName)
    {
        this.enermyLevel = 1;
        this.currentTime = 0;
        this.currentPoint = 0;
        this.sceneName = sceneName;
    }

    public towerLevelScript(string sceneName, int n)
    {
        this.enermyLevel = 1;
        this.currentTime = 0;
        this.currentPoint = 0;
        this.sceneName = sceneName;

        levelSubTower = new List<uint>();
        for(int i = 0; i < n; i++)
        {
            levelSubTower.Add(0);
        }
    }
}
