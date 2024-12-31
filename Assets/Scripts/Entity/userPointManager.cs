using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class items
{
    public string name;
    public string title;
    public int expense;
    public string des;

    public items(string name, string title, int expense, string des)
    {
        this.name = name;
        this.title = title;
        this.expense = expense;
        this.des = des;
    }
}

[Serializable]
public class userPoint
{
    public int coint = 0;
    public int exp = 0;
    public int[] chamgeLevel = { 0, 25, 75, 150, 250, 500, 1000, 1750, 3000, 5500, 7000, 10000, 15500, 21000 };
    public int fireBallLV = 1, heartLV = 1, shieldLV = 0;
    public List<towerLevelScript> towerLV = new List<towerLevelScript> ();
    public List<int> skillLevel = new List<int>{ 0, 0, 0 };
    public List<int> itemInvent = new List<int> ();
    public List<int> itemBackpack = new List<int>(3);

    public userPoint(List<towerLevelScript> tower)
    {
        this.towerLV = tower;
    }
}

public static class userPointManager
{
    public static userPoint UserPoint = new userPoint (new List<towerLevelScript>
    {
        new towerLevelScript("P3Tower" , 1),
        new towerLevelScript("P4Tower" , 2),
        new towerLevelScript("P5Tower" , 3)
    });

    public static List<items> shopInfor = new List<items>
    {
        new items("restore_HP", "Restore HP", 25, "+ 10 HP"),
        new items("restore_%HP", "Restore % HP", 34, "+ 50 %HP"),
        new items("upGradeShiel", "Restore Shiel", 48, "+ 5 Shiel"),
        //new items("restore_HP", "Restore HP", 11, "+ 2 HP"),
        //new items("restore_HP", "Restore HP", 33, "+ 2 HP"),
        //new items("restore_HP", "Restore HP", 92, "+ 2 HP")
    };

    public static string toJsonCode(userPoint userPoint)
    {
        string json = JsonUtility.ToJson(userPoint);
        return json;
    }

    public static userPoint getJsonCode(string str)
    {
        return JsonUtility.FromJson<userPoint>(str);
    }

    public static void SaveData()
    {
        PlayerPrefs.SetString("GameData", toJsonCode(UserPoint));
    }
}
