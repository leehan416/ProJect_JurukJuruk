using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_MultiScene : MonoBehaviour {
    // transform에 접근하기 위한 instance
    public static UI_MultiScene instance;

    // 물통 옆 현재 각 빗물 Text set
    private Slider waterTank;
    private Text money;

    private Text[] waterCounter = new Text[3];

    private void Start()
    {
        if (!instance) instance = this;
        else Destroy(this);
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

    public void setWaterCounter()
    {
        DataBase.GetWaterData(); // 빗물 데이터 가져오기

        //text set
        waterCounter[0].text = DataBase.uncleanedWater.ToString();
        waterCounter[1].text = DataBase.cleanedWater.ToString();
        waterCounter[2].text = DataBase.desertWater.ToString();
    }


    // 물탱크 초기 세팅
    public void setWaterTank()
    {
        DataBase.GetWaterData(); // 빗물 데이터 가져오기
        DataBase.GetLevels(); // 레벨 데이터 가져오기
        waterTank.maxValue = DataBase.valueMaxWater[DataBase.tankLevel]; // maxvalue = 현재 레벨의 빗물 최대 양
        waterTank.minValue = 0f; // min = 0 
    }

    // 물탱크 static value 변경
    public void updateWaterTank()
    {
        DataBase.GetWaterData(); // 빗물 데이터 가져오기
        waterTank.value = DataBase.AllWater(); //  value = 빗물 전체 합
    }

    // 현재 돈 text set
    public void setMoney()
    {
        DataBase.GetMoney(); // 돈 데이터 가져오기
        money.text = Convert.ToString(DataBase.money) + " $"; // text set
    }

    //Scene 이동 
    public void moveScene(string val)
    {
        SceneManager.LoadScene(val);
    }
}