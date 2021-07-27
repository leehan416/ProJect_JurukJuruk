/* 총괄 DataBase */

using System;
using UnityEngine;


// pail => user pot 
// pot => extra pot 

public class DataBase : MonoBehaviour {
    [HideInInspector] public static long money = 0; // 돈 

    [HideInInspector] public static long desertWater = 0; // 모은 사막빗물 양
    [HideInInspector] public static long uncleanedWater = 0; // 정화 전 빗물 양
    [HideInInspector] public static long cleanedWater = 0; // 정화 후 빗물 양
    [HideInInspector] public static long maxWater = 10000; // 최대 양

    [HideInInspector] public static int perDrop = 1000;

    [HideInInspector] public static int[] perSecond = new int[4];

    [HideInInspector] public static int pailLevel = 1; // 양동이 레벨
    [HideInInspector] public static int tankLevel = 1; // 물 저장소 레벨

    [HideInInspector] public static int[] potLevel = new int[4]; // 양동이 레벨[지역별]

    [HideInInspector] public static DateTime lateTime;


    /*[HideInInspector]*/
    public static int[] potWater = new int[4]; // 양동이 빗물양 [지역별]
    [HideInInspector] public static int[] potMax = new int[4]; // 양동이 빗물양 [지역별]

    [HideInInspector] public static int cleanLevel = 1; //물 정화기 레벨
    [HideInInspector] public static int perclean = 100; //물 정화기 레벨

    [HideInInspector] public static float[] rainCycle = {1f, 1.5f, .5f, 5f}; // 지역별 비오는 주기(초)
    [HideInInspector] public static int nowLocal = 0; // 현 위치

    [HideInInspector] public static String timeRecord = ""; // 시간 변수

    //Setting value
    [HideInInspector] public static float bgmVol;
    [HideInInspector] public static float fxVol;

    [HideInInspector] public static bool isReverse = false;

    //const value 
    [HideInInspector] public static String[] localName = {"우리집 마당", "시골집 뒷마당", "아마존 캠프", "피라미드 앞"};
    [HideInInspector] public static int[] upgradePail = {10000, 30000};
    [HideInInspector] public static int[] upgradeTank = {10000, 25000};
    [HideInInspector] public static int[] upgradeClean = {10000, 50000};
    [HideInInspector] public static int[] upgradePot = {50000, 120000};
    [HideInInspector] public static float upEfficiency = 2.71f; // 업글시 증가하는 성능 비율
    [HideInInspector] public static int[] localCost = {0, 1000, 10000, 100000};
    [HideInInspector] public static bool[] isLocalLock = {false, true, true, true};


    private void Awake()
    {
        GetWaterData();
        DataGet();
        potLevel[0] = 1;
        potMax[0] = 10000;
        perSecond[0] = 1000;
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

    public static void DataSet()
    {
        PlayerPrefs.SetString("Money", System.Convert.ToString(money));
        PlayerPrefs.SetString("DesertWater", System.Convert.ToString(desertWater));
        PlayerPrefs.SetString("UncleanedWater", System.Convert.ToString(uncleanedWater));
        PlayerPrefs.SetString("CleanedWater", System.Convert.ToString(cleanedWater));
        PlayerPrefs.SetString("MaxWater", System.Convert.ToString(maxWater));

        PlayerPrefs.SetFloat("PerDrop", perDrop);


        for (int a = 0; a < potLevel.Length; a++)
            PlayerPrefs.SetFloat("PerSecond" + a, perSecond[a]);

        PlayerPrefs.SetInt("PailLevel", pailLevel);
        PlayerPrefs.SetInt("TankLevel", tankLevel);
        for (int a = 0; a < potLevel.Length; a++)
            PlayerPrefs.SetInt("PotLevel" + a, potLevel[a]);

        PlayerPrefs.SetInt("CleanLevel", cleanLevel);
        PlayerPrefs.SetInt("NowLocal", nowLocal);
        PlayerPrefs.SetString("TimeRecord", timeRecord);
    }

    public static void DataGet()
    {
        money = Convert.ToInt64(PlayerPrefs.GetString("Money", "0"));
        uncleanedWater = Convert.ToInt64(PlayerPrefs.GetString("UncleanedWater", "0"));
        cleanedWater = Convert.ToInt64(PlayerPrefs.GetString("CleanedWater", "0"));
        desertWater = Convert.ToInt64(PlayerPrefs.GetString("DesertWater", "0"));
        maxWater = Convert.ToInt64(PlayerPrefs.GetString("MaxWater", "10000"));

        perDrop = PlayerPrefs.GetInt("PerDrop", 1000);

        for (int i = 0; i < potLevel.Length; i++)
            perSecond[i] = PlayerPrefs.GetInt("PotSecond" + i, 0);

        pailLevel = PlayerPrefs.GetInt("PailLevel", 1);
        tankLevel = PlayerPrefs.GetInt("TankLevel", 1);

        for (int i = 0; i < potLevel.Length; i++)
            potLevel[i] = PlayerPrefs.GetInt("PotLevel" + i, 0);

        cleanLevel = PlayerPrefs.GetInt("CleanLevel", 1);
        nowLocal = PlayerPrefs.GetInt("NowLocal", 0);
        //timeRecord = PlayerPrefs.GetInt("NowLocal", 0);
        // 데이터를 활용하는 스크립트에서 알아서 가져감.
    }
}
public class Local { //// : MonoBehaviour {
    public int cost;
    public bool isLock;

    Local(int val)
    {
        isLock = DataBase.isLocalLock[val];
        cost = DataBase.localCost[val];
    }

    public static Local[] local = new Local[4];

    void Start()
    {
        for (int i = 1; i < local.Length; i++)
            local[i] = new Local(i);
        local[0].isLock = false;
    }
}