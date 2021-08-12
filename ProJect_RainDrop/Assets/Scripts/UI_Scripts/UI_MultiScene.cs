using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_MultiScene : MonoBehaviour {
    // 물통 옆 현재 각 빗물 Text set


    public static Slider waterTank;
    public static Text money;

    public static Text[] waterCounter = new Text[3];

    private void Start()
    {
        money = GameObject.Find("Canvas/MoneyBack/Money").GetComponent<Text>(); // money

        try
        {
            waterTank = GameObject.Find("Canvas/Tank").GetComponent<Slider>(); // waterTank
            waterCounter[0] = GameObject.Find("Canvas/Tank/ShowAmount/ShowAmount_Basic").GetComponent<Text>();
            waterCounter[1] = GameObject.Find("Canvas/Tank/ShowAmount/ShowAmount_Clean").GetComponent<Text>();
            waterCounter[2] = GameObject.Find("Canvas/Tank/ShowAmount/ShowAmount_Desert").GetComponent<Text>();
        }
        catch (Exception e)
        {
            return;
        }
    }

    public static void setWaterCounter()
    {
        DataBase.GetWaterData(); // 빗물 데이터 가져오기

        //text set
        waterCounter[0].text = DataBase.uncleanedWater.ToString();
        waterCounter[1].text = DataBase.cleanedWater.ToString();
        waterCounter[2].text = DataBase.desertWater.ToString();
    }


    // 물탱크 초기 세팅
    public static void setWaterTank()
    {
        DataBase.GetWaterData(); // 빗물 데이터 가져오기
        DataBase.GetLevels(); // 레벨 데이터 가져오기
        waterTank.maxValue = DataBase.valueMaxWater[DataBase.tankLevel]; // maxvalue = 현재 레벨의 빗물 최대 양
        waterTank.minValue = 0f; // min = 0 
    }

    // 물탱크 static value 변경
    public static void UpdateWaterTank()
    {
        DataBase.GetWaterData(); // 빗물 데이터 가져오기
        waterTank.value = DataBase.AllWater(); //  value = 빗물 전체 합
    }

    // 현재 돈 text set
    public static void setMoney()
    {
        DataBase.GetMoney(); // 돈 데이터 가져오기
        money.text = Convert.ToString(DataBase.money) + " $"; // text set
    }

    //Scene 이동 
    public static void moveScene(string val)
    {
        SceneManager.LoadScene(val);
    }
}