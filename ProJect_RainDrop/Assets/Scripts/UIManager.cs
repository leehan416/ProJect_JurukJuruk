﻿/* UI 컨트롤 스크립트 */

using System;
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


    private void Start()
    {
        if (!instance) instance = this;
        else DestroyImmediate(this);
        money = GameObject.Find("Canvas/Money").GetComponent<Text>();
        local = GameObject.Find("Canvas/Local").GetComponent<Text>();
        waterTank = GameObject.Find("Canvas/Tank").GetComponent<Slider>();
        WaterTankSet();
        WaterTankUpdate();
    }
    //--------------------------------------------------------
    //main
    public void WaterTankUpdate()
    {
        waterTank.value = DataBase.savedWater;
        //ToDo : 사막지역에서 사막 물로 변경할것.
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
        local.text = Convert.ToString(DataBase.nowLocal);
    }

    public void MoveScene(string val)
    {
        SceneManager.LoadScene(val);
    }

    public void BackGroundSet()
    {
        Image bg = GameObject.Find("Canvas/BackGround").GetComponent<Image>();
        bg.sprite = localBG[DataBase.nowLocal];
    }


    //--------------------------------------------------------
    //map
    public void MoveLocal(int val)
    {
        PlayerPrefs.SetInt("NowLocal", val);
        MoveScene("Main");
    }
    
    
    //--------------------------------------------------------
    //shop
    public void Sell()
    {
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
    }



}