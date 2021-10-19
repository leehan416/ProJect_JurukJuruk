using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ShopScene : MonoBehaviour {
    private Text[] popupText = new Text[2];
    private Text okText;
    private Button yesBtn;

    private void Awake()
    {
        // get UI
        popupText[0] = UI_MultiScene.instance.popUpOK.gameObject.GetComponentsInChildren<Text>()[1];
        popupText[1] = UI_MultiScene.instance.popUpYN.gameObject.GetComponentsInChildren<Text>()[2];
        okText = UI_MultiScene.instance.popUpYN.gameObject.GetComponentsInChildren<Text>()[1];
        yesBtn = UI_MultiScene.instance.popUpYN.gameObject.GetComponentsInChildren<Button>()[1];
    }

    void Start()
    {
        //get Data
        DataBase.getMoney();
        DataBase.getWaterData();
        DataBase.getLocalData(3);
        DataBase.getConsumerLock();

        // set ui
        lockerSet();

        //set Text
        UI_MultiScene.instance.setMoney();
        UI_MultiScene.instance.setWaterCounter();
        //set Tank
        UI_MultiScene.instance.setWaterTank();
        UI_MultiScene.instance.updateWaterTank();
    }

    public void lockerSet()
    {
        for (int i = 0; i < DataBase.consumers.Length; i++)
        {
            if (DataBase.consumers[i].isLock)
            {
                GameObject.Find("Canvas/ListView/Viewport/Content/List" + (i + 1) + "/Lock")
                    .SetActive(true);
                GameObject.Find("Canvas/ListView/Viewport/Content/List" + (i + 1) + "/TextBouble/Explain")
                    .GetComponent<Text>()
                    .text = "???";
            }
            else
            {
                try
                {
                    //lock이 없다면 넘어가기
                    GameObject.Find("Canvas/ListView/Viewport/Content/List" + (i + 1) + "/Lock")
                        .SetActive(false);
                }
                catch (Exception e)
                {
                }

                GameObject.Find("Canvas/ListView/Viewport/Content/List" + (i + 1) + "/TextBouble/Explain")
                    .GetComponent<Text>()
                    .text = DataBase.consumers[i].text;
            }
        }
    }

    // 물 판매업자 해금
    public void unlockConsumer(int index)
    {
        UI_MultiScene.instance.unactivePopup();
        DataBase.getMoney();
        if (DataBase.money >= DataBase.consumers[index].cost)
        {
            // 해금 가능
            DataBase.money -= DataBase.consumers[index].cost;
            DataBase.consumers[index].isLock = false;
            DataBase.setConsumerLock();
            lockerSet();
            UI_MultiScene.instance.setMoney();
        }
        else
        {
            //해금 불가능
            UI_MultiScene.instance.popupIsOn = true;
            UI_MultiScene.instance.popUpBG.SetActive(true);
            UI_MultiScene.instance.popUpOK.SetActive(true);
            popupText[0].text = "보유 금액이 부족합니다.";
        }
    }

    // 물 자선
    public void giveWater()
    {
        UI_MultiScene.instance.popupIsOn = true;
        UI_MultiScene.instance.popUpBG.SetActive(true);
        UI_MultiScene.instance.popUpYN.SetActive(true);
        okText.text = "YES";

        // 만약 EventTrigger 가 이미 존재한다면 파괴.
        Destroy(yesBtn.GetComponent<EventTrigger>());
        //eventrigger 생성
        EventTrigger trg = yesBtn.gameObject.AddComponent<EventTrigger>();
        //엔트리 생성
        EventTrigger.Entry en = new EventTrigger.Entry();
        // 트리거타입 추가 (터치를 종료했을 때) 
        en.eventID = EventTriggerType.PointerUp;
        // 함수 설정
        en.callback.AddListener(delegate { sellWater(-1); });
        // 트리거에 엔트리를 추가한다.
        trg.triggers.Add(en);
    }


    // 물 판매 (index) => 상인 선택
    public void sellWater(int index)
    {
        //Get data
        DataBase.getWaterData();
        DataBase.getMoney();
        DataBase.getConsumerLock();

        // 물 초기화
        if (index == -1)
        {
            for (int i = 0; i < DataBase.water.Length; i++)
                DataBase.water[i] = 0;
            DataBase.setWaterData();
            UI_MultiScene.instance.unactivePopup();
            UI_MultiScene.instance.updateWaterTank();
            UI_MultiScene.instance.setWaterCounter();
            return;
        }

        if (DataBase.consumers[index].isLock)
        {
            // 해금 필요
            if (DataBase.consumers[index].limitOption <= DataBase.soldWater[DataBase.consumers[index].waterType])
            {
                // 해금 가능

                UI_MultiScene.instance.popupIsOn = true;
                UI_MultiScene.instance.popUpBG.SetActive(true);
                UI_MultiScene.instance.popUpYN.SetActive(true);
                popupText[1].text = "해금하시겠습니까?";
                okText.text = DataBase.consumers[index].cost + "$";

                // 만약 EventTrigger 가 이미 존재한다면 파괴.
                Destroy(yesBtn.GetComponent<EventTrigger>());
                //eventrigger 생성
                EventTrigger trg = yesBtn.gameObject.AddComponent<EventTrigger>();
                //엔트리 생성
                EventTrigger.Entry en = new EventTrigger.Entry();
                // 트리거타입 추가 (터치를 종료했을 때) 
                en.eventID = EventTriggerType.PointerUp;
                // 합수 설정
                en.callback.AddListener(delegate { unlockConsumer(index); });
                // 트리거에 엔트리를 추가한다.
                trg.triggers.Add(en);
                return;
            }
            else
            {
                // 해금 불가능
                UI_MultiScene.instance.popupIsOn = true;
                UI_MultiScene.instance.popUpBG.SetActive(true);
                UI_MultiScene.instance.popUpOK.SetActive(true);
                popupText[0].text = "해금되지 않은 거래처 입니다\n해금까지 필요한 거래량 (" +
                                    DataBase.soldWater[DataBase.consumers[index].waterType] + " / " +
                                    DataBase.consumers[index].limitOption + ")";
                return;
            }
        }


        if (DataBase.water[DataBase.consumers[index].waterType] < DataBase.consumers[index].perWater)
        {
            // 물 부족
            UI_MultiScene.instance.popupIsOn = true;
            UI_MultiScene.instance.popUpBG.SetActive(true);
            UI_MultiScene.instance.popUpOK.SetActive(true);
            popupText[0].text = "보유 빗물이 부족합니다.";
            return;
        }

        //value set
        DataBase.water[DataBase.consumers[index].waterType] -= DataBase.consumers[index].perWater;
        DataBase.soldWater[DataBase.consumers[index].waterType] += DataBase.consumers[index].perWater;
        DataBase.money += DataBase.consumers[index].perCell;

        //set Data
        DataBase.setWaterData();
        DataBase.setMoney();

        //set Text
        UI_MultiScene.instance.setWaterCounter();
        UI_MultiScene.instance.setMoney();

        //set Tank
        UI_MultiScene.instance.updateWaterTank();
    }
}