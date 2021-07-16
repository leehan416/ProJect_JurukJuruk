/* UI 컨트롤 스크립트 */

using System;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    public static UIManager instance;

    public Text money;
    public Text local;
    public Slider waterTank;

    public Sprite[] localBG = new Sprite[4];


    void Start()
    {
        if (!instance) instance = this;
        else DestroyImmediate(this);
        //TODO scene에 맞춰서 작업할것.
        money = GameObject.Find("Canvas/MoneyBack/Money").GetComponent<Text>();
        local = GameObject.Find("Canvas/LocalBack/Local").GetComponent<Text>();
        waterTank = GameObject.Find("Canvas/Tank").GetComponent<Slider>();
        WaterTankSet();
        WaterTankUpdate();
        MoneySet();
        LocalSet();
        BackGroundSet();
        
    }

    //--------------------------------------------------------
    //main

    #region main

    public void WaterTankUpdate()
    {
        waterTank.value = DataBase.savedWater;
        //TODO : 사막지역에서 사막 물로 변경할것.
    }

    public void WaterTankSet()
    {
        waterTank.maxValue = DataBase.maxWater;
        waterTank.minValue = 0f;
    }

    public void MoneySet()
    {
        money.text = Convert.ToString(DataBase.money);
    }

    public void LocalSet()
    {
        local.text = DataBase.localName[DataBase.nowLocal];
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
        MoveScene("Main");
    }

    #endregion

    //--------------------------------------------------------
    //shop

    #region shop

    public void Sell(int index)
    {
        // TODO need cleaned water setting 
        if (DataBase.savedWater < 1000 && Consumer.consumerList[index].isCleaned)
        {
            // 물 부족
            return;
        }
        else
        {
            DataBase.money += Consumer.consumerList[index].perLiter;
        }
    }

    #endregion

    //--------------------------------------------------------
    //Setting
    //TODO 1. Slider Setting 2. toggle set
    //--------------------------------------------------------
    //Cleaning
    //TODO Cleaning system set, cleaning up system set.
    //--------------------------------------------------------
    //Market
    //TODO 1. pot up System set, 2. extra pot up system, 3. water tank system set
}