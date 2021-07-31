/* 총괄 DataBase */

using System;
using UnityEngine;


// pail => user pot 
// pot => extra pot 

public class DataBase : MonoBehaviour {
    //Fisrt access?
    public static bool isReset = true;

    // meony value
    public static long money = 0;

    //Water value
    public static long desertWater = 0; // 모은 사막빗물 양
    public static long uncleanedWater = 0; // 정화 전 빗물 양
    public static long cleanedWater = 0; // 정화 후 빗물 양

    public static long maxWater = 10000; // 최대 양

    //--------------------------------------------------------
    public static int perDrop = 1000;
    public static int pailLevel = 1; // 양동이 레벨
    public static int tankLevel = 1; // 물 저장소 레벨
    public static int[] potLevel = new int[4]; // 양동이 레벨[지역별]
    public static int[] potWater = new int[4]; // 양동이 빗물양 [지역별]
    public static int[] potMax = new int[4]; // 양동이 빗물양 [지역별]
    public static int cleanLevel = 1; //물 정화기 레벨
    public static int perclean = 100; //물 정화기 레벨

    public static int nowLocal = 0; // 현 위치

    public static Local[] local = new Local[4];
    public static Consumer[] consumerList = new Consumer[3];

    //--------------------------------------------------------

    //Time value

    public static DateTime lateTime;

    //--------------------------------------------------------

    //Setting value
    public static float bgmVol = .7f;
    public static float fxVol = .7f;
    public static bool isReverse = false;


    //--------------------------------------------------------
    //System value 
    public static int[] perSecond = {100, 1000, 10000, 100000}; //new int[4];
    public static float[] rainCycle = {1f, 1.5f, .5f, 5f}; // 지역별 비오는 주기(초)
    public static String[] localName = {"우리집 마당", "시골집 뒷마당", "아마존 캠프", "피라미드 앞"};
    public static int[] valuePerDrop = {1000, 10000, 100000, 100000, 10000000};
    public static int[] valueMaxWater = {1000000, 100000000, 100000000, 100000000, 10000000};
    public static int[] upgradePail = {10000, 30000, 300000, 30000003, 300000003};
    public static int[] upgradeTank = {10000, 25000, 50000, 60000, 70000, 800000};
    public static int[] upgradeClean = {10000, 50000};
    public static int[] upgradePot = {50000, 120000};
    public static int[] localCost = {0, 1000, 10000, 100000};

    //--------------------------------------------------------

    private void Awake()
    {
        // GetWaterData();
        // DataGet();
        GetWaterData();
        GetMoney();
        potLevel[0] = 1;
        potMax[0] = 10000;
        perSecond[0] = 1000;

        for (int i = 0; i < local.Length; i++)
            local[i] = new Local();
        for (int i = 0; i < consumerList.Length; i++)
            consumerList[i] = new Consumer();

        consumerList[0].perLiter = 100;
        consumerList[1].perLiter = 200;
        consumerList[2].perLiter = 500;
        local[0].isLock = false;
    }

    public static void SetMoney()
    {
        PlayerPrefs.SetString("Money", Convert.ToString(money));
    }

    public static void GetMoney()
    {
        money = Convert.ToInt64(PlayerPrefs.GetString("Money", "0"));
    }

    public static long AllWater() // 물 총량 return
    {
        return (uncleanedWater + cleanedWater + desertWater);
    }

    public static void SetWaterData()
    {
        PlayerPrefs.SetString("DesertWater", System.Convert.ToString(desertWater));
        PlayerPrefs.SetString("UncleanedWater", System.Convert.ToString(uncleanedWater));
        PlayerPrefs.SetString("CleanedWater", System.Convert.ToString(cleanedWater));
        for (int i = 0; i < potWater.Length; i++)
            PlayerPrefs.SetInt("PotWater" + i, potWater[i]);
    }

    public static void GetWaterData()
    {
        uncleanedWater = Convert.ToInt64(PlayerPrefs.GetString("UncleanedWater", "0"));
        cleanedWater = Convert.ToInt64(PlayerPrefs.GetString("CleanedWater", "0"));
        desertWater = Convert.ToInt64(PlayerPrefs.GetString("DesertWater", "0"));
        for (int i = 0; i < potWater.Length; i++)
            potWater[i] = PlayerPrefs.GetInt("PotWater" + i, 0);
    }

    public static void GetSettingVal()
    {
        bgmVol = PlayerPrefs.GetFloat("BgmVol", .7f);
        fxVol = PlayerPrefs.GetFloat("FxVol", .7f);
        isReverse = Convert.ToBoolean(PlayerPrefs.GetInt("IsReverse", 0));
    }

    public static void SetSettingVal()
    {
        PlayerPrefs.SetFloat("BgmVol", bgmVol);
        PlayerPrefs.SetFloat("FxVol", fxVol);
        PlayerPrefs.SetInt("IsReverse", Convert.ToInt32(isReverse));
        Debug.Log(PlayerPrefs.GetInt("IsReverse"));
    }

    public static void GetLateTime()
    {
        lateTime = Convert.ToDateTime(PlayerPrefs.GetString("", Convert.ToString(DateTime.Now)));
    }

    public static void SetLateTime()
    {
        PlayerPrefs.SetString("LateTime", Convert.ToString(lateTime));
    }
}

public class Local {
    public int cost;
    public string title;
    public bool isLock = true;

    public float rainCycle;
    // public Local(int val)
    // {
    //     //isLock = DataBase.isLocalLock[val];
    //     cost = DataBase.localCost[val];
    // }
}

public class Consumer {
    public int perLiter = 10;
    public bool isCleaned = false;
    public Sprite image;
    public string story;
}