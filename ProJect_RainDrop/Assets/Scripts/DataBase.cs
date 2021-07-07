/* 총괄 DataBase */

using System;
using UnityEngine;


public class DataBase : MonoBehaviour {
    [HideInInspector] public static long money = 0; // 돈 
    [HideInInspector] public static long savedWater = 0; // 모은 빗물양
    [HideInInspector] public static long desertWater = 0; // 모은 사막빗물 양
    [HideInInspector] public static long uncleanedWater = 0; // 정화 전 빗물 양
    [HideInInspector] public static long cleanedWater = 0; // 정화 후 빗물 양
    
    [HideInInspector] public static int[] upgradePail = {10000, 30000};
    [HideInInspector] public static int[] upgradeTank = {10000, 25000};
    [HideInInspector] public static int[] upgradeClean = {10000, 50000};
    [HideInInspector] public static int[] upgradePot = {50000, 120000};
    [HideInInspector] public static float upEfficiency = 2.71f; // 업글시 증가하는 성능 비율

    [HideInInspector] public static float perDrop = 0;
    [HideInInspector] public static float perSecond = 0;

    [HideInInspector] public static int pailLevel = 1; // 양동이 레벨
    [HideInInspector] public static int tankLevel = 0; // 물 저장소 레벨
    [HideInInspector] public static int[] potLevel = new int[4]; // 양동이 레벨[지역별]
    [HideInInspector] public static int cleanLevel = 1; //물 정화기 레벨

    [HideInInspector] public static float[] rainCycle = {2f, 2f, 2f, 20f}; // 지역별 비오는 주기(초)
    [HideInInspector] public static short nowLocal = 0; // 현 위치

    private void Update()
    {
    }

    // private void Start()
// {
// 
//     //TODO: Key, Value 데이터 저장 시스템 구축
// }
}