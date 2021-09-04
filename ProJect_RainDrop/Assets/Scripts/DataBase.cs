/* 총괄 DataBase */

using System;
using UnityEngine;

// pail => user pot 
// pot => extra pot 

public class DataBase : MonoBehaviour {
    // meony value
    public static long money = 0;

    //Water value
    public static long desertWater = 0; // 모은 사막빗물 양
    public static long uncleanedWater = 0; // 정화 전 빗물 양
    public static long cleanedWater = 0; // 정화 후 빗물 양

    //public static long maxWater = 10000; // 최대 양
    public static int[] potWater = new int[4]; // 양동이 빗물양 [지역별]

    //--------------------------------------------------------
    public static int pailLevel = 1; // 양동이 레벨
    public static int tankLevel = 1; // 물 저장소 레벨
    public static int[] potLevel = new int[4]; // 양동이 레벨[지역별]

    public static int cleanLevel = 1; //물 정화기 레벨

    public static int nowLocal = 0; // 현 위치

    public static int costume = 0; // 현 위치
    public static bool[] isCostumeLock = {false, true, true, true, true, true}; // 지역 해금 변수

    public static bool[] isLocalLock = {false, true, true, true}; // 지역 해금 변수

    //TODO 최적화 필요
    // public static Local[] local = new Local[4];

    // public static Consumer[] consumerList = new Consumer[3];

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
    public static float[] rainCycle = {1f, 1f, .5f, 5f}; // 지역별 비오는 주기(초)
    public static int[] potCycle = {5, 5, 4, 60};
    public static int[] localCost = {0, 5000, 10000, 20000};
    public static String[] localName = {"우리집 마당", "시골집 뒷마당", "아마존 캠프", "피라미드 앞"};

    // pail
    public static int[] valuePerDrop = {10, 15, 20, 23, 26, 28, 30, 32, 34, 36, 38, 40};
    public static int[] upgradePail = {0, 200, 300, 500, 700, 1000, 1500, 2200, 3000, 3900, 4900, 6000};

    //tank
    public static int[] valueMaxWater =
        {3000, 5000, 7000, 9000, 12000, 15000, 20000, 27000, 34000, 43000, 52000, 64000};

    public static int[] upgradeTank = {0, 1000, 1200, 1500, 2000, 2700, 3400, 4500, 5400, 6400, 7200, 9000};

    //clean
    public static int[] valueCleanWater = {1, 2, 3, 5, 7, 10, 15, 20};
    public static int[] upgradeClean = {0, 100, 200, 300, 500, 700, 1000, 1500};

    //pot
    public static int[] unLockPot = {1000, 3000, 4000, 5000};
    public static int[] perSecond = {5, 7, 10, 12, 13, 14, 15, 16};
    public static float[] valuePotMax = {1800, 2500, 3600, 4300, 4700, 5000, 5400, 5800};
    public static int[] upgradePot = {0, 300, 400, 500, 600, 700, 800, 900};

    public static int[] costConsummer = {100, 200, 500};

    //fever
    public static int feverTime = 30;

    //public static int catDrop = 300; //random(240, 300)
    public static int catSustainTime = 20;
    public static float feverEfficiency = 10; // 빗물 내리는 시간 줄여주는 비율
    public static float feverDrop = .45f; // 고양이 등장 판별하는 물탱크의 물 양
    public static bool isFeverChecked = false; // 고양이 등장 판정 검사했음?


    //gacha
    public static int gachaCost = 3000;

    //--------------------------------------------------------

    public static void setMoney()
    {
        PlayerPrefs.SetString("Money", Convert.ToString(money));
    }

    public static void getMoney()
    {
        money = Convert.ToInt64(PlayerPrefs.GetString("Money", "0"));
    }

    public static long getAllWater() // 물 총량 return
    {
        return (uncleanedWater + cleanedWater + desertWater);
    }

    public static float getWaterTankPercent()
    {
        return Convert.ToSingle(DataBase.getAllWater()) /
               Convert.ToSingle(DataBase.valueMaxWater[DataBase.tankLevel]);
    }


