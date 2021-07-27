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
    public Toggle toggle;
    public GameObject[] locker = new GameObject[4];
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
            return;
            //--------------------------------------------------------
        }
        catch (Exception e)
        {
        }
        // clean
        //TODO per click text set

        //--------------------------------------------------------
        try
        {
            // Shop
            text[0] = GameObject.Find("Canvas/MoneyBack/Money").GetComponent<Text>(); // money
            slider[0] = GameObject.Find("Canvas/Tank").GetComponent<Slider>(); // waterTank
            MoneySet();
            DataBase.GetWaterData();
            WaterTankSet();
            WaterTankUpdate();
            return;
        }
        catch (Exception e)
        {
        }

        //--------------------------------------------------------
        //map
        try
        {
            locker[1] = GameObject.Find("Canvas/List/Countryside/lock").gameObject;
            locker[2] = GameObject.Find("Canvas/List/Amazon/lock").gameObject;
            locker[3] = GameObject.Find("Canvas/List/Desert/lock").gameObject;

            for (int i = 1; i < DataBase.local.Length; i++)
                locker[i].SetActive(DataBase.local[i].isLock);
        }
        catch (Exception e)
        {
            return;
        }

        //--------------------------------------------------------
        try
        {
            //setting
            slider[0] = GameObject.Find("Canvas/Setting_bg/BgmSlider").GetComponent<Slider>();
            slider[1] = GameObject.Find("Canvas/Setting_bg/FxSlider").GetComponent<Slider>();
            toggle = GameObject.Find("Canvas/Setting_bg/ControllerTogle").GetComponent<Toggle>();
            SetSettingObj();
        }
        catch (Exception e)
        {
            return;
        }
        //--------------------------------------------------------
    }

    //--------------------------------------------------------
    //main

    #region main

    public void WaterTankUpdate()
    {
        // 물탱크 변수 변경
        slider[0].value = DataBase.AllWater();
        DataBase.SetWaterData(); // 데이터베이스 세팅
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
            {
                DataBase.uncleanedWater -= DataBase.AllWater() - DataBase.maxWater;
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


    public void UnLockLocal(int val)
    {
        if (DataBase.money > DataBase.local[val].cost)
        {
            DataBase.money -= DataBase.local[val].cost;
            DataBase.SetMoney();
        }
        else
        {
            // 돈부족
        }
    }

    public void MoveLocal(int val)
    {
        Debug.Log(val);
        if (DataBase.local[val].isLock)
        {
            UnLockLocal(val);
        }
        else
        {
            PlayerPrefs.SetInt("NowLocal", val);
            DataBase.nowLocal = val;
            MoveScene("Main");
        }
    }

    #endregion

    //--------------------------------------------------------
    //shop

    #region shop

    public void SetShopText()
    {
    }

    public void Sell(int index)
    {
        // 물판매
        if (index == 0)
        {
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
                DataBase.money += DataBase.consumerList[index].perLiter;
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
                DataBase.money += DataBase.consumerList[index].perLiter;
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

    #region Setting

    public void SetSettingObj()
    {
        DataBase.GetSettingVal();
        //DataBase.fxVol = .7f;
        slider[0].value = DataBase.bgmVol;
        slider[1].value = DataBase.fxVol;
        toggle.isOn = DataBase.isReverse;
    }

    public void ChangeBgmVol(float val)
    {
        SoundManager.instance.bgmSource.volume = val;
        DataBase.bgmVol = val;
        DataBase.SetSettingVal();
    }

    public void ChangeFxVol(float val)
    {
        SoundManager.instance.fxSource.volume = val;
        DataBase.fxVol = val;
        DataBase.SetSettingVal();
    }

    public void ChangeControllReverse(bool val)
    {
        DataBase.isReverse = val;
        DataBase.SetSettingVal();
    }

    #endregion

    //--------------------------------------------------------
    //Cleaning
    //TODO cleaning up system set.

    #region Cleaning

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

        DataBase.SetWaterData();
    }

    public void SetCleanText()
    {
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

    #endregion

    //--------------------------------------------------------

    //Market

    //TODO 1. pot up System set, 2. extra pot up system, 3. water tank system set
}