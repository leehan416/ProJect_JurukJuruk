/* UI 컨트롤 스크립트 */

using System;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    public static UIManager instance;

    public Text[] text = new Text[3]; // 0 = money 1 = local 
    public Slider[] slider = new Slider[3]; // 0 = waterTank or BgmVol 1 = Fx Vol
    [Header("메인 맵 배경")] public Sprite[] localBG = new Sprite[4];

    void Start()
    {
        if (!instance) instance = this;
        else DestroyImmediate(this);
        //TODO scene에 맞춰서 작업할것.


        try
        {
            //--------------------------------------------------------
            //main
            text[0] = GameObject.Find("Canvas/MoneyBack/Money").GetComponent<Text>(); // money
            text[1] = GameObject.Find("Canvas/LocalBack/Local").GetComponent<Text>(); // Local

            slider[0] = GameObject.Find("Canvas/Tank").GetComponent<Slider>(); // waterTank

            if (DataBase.potLevel[DataBase.nowLocal] == 0)
                GameObject.Find("Canvas/BigBox/EmptyExtraBottle").SetActive(false);
            //--------------------------------------------------------
            DataBase.GetWaterData();
            WaterTankSet();
            WaterTankUpdate();
            MoneySet();
            LocalSet();
            BackGroundSet();
            //--------------------------------------------------------
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        // clean
        //TODO per click text set

        //--------------------------------------------------------
        // Shop
        text[0] = GameObject.Find("Canvas/MoneyBack/Money").GetComponent<Text>(); // money
        slider[0] = GameObject.Find("Canvas/Tank").GetComponent<Slider>(); // waterTank
        MoneySet();
        DataBase.GetWaterData();
        WaterTankSet();
        WaterTankUpdate();
    }

    //--------------------------------------------------------
    //main

    #region main

    public void WaterTankUpdate()
    {
        // 물탱크 변수 변경
        slider[0].value = DataBase.AllWater();
        DataBase.SetWaterData(); // 데이터베이스 세팅
        Debug.Log(DataBase.AllWater());
    }

    public void WaterTankSet()
    {
        // 물탱크 초기 세팅
        slider[0].maxValue = Convert.ToInt64(PlayerPrefs.GetString("MaxWater", "10000"));
        slider[0].minValue = 0f;
    }

    public void EmptyWaterTank()
    {
        // 초당 물 얻는 양동이 비우기
        // 청정구역이라면 
        if (DataBase.nowLocal == 1) DataBase.cleanedWater += DataBase.potWater[DataBase.nowLocal];
        // 사막지역
        else if (DataBase.nowLocal == 3) DataBase.desertWater += DataBase.potWater[DataBase.nowLocal];
        // 나머지 지역
        else DataBase.uncleanedWater += DataBase.potWater[DataBase.nowLocal];
        // 물병 비우기

        if (DataBase.AllWater() > DataBase.maxWater)
        {
            if (DataBase.nowLocal == 1)
            {
                DataBase.cleanedWater -= DataBase.AllWater() - DataBase.maxWater;
            }
            else if (DataBase.nowLocal == 3)
            {
                DataBase.desertWater -= DataBase.AllWater() - DataBase.maxWater;
            }
            else
            {DataBase.uncleanedWater -=  DataBase.AllWater() - DataBase.maxWater;
                
            }
        }

        DataBase.potWater[DataBase.nowLocal] = 0;
        WaterTankUpdate();
        DataBase.SetLateTime();
    }

    public void MoneySet()
    {
        // 현재 돈 
        DataBase.money = Convert.ToInt64(PlayerPrefs.GetString("Money", "0"));
        text[0].text = Convert.ToString(DataBase.money) + " $";
    }

    public void LocalSet()
    {
        // 현위치 텍스트 변경
        text[1].text = DataBase.localName[DataBase.nowLocal];
    }


    public void BackGroundSet()
    {
        Image bg = GameObject.Find("Canvas/BackGround").GetComponent<Image>();
        bg.sprite = localBG[DataBase.nowLocal];
    }

    #endregion

    //--------------------------------------------------------
    //map + moveScene

    #region map_moveScene

    public void MoveScene(string val)
    {
        SceneManager.LoadScene(val);
    }

    public void MoveLocal(int val)
    {
        PlayerPrefs.SetInt("NowLocal", val);
        DataBase.nowLocal = val;
        MoveScene("Main");
    }

    #endregion

    //--------------------------------------------------------
    //shop

    #region shop

    public void Sell(int index)
    {
        // 물판매
        if (index == 0)
        {
            Debug.Log("!");
            if (DataBase.uncleanedWater + DataBase.cleanedWater + DataBase.desertWater > 1000)
            {
                DataBase.money += 10; //Consumer.consumerList[index].perLiter;
                DataBase.uncleanedWater -= 1000;
                //TODO 물 더러운 물 => 깨끗한 물 => 사막 물 순서로 빠지게 만들기
                PlayerPrefs.SetString("Money", System.Convert.ToString(DataBase.money));
            }
            else
            {
                return;
                //물 부족
            }
        }
        else if (index == 3)
        {
            if (DataBase.desertWater > 1000)
            {
                DataBase.money += Consumer.consumerList[index].perLiter;
                DataBase.desertWater -= 1000;
            }
            else
            {
                return;
                //돈 부족
            }
        }
        else
        {
            if (DataBase.cleanedWater > 1000)
            {
                DataBase.money += Consumer.consumerList[index].perLiter;
                DataBase.cleanedWater -= 1000;
            }
            else
            {
                return;
                //돈 부족
            }
        }

        MoneySet();
        WaterTankUpdate();
    }

    #endregion

    //--------------------------------------------------------
    //Setting
    //TODO 1. Slider Setting 2. toggle set
    //
    // public bool Drag { get; set; }
    //
    // public void ChangeBgmVol()
    // {
    //     if (!Drag)
    //     {
    //     }
    // }
    //
    // public void ChangeFxVol()
    // {
    //     if (!Drag)
    //     {
    //     }
    // }


    //--------------------------------------------------------
    //Cleaning
    //TODO Cleaning system set, cleaning up system set.
    public void ClickClean()
    {
        if (DataBase.uncleanedWater < DataBase.perclean && DataBase.uncleanedWater > 0)
        {
            if (DataBase.uncleanedWater < 0)
            {
                DataBase.uncleanedWater = 0;
                return;
            }

            DataBase.cleanedWater += DataBase.uncleanedWater;
            DataBase.uncleanedWater = 0;
        }
        else
        {
            if (DataBase.uncleanedWater <= 0)
            {
                DataBase.uncleanedWater = 0;
                return;
            }

            DataBase.uncleanedWater -= DataBase.perclean;
            DataBase.cleanedWater += DataBase.perclean;
        }
    }

    public void UpCleanLevel()
    {
        if (DataBase.money < DataBase.upgradeClean[DataBase.cleanLevel])
        {
            //up
        }
        else
        {
            // 돈부족
        }
    }
    //--------------------------------------------------------

    //Market

    //TODO 1. pot up System set, 2. extra pot up system, 3. water tank system set
}