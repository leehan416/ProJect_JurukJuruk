﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_CleanScene : MonoBehaviour {
    private Text money; // money texst
    private Text[] waterCounter = new Text[3]; //waterCounter Text
    private Button clickZone; // CleanBtn

    private Slider waterTank; // 물탱크

    private Text upText; // 업그레이드 가격 Text

    private Text explain; // 현재, 다음 업그레이드 설명 Text 
    [Header("클린 스프라이트")] public Sprite[] cleanFx = new Sprite[2]; // click FX


    void Start()
    {
        //UI Set
        money = GameObject.Find("Canvas/MoneyBack/Money").GetComponent<Text>();

        waterCounter[0] = GameObject.Find("Canvas/Tank/ShowAmount/ShowAmount_Basic").GetComponent<Text>();
        waterCounter[1] = GameObject.Find("Canvas/Tank/ShowAmount/ShowAmount_Clean").GetComponent<Text>();
        waterCounter[2] = GameObject.Find("Canvas/Tank/ShowAmount/ShowAmount_Desert").GetComponent<Text>();

        explain = GameObject.Find("Canvas/Explain_Memo/Recent").GetComponent<Text>();
        upText = GameObject.Find("Canvas/Up/Text").GetComponent<Text>();

        waterTank = GameObject.Find("Canvas/Tank").GetComponent<Slider>();
        clickZone = GameObject.Find("Canvas/ClickZone").GetComponent<Button>();

        // 버튼 이벤트 트리거 생성.
        EventTrigger trgY = clickZone.gameObject.AddComponent<EventTrigger>();
        // hold, up
        EventTrigger.Entry enBH = new EventTrigger.Entry();
        EventTrigger.Entry enBU = new EventTrigger.Entry();

        // 각 행동 등록 
        // PointerDown : 클릭시 | PointerUp : 클릭 종료시
        enBH.eventID = EventTriggerType.PointerDown;
        enBU.eventID = EventTriggerType.PointerUp;


        //TODO : Level Value Set

        // 각 entry에 함수 등록
        enBH.callback.AddListener(delegate
        {
            GameObject.Find("Canvas/ClickZone/onClick").GetComponent<Image>().sprite = cleanFx[1];
        });
        enBU.callback.AddListener(delegate
        {
            GameObject.Find("Canvas/ClickZone/onClick").GetComponent<Image>().sprite = cleanFx[0];
        });
        trgY.triggers.Add(enBH);
        trgY.triggers.Add(enBU);

        // ClickZone 기본 sprite 적용 (투명 이미지)
        GameObject.Find("Canvas/ClickZone/onClick").GetComponent<Image>().sprite = cleanFx[0];

        //get datas 
        DataBase.GetMoney();
        DataBase.GetWaterData();
        DataBase.GetLevels();

        // set slider
        UI_MultiScene.setWaterTank(waterTank);
        UI_MultiScene.UpdateWaterTank(waterTank);

        //Set Text
        UI_MultiScene.setWaterCounter(waterCounter);
        UI_MultiScene.setMoney(money);
        setCleanText();
    }

    // 정화 버튼 클릭
    public void clickClean()
    {
        //get Datas
        DataBase.GetLevels();
        DataBase.GetWaterData();

        // 만약 각 터치당 정화량 보다 보유중인 물의 양이 적을 때
        if (DataBase.uncleanedWater < DataBase.valueCleanWater[DataBase.cleanLevel])
        {
            DataBase.cleanedWater += DataBase.uncleanedWater;
            DataBase.uncleanedWater = 0;
        }
        // 일반 경우의 정화
        else
        {
            //만약 정화할 물이 없다면 (아마 접근 불가)
            if (DataBase.uncleanedWater == 0)
            {
                return;
            }

            DataBase.uncleanedWater -= DataBase.valueCleanWater[DataBase.cleanLevel];
            DataBase.cleanedWater += DataBase.valueCleanWater[DataBase.cleanLevel];
        }

        //Set data
        DataBase.SetWaterData();

        //Set Text
        UI_MultiScene.setWaterCounter(waterCounter);
    }

    //현 정보 표시 Texts 세팅
    public void setCleanText()
    {
        //get Datas
        DataBase.GetLevels();
        DataBase.GetWaterData();

        // 최고 레벨보다 낮다면
        if (DataBase.cleanLevel != DataBase.valueCleanWater.Length - 1)
        {
            explain.text = "현재 :  1터치 = " + DataBase.valueCleanWater[DataBase.cleanLevel] + "ml\n" +
                           "업그레이드 :  1터치 = " +
                           DataBase.valueCleanWater[DataBase.cleanLevel + 1] + "ml";
            upText.text = DataBase.upgradeClean[DataBase.cleanLevel + 1] + "$";
        }
        // 최고 레벨이라면
        else
        {
            explain.text =
                "현재 :  1터치 = " + DataBase.valueCleanWater[DataBase.cleanLevel] + "ml\n" + "최고 레벨입니다.";
            upText.text = "MAX";
        }
    }

    //정화소 업그레이드
    public void upCleanLevel()
    {
        //Get data
        DataBase.GetMoney();
        DataBase.GetLevels();

        // 업그레이드 비용이 충분하고, 레벨이 최대가 아니라면
        if ((DataBase.money >= DataBase.upgradeClean[DataBase.cleanLevel]) &&
            (DataBase.cleanLevel < DataBase.valueCleanWater.Length))
        {
            DataBase.money -= DataBase.upgradeClean[++DataBase.cleanLevel];
            
            //Set Data
            DataBase.SetLevels();
            DataBase.SetMoney();
            
            //Set Texts
            setCleanText();
            UI_MultiScene.setMoney(money);
        }
        else
        {
            // 돈부족
            //TODO : 돈이 부족합니다 팝업 띄우기 
        }
    }
}