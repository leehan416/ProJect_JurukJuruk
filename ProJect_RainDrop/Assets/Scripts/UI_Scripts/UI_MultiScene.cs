using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_MultiScene : MonoBehaviour {
    // 물통 옆 현재 각 빗물 Text set
    public void setWaterCounter(Text[] text)
    {
        DataBase.GetWaterData(); // 빗물 데이터 가져오기
        
        //text set
        text[0].text = DataBase.uncleanedWater.ToString();
        text[1].text = DataBase.cleanedWater.ToString();
        text[2].text = DataBase.desertWater.ToString();
    }


    // 물탱크 초기 세팅
    public void waterTankSet(Slider slider)
    {
        DataBase.GetWaterData(); // 빗물 데이터 가져오기
        DataBase.GetLevels(); // 레벨 데이터 가져오기
        slider.maxValue = DataBase.valueMaxWater[DataBase.tankLevel]; // maxvalue = 현재 레벨의 빗물 최대 양
        slider.minValue = 0f; // min = 0 


        //TODO : move to main UI script
        // try
        // {
        //     Debug.Log(DataBase.valuePotMax[DataBase.nowLocal]);
        //     slider[1].maxValue = DataBase.valuePotMax[DataBase.nowLocal];
        //     slider[1].minValue = 0f;
        // }
        // catch (Exception e)
        // {
        // }
    }

    // 물탱크 value 변경
    public void waterTankUpdate(Slider slider)
    {
        DataBase.GetWaterData(); // 빗물 데이터 가져오기
        slider.value = DataBase.AllWater(); //  value = 빗물 전체 합
    }

    // 현재 돈 text set
    public void moneySet(Text text)
    {
        DataBase.GetMoney(); // 돈 데이터 가져오기
        text.text = Convert.ToString(DataBase.money) + " $"; // text set
    }

    //Scene 이동 
    public void moveScene(string val)
    {
        SceneManager.LoadScene(val);
    }
    
    
}