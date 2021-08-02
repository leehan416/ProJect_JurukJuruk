/* UI 컨트롤 스크립트 */

using System;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    public static UIManager instance;

    [HideInInspector] public Text[] text = new Text[10]; // 0 = money 1 = local 
    [HideInInspector] public Slider[] slider = new Slider[3]; // 0 = waterTank or BgmVol 1 = Fx Vol
    [HideInInspector] public Toggle toggle;
    [HideInInspector] public GameObject[] locker = new GameObject[4];

    [Header("메인 맵 배경")] public Sprite[] localBG = new Sprite[4];
    //--------------------------------------------------------

    void Start()
    {
        if (!instance) instance = this;
        else DestroyImmediate(this);
        //--------------------------------------------------------
        //main
        try
        {
            text[0] = GameObject.Find("Canvas/MoneyBack/Money").GetComponent<Text>(); // money
            text[1] = GameObject.Find("Canvas/LocalBack/Local").GetComponent<Text>(); // Local
            text[2] = GameObject.Find("Canvas/Tank/ShowAmount/ShowAmount_Basic").GetComponent<Text>();
            text[3] = GameObject.Find("Canvas/Tank/ShowAmount/ShowAmount_Clean").GetComponent<Text>();
            text[4] = GameObject.Find("Canvas/Tank/ShowAmount/ShowAmount_Desert").GetComponent<Text>();


            for (int i = 0; i < 4; i++)
                locker[i] = GameObject.Find("Canvas/BigBox/PotSlider" + i);

            for (int i = 0; i < 4; i++)
                if (DataBase.nowLocal != i)
                    locker[i].SetActive(false);


            slider[0] = GameObject.Find("Canvas/Tank").GetComponent<Slider>(); // waterTank
            slider[1] = GameObject.Find("Canvas/BigBox/PotSlider" + DataBase.nowLocal + "/mask/Slider")
                .GetComponent<Slider>(); // PotTank

            //locker[0] =  // PotTank


            // Debug.Log(++DataBase.potLevel[DataBase.nowLocal]);
            // DataBase.GetLevels();
            // DataBase.potLevel[0] = 0;
            // DataBase.SetLevels();
            if (DataBase.potLevel[DataBase.nowLocal] == 0)
            {
                GameObject.Find("Canvas/BigBox/EmptyExtraBottle").SetActive(false);
                GameObject.Find("Canvas/BigBox/PotSlider" + DataBase.nowLocal).SetActive(false);
            }

            DataBase.GetWaterData();
            WaterTankSet();
            WaterTankUpdate();
            MoneySet();
            LocalSet();
            BackGroundSet();
            SetMainText();
            PotUpdate();
            return;
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        //--------------------------------------------------------
        //intro

        try
        {
        }
        catch (Exception e)
        {
        }


        //--------------------------------------------------------
        // clean
        //TODO per click text set

        try
        {
            text[5] = GameObject.Find("Canvas/Explain_Memo/Recent").GetComponent<Text>();
            text[6] = GameObject.Find("Canvas/Up/Text").GetComponent<Text>();

            text[0] = GameObject.Find("Canvas/MoneyBack/Money").GetComponent<Text>(); // money
            text[2] = GameObject.Find("Canvas/Tank/ShowAmount/ShowAmount_Basic").GetComponent<Text>();
            text[3] = GameObject.Find("Canvas/Tank/ShowAmount/ShowAmount_Clean").GetComponent<Text>();
            text[4] = GameObject.Find("Canvas/Tank/ShowAmount/ShowAmount_Desert").GetComponent<Text>();

            slider[0] = GameObject.Find("Canvas/Tank").GetComponent<Slider>(); // waterTank

            DataBase.GetMoney();
            DataBase.GetWaterData();
            DataBase.GetLevels();

            SetCleanText();
            MoneySet();
            WaterTankSet();
            WaterTankUpdate();
            SetMainText();
            return;
        }
        catch (Exception e)
        {
        }
        //--------------------------------------------------------
        // Shop

        try
        {
            text[0] = GameObject.Find("Canvas/MoneyBack/Money").GetComponent<Text>(); // money
            text[2] = GameObject.Find("Canvas/Tank/ShowAmount/ShowAmount_Basic").GetComponent<Text>();
            text[3] = GameObject.Find("Canvas/Tank/ShowAmount/ShowAmount_Clean").GetComponent<Text>();
            text[4] = GameObject.Find("Canvas/Tank/ShowAmount/ShowAmount_Desert").GetComponent<Text>();

            slider[0] = GameObject.Find("Canvas/Tank").GetComponent<Slider>(); // waterTank

            //
            // DataBase.money = 0; // 10000000;
            // DataBase.uncleanedWater = 10000; //DataBase.valueMaxWater[DataBase.tankLevel];
            // DataBase.cleanedWater = 10000;
            // DataBase.desertWater = 10000;
            // // DataBase.tankLevel = 10000;
            // //DataBase.pailLevel = 0;
            // DataBase.SetLevels();
            // DataBase.SetMoney();
            // DataBase.SetWaterData();
            //

            DataBase.GetMoney();
            DataBase.GetWaterData();
            Debug.Log(DataBase.AllWater());
            MoneySet();
            WaterTankSet();
            WaterTankUpdate();
            SetMainText();
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
            return;
        }
        catch (Exception e)
        {
        }

        //--------------------------------------------------------
        //setting
        try
        {
            slider[0] = GameObject.Find("Canvas/Setting_bg/BgmSlider").GetComponent<Slider>();
            slider[1] = GameObject.Find("Canvas/Setting_bg/FxSlider").GetComponent<Slider>();
            toggle = GameObject.Find("Canvas/Setting_bg/ControllerTogle").GetComponent<Toggle>();
            SetSettingObj();
            return;
        }
        catch (Exception e)
        {
        }

        //--------------------------------------------------------
        // Market
        try
        {
            text[0] = GameObject.Find("Canvas/MoneyBack/Money").GetComponent<Text>(); // money
            text[1] = GameObject.Find("Canvas/Goods/Pail_BG/Info").GetComponent<Text>(); // 정보
            text[2] = GameObject.Find("Canvas/Goods/Pail_BG/PailUp/Text").GetComponent<Text>(); // 가격
            text[3] = GameObject.Find("Canvas/Goods/Tank_BG/Info").GetComponent<Text>(); // 정보
            text[4] = GameObject.Find("Canvas/Goods/Tank_BG/TankUp/Text").GetComponent<Text>(); // 가격

            for (int i = 5; i < 9; i++)
                text[i] = GameObject.Find("Canvas/Goods/Pot_BG/Pot_" + (i - 5) + "/Text").GetComponent<Text>();


            SetMarketLockers();

            DataBase.GetMoney();
            DataBase.GetWaterData();
            SetMarketText();
            MoneySet();
            return;
        }
        catch (Exception e)
        {
        }
        //--------------------------------------------------------
    }

    //--------------------------------------------------------
    //main

    #region main

    public void SetMainText()
    {
        DataBase.GetWaterData();
        text[2].text = DataBase.uncleanedWater.ToString();
        text[3].text = DataBase.cleanedWater.ToString();
        text[4].text = DataBase.desertWater.ToString();
    }

    public void PotUpdate()
    {
        DataBase.GetWaterData();
        slider[1].value = DataBase.potWater[DataBase.nowLocal];
        DataBase.SetWaterData(); // 데이터베이스 세팅
    }

    public void WaterTankUpdate()
    {
        // 물탱크 변수 변경

        DataBase.GetWaterData();
        slider[0].value = DataBase.AllWater();
    }

    public void WaterTankSet()
    {
        DataBase.GetWaterData();
        DataBase.GetLevels();
        // 물탱크 초기 세팅
        slider[0].maxValue = DataBase.valueMaxWater[DataBase.tankLevel]; //DataBase.maxWater;
        slider[0].minValue = 0f;

        try
        {
            slider[1].maxValue = DataBase.potMax[DataBase.nowLocal];
            slider[1].minValue = 0f;
        }
        catch (Exception e)
        {
        }
    }

    public void EmptyWaterTank()
    {
        if (DataBase.valueMaxWater[DataBase.tankLevel] <= DataBase.AllWater())
        {
            return;
        }

        DataBase.GetWaterData();

        // 초당 물 얻는 양동이 비우기
        // 청정구역이라면 
        if (DataBase.nowLocal == 1) DataBase.cleanedWater += DataBase.potWater[DataBase.nowLocal];
        // 사막지역
        else if (DataBase.nowLocal == 3) DataBase.desertWater += DataBase.potWater[DataBase.nowLocal];
        // 나머지 지역
        else DataBase.uncleanedWater += DataBase.potWater[DataBase.nowLocal];

        // 물병 비우기
        if (DataBase.AllWater() > DataBase.valueMaxWater[DataBase.tankLevel])
        {
            if (DataBase.nowLocal == 1)
            {
                DataBase.cleanedWater -= DataBase.AllWater() - DataBase.valueMaxWater[DataBase.tankLevel];
            }
            else if (DataBase.nowLocal == 3)
            {
                DataBase.desertWater -= DataBase.AllWater() - DataBase.valueMaxWater[DataBase.tankLevel];
            }
            else
            {
                DataBase.uncleanedWater -= DataBase.AllWater() - DataBase.valueMaxWater[DataBase.tankLevel];
            }
        }

        DataBase.potWater[DataBase.nowLocal] = 0;
        DataBase.SetLateTime();
        DataBase.SetWaterData();
        WaterTankUpdate();
        PotUpdate();
        SetMainText();
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

    public void IntroMoveScene()
    {
        SoundManager.instance.bgmSource.clip = SoundManager.instance.musics[0];
        SoundManager.instance.bgmSource.Play();
        MoveScene("Main");
    }

    #endregion

    //--------------------------------------------------------
    //shop

    #region shop

    public void Sell(int index)
    {
        // 물판매

        DataBase.GetWaterData();
        DataBase.GetMoney();

        if (index == 0)
        {
            if (DataBase.AllWater() >= 1000)
            {
                if (DataBase.uncleanedWater >= 1000)
                {
                    DataBase.uncleanedWater -= 1000;
                }
                else if (DataBase.cleanedWater >= 1000)
                {
                    DataBase.cleanedWater -= 1000;
                }
                else if (DataBase.desertWater >= 1000)
                {
                    DataBase.desertWater -= 1000;
                }
                else
                {
                    return;
                    //물없음
                }

                DataBase.money += DataBase.consumerList[index].perLiter;

                //TODO 물 더러운 물 => 깨끗한 물 => 사막 물 순서로 빠지게 만들기
            }
            else
            {
                return;
                //물 부족
            }
        }
        else if (index == 1)
        {
            if (DataBase.cleanedWater >= 1000)
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
        else if (index == 2)
        {
            if (DataBase.desertWater >= 1000)
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


        DataBase.SetWaterData();
        DataBase.SetMoney();
        SetMainText();
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
        DataBase.GetLevels();
        DataBase.GetWaterData();
        if (DataBase.uncleanedWater < DataBase.valueCleanWater[DataBase.cleanLevel])
        {
            DataBase.cleanedWater += DataBase.uncleanedWater;
            DataBase.uncleanedWater = 0;
        }
        else
        {
            if (DataBase.uncleanedWater == 0)
            {
                return;
            }

            DataBase.uncleanedWater -= DataBase.valueCleanWater[DataBase.cleanLevel];
            DataBase.cleanedWater += DataBase.valueCleanWater[DataBase.cleanLevel];
        }

        DataBase.SetWaterData();

        SetMainText();
    }

    public void SetCleanText()
    {
        DataBase.GetLevels();
        DataBase.GetWaterData();
        if (DataBase.cleanLevel != DataBase.valueCleanWater.Length - 1)
        {
            text[5].text = "현재 :  1터치 = " + DataBase.valueCleanWater[DataBase.cleanLevel] + "ml\n" + "업그레이드 :  1터치 = " +
                           DataBase.valueCleanWater[DataBase.cleanLevel + 1] + "ml";
            text[6].text = DataBase.upgradeClean[DataBase.cleanLevel + 1] + "$";
        }
        else
        {
            text[5].text = "현재 :  1터치 = " + DataBase.valueCleanWater[DataBase.cleanLevel] + "ml\n" + "최고 레벨입니다.";
            text[6].text = "MAX";
        }
    }

    public void UpCleanLevel()
    {
        DataBase.GetMoney();
        DataBase.GetLevels();
        try
        {
            if (DataBase.money > DataBase.upgradeClean[DataBase.cleanLevel] &&
                DataBase.cleanLevel < DataBase.valueCleanWater.Length)
            {
                DataBase.GetLevels();
                DataBase.money -= DataBase.upgradeClean[++DataBase.cleanLevel];
                DataBase.SetLevels();
                DataBase.SetMoney();
                SetCleanText();
                MoneySet();
            }
            else
            {
                // 돈부족
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    #endregion

    //--------------------------------------------------------
    //Market

    #region Market

    //TODO TextSet

    public void SetMarketLockers()
    {
        for (int i = 0; i < 4; i++)
            if (DataBase.potLevel[i] > 0)
                GameObject.Find("Canvas/Goods/Pot_BG/Pot_" + i + "/Lock").gameObject.SetActive(false);
    }

    public void SetMarketText()
    {
        // DataBase.GetWaterData();
        // DataBase.GetLevels();
        //TODO TRY CATCH => MAX LEVEL

        if (DataBase.pailLevel != DataBase.valuePerDrop.Length - 1)
        {
            text[1].text =
                "업그레이드시\n한 물방울 = " + DataBase.valuePerDrop[DataBase.pailLevel] + "ml >> " +
                DataBase.valuePerDrop[DataBase.pailLevel + 1] + "ml";
            text[2].text = DataBase.upgradePail[DataBase.pailLevel + 1] + " $";
        }
        else
        {
            text[1].text = "한계에 도달했습니다.\n" + "한 물방울 =" + DataBase.valuePerDrop[DataBase.pailLevel] + "ml";
            text[2].text = "Max";
        }


        if (DataBase.tankLevel != DataBase.valueMaxWater.Length - 1)
        {
            text[3].text = "업그레이드시\n" + DataBase.valueMaxWater[DataBase.tankLevel] + "L >> " +
                           DataBase.valueMaxWater[DataBase.tankLevel + 1] + "L";
            text[4].text = DataBase.upgradeTank[DataBase.tankLevel + 1] + " $";
        }
        else
        {
            text[3].text = "한계에 도달했습니다.\n" + DataBase.valueMaxWater[DataBase.tankLevel] + "L";
            text[4].text = "Max";
        }

        for (int i = 0; i < 4; i++)
        {
            if (DataBase.potLevel[i] != DataBase.valuePotMax.Length - 1)
                text[i + 5].text = DataBase.unLockPot[i] + DataBase.upgradePail[DataBase.potLevel[i]] + "$";
            else
                text[i + 5].text = "Max";
        }
    }

    public void UpPailLevel()
    {
        DataBase.GetLevels();
        DataBase.GetMoney();
        if (DataBase.money > DataBase.upgradePail[DataBase.pailLevel + 1])
        {
            DataBase.money -= DataBase.upgradePail[++DataBase.pailLevel];
            DataBase.SetLevels();
            DataBase.SetMoney();
            SetMarketText();
            MoneySet();
        }
        else
        {
            //돈부족 
        }
    }

    public void UpTankLevel()
    {
        DataBase.GetLevels();
        DataBase.GetMoney();
        if (DataBase.money > DataBase.upgradeTank[DataBase.tankLevel + 1])
        {
            DataBase.money -= DataBase.upgradeTank[++DataBase.tankLevel];
            DataBase.SetLevels();
            DataBase.SetMoney();
            SetMarketText();
            MoneySet();
        }
        else
        {
            //돈부족 
        }
        //
        // DataBase.SetLevels();
        // DataBase.SetMoney();
    }

    public void UpPotLevel(int val)
    {
        DataBase.GetLevels();
        DataBase.GetMoney();
        if (DataBase.potLevel[val] == 0)
        {
            if (DataBase.money >= DataBase.unLockPot[val])
                DataBase.money -= DataBase.unLockPot[val] + DataBase.upgradePot[++DataBase.potLevel[val] + 1];
        }
        else
        {
            if (DataBase.money >= (DataBase.unLockPot[val] + DataBase.upgradePot[DataBase.potLevel[val] + 1]))
            {
                DataBase.money -= (DataBase.unLockPot[val] + DataBase.upgradePot[++DataBase.potLevel[val]]);
                Debug.Log("!");
                DataBase.SetLevels();
                DataBase.SetMoney();
                SetMarketText();
                MoneySet();
                SetMarketLockers();
            }
            else
            {
                //돈부족 
            }
        }
        //
        // DataBase.SetLevels();
        // DataBase.SetMoney();
    }

    #endregion
}