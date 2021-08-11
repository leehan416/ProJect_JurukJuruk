using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainScene : MonoBehaviour {
    Text money; //money Text
    Text local; // local Text

    Text[] waterCounter = new Text[3]; // waterCounter Text

    Slider waterTank; // 물탱크
    Slider waterPot; // 물탱크

    // 배경 sprite 
    [Header("메인 맵 배경")] public Sprite[] localBG = new Sprite[4];

    void Start()
    {
        //Get UI
        money = GameObject.Find("Canvas/MoneyBack/Money").GetComponent<Text>(); // money
        waterCounter[0] = GameObject.Find("Canvas/LocalBack/Local").GetComponent<Text>();
        waterCounter[1] = GameObject.Find("Canvas/Tank/ShowAmount/ShowAmount_Basic").GetComponent<Text>();
        waterCounter[2] = GameObject.Find("Canvas/Tank/ShowAmount/ShowAmount_Clean").GetComponent<Text>();
        waterTank = GameObject.Find("Canvas/Tank").GetComponent<Slider>(); // waterTank
        waterPot = GameObject.Find("Canvas/BigBox/PotSlider" + DataBase.nowLocal + "/mask/Slider")
            .GetComponent<Slider>(); // PotTank

        //현 지역 전용 pot 이외엔 모두 비활성화
        for (int i = 0; i < 4; i++)
            if (DataBase.nowLocal != i)
                GameObject.Find("Canvas/BigBox/PotSlider" + i).SetActive(false);

        //현 지역의 pot의 레벨이 0이라면 비활성화
        if (DataBase.potLevel[DataBase.nowLocal] == 0)
        {
            GameObject.Find("Canvas/BigBox/EmptyExtraBottle").SetActive(false);
            GameObject.Find("Canvas/BigBox/PotSlider" + DataBase.nowLocal).SetActive(false);
        }

        //dataGet
        DataBase.GetWaterData();
        DataBase.GetLevels();
        DataBase.GetMoney();

        //sliderSet
        UI_MultiScene.setWaterTank(waterTank);
        setWaterPot();

        //TextSet
        setLocal();
        UI_MultiScene.setMoney(money);

        setbackGround();
        UI_MultiScene.setWaterCounter(waterCounter);

        //slider update
        updateWaterPot();
        UI_MultiScene.UpdateWaterTank(waterTank);
    }

    // public void setMainText()
    // {
    //     DataBase.GetWaterData();
    //     waterCounter[0].text = DataBase.uncleanedWater.ToString();
    //     waterCounter[1].text = DataBase.cleanedWater.ToString();
    //     waterCounter[2].text = DataBase.desertWater.ToString();
    // }
    // pot (추가 양동이) value set
    public void updateWaterPot()
    {
        DataBase.GetWaterData();
        waterPot.value = DataBase.potWater[DataBase.nowLocal];
    }

    // public void waterTankUpdate()
    // {
    //     // 물탱크 변수 변경
    //     DataBase.GetWaterData();
    //     waterTank.value = DataBase.AllWater();
    // }

    // pot (추가 양동이) 초기 데이터 설정
    public void setWaterPot()
    {
        DataBase.GetWaterData();
        DataBase.GetLevels();
        waterPot.maxValue = DataBase.valuePotMax[DataBase.nowLocal]; // 촤댓값
        waterPot.minValue = 0f; // 최솟값
    }

    //pot (추가 양동이) 비우기
    public void emptyPot()
    {
        DataBase.GetWaterData();
        DataBase.GetLevels();

        //물탱크가 최대라면
        if (DataBase.valueMaxWater[DataBase.tankLevel] <= DataBase.AllWater())
        {
            return;
        }

        // 청정구역이라면 
        if (DataBase.nowLocal == 1) DataBase.cleanedWater += DataBase.potWater[DataBase.nowLocal];
        // 사막지역
        else if (DataBase.nowLocal == 3) DataBase.desertWater += DataBase.potWater[DataBase.nowLocal];
        // 나머지 지역
        else DataBase.uncleanedWater += DataBase.potWater[DataBase.nowLocal];

        // 물병 비우기
        if (DataBase.AllWater() > DataBase.valueMaxWater[DataBase.tankLevel])
        {
            // clean local
            if (DataBase.nowLocal == 1)
            {
                DataBase.cleanedWater -= DataBase.AllWater() - DataBase.valueMaxWater[DataBase.tankLevel];
            }
            //dessert local
            else if (DataBase.nowLocal == 3)
            {
                DataBase.desertWater -= DataBase.AllWater() - DataBase.valueMaxWater[DataBase.tankLevel];
            }
            // normal local
            else
            {
                DataBase.uncleanedWater -= DataBase.AllWater() - DataBase.valueMaxWater[DataBase.tankLevel];
            }
        }

        // value reset
        DataBase.potWater[DataBase.nowLocal] = 0;

        //data set
        DataBase.SetLateTime();
        DataBase.SetWaterData();

        //ui update
        updateWaterPot();
        UI_MultiScene.UpdateWaterTank(waterTank);
        UI_MultiScene.setWaterCounter(waterCounter);
    }

    // public void moneySet()
    // {
    //     // 현재 돈 
    //
    //     //DataBase.money = Convert.ToInt64(PlayerPrefs.GetString("Money", "0"));
    //     DataBase.GetMoney();
    //     money.text = Convert.ToString(DataBase.money) + " $";
    // }


    // 지역 text set
    public void setLocal()
    {
        local.text = DataBase.localName[DataBase.nowLocal];
    }

    // 배경 set
    public void setbackGround()
    {
        Image bg = GameObject.Find("Canvas/BackGround").GetComponent<Image>();
        bg.sprite = localBG[DataBase.nowLocal];
    }
}