    public static void setWaterData()
    {
        PlayerPrefs.SetString("DesertWater", Convert.ToString(desertWater));
        PlayerPrefs.SetString("UncleanedWater", Convert.ToString(uncleanedWater));
        PlayerPrefs.SetString("CleanedWater", Convert.ToString(cleanedWater));
        for (int i = 0; i < potWater.Length; i++)
            PlayerPrefs.SetInt("PotWater" + i, potWater[i]);
    }

    public static void getWaterData()
    {
        uncleanedWater = Convert.ToInt64(PlayerPrefs.GetString("UncleanedWater", "0"));
        cleanedWater = Convert.ToInt64(PlayerPrefs.GetString("CleanedWater", "0"));
        desertWater = Convert.ToInt64(PlayerPrefs.GetString("DesertWater", "0"));
        for (int i = 0; i < potWater.Length; i++)
            potWater[i] = PlayerPrefs.GetInt("PotWater" + i, 0);
    }


    public static void setLevels()
    {
        PlayerPrefs.SetInt("PailLevel", pailLevel);
        PlayerPrefs.SetInt("TankLevel", tankLevel);
        PlayerPrefs.SetInt("CleanLevel", cleanLevel);
        for (int i = 0; i < potLevel.Length; i++)
            PlayerPrefs.SetInt("PotLevel" + i, potLevel[i]);
    }

    public static void getLevels()
    {
        pailLevel = PlayerPrefs.GetInt("PailLevel", 0);
        tankLevel = PlayerPrefs.GetInt("TankLevel", 0);
        cleanLevel = PlayerPrefs.GetInt("CleanLevel", cleanLevel);
        for (int i = 0; i < potLevel.Length; i++)
            potLevel[i] = PlayerPrefs.GetInt("PotLevel" + i, 0);
    }

    public static void getSettingVal()
    {
        bgmVol = PlayerPrefs.GetFloat("BgmVol", .7f);
        fxVol = PlayerPrefs.GetFloat("FxVol", .4f);
        isReverse = Convert.ToBoolean(PlayerPrefs.GetInt("IsReverse", 0));
    }

    public static void setSettingVal()
    {
        PlayerPrefs.SetFloat("BgmVol", bgmVol);
        PlayerPrefs.SetFloat("FxVol", fxVol);
        PlayerPrefs.SetInt("IsReverse", Convert.ToInt32(isReverse));
        Debug.Log(PlayerPrefs.GetInt("IsReverse"));
    }


    public static void getLocalData(int val)
    {
        Debug.Log(Convert.ToBoolean(PlayerPrefs.GetInt("local+" + val + "_isLock", 1)));
        if (val == 0) return;
        isLocalLock[val] = Convert.ToBoolean(PlayerPrefs.GetInt("local+" + val + "_isLock", 1));
    }

    public static void setLocalData(int val)
    {
        PlayerPrefs.SetInt("local+" + val + "_isLock", (isLocalLock[val]) ? 1 : 0);
    }


    public static void getLateTime()
    {
        lateTime = Convert.ToDateTime(PlayerPrefs.GetString("LateTime", Convert.ToString(DateTime.Now)));
    }

    public static void setLateTime()
    {
        PlayerPrefs.SetString("LateTime", Convert.ToString(DateTime.Now));
    }

    public static void setCostume()
    {
        PlayerPrefs.SetInt("Costume", DataBase.costume);
        for (int i = 1; i < isCostumeLock.Length; i++)
        {
            PlayerPrefs.SetInt("IsCostumeLock" + i, Convert.ToInt32(isCostumeLock[i]));
        }
    }

    public static void getCoustume()
    {
        DataBase.costume = PlayerPrefs.GetInt("Costume", 0);
        for (int i = 1; i < isCostumeLock.Length; i++)
        {
            isCostumeLock[i] = Convert.ToBoolean(PlayerPrefs.GetInt("IsCostumeLock" + i, 1));
        }
    }
}

//
// public class Local {
//     public int cost;
//     public string title;
//     public bool isLock = false;
//
//     //  public float rainCycle;
//
//     // public Local(int val)
//     // {
//     //     title = DataBase.localName[val];
//     //     cost = DataBase.localCost[val];
//     //     if (val != 0)
//     //         isLock = true;
//     // }
// }
//
// public class Consumer {
//     public int perLiter = 10;
//
//     public Consumer(int val)
//     {
//         perLiter = DataBase.costConsummer[val];
//     }
// }