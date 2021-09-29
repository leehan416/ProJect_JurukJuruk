using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_ShopScene : MonoBehaviour {
    // [Header("팝업창")] public GameObject popupOK;
    // [Header("팝업창 text")] public Text popupText;
    private Text popupText;

    void Start()
    {
        popupText = UI_MultiScene.instance.popUpOK.gameObject.GetComponentsInChildren<Text>()[1];
        // //Get UI
        // popupOK = GameObject.Find("Canvas/PopUp(ok)");
        // popupText = GameObject.Find("Canvas/PopUp(ok)/Text").GetComponent<Text>();

        //get Data
        DataBase.getMoney();
        DataBase.getWaterData();
        DataBase.getLocalData(3);

        // dessert locker
        GameObject.Find("Canvas/ListView/Viewport/Content/RichMan/Lock").SetActive(DataBase.locals[3].isLock);

        //set Text
        UI_MultiScene.instance.setMoney();
        UI_MultiScene.instance.setWaterCounter();
        //set Tank
        UI_MultiScene.instance.setWaterTank();
        UI_MultiScene.instance.updateWaterTank();
    }

    public void unlockConsumer(int index)
    {
    }

    public void giveWater()
    {
        UI_MultiScene.instance.popupIsOn = true;
        UI_MultiScene.instance.popUpBG.SetActive(true);
        UI_MultiScene.instance.popUpYN.SetActive(true);
    }


    // 물 판매 (index) => 상인 선택
    public void sellWater(int index)
    {
        DataBase.getWaterData();
        DataBase.getMoney();
        DataBase.getConsumerLock();

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
            UI_MultiScene.instance.popupIsOn = true;
            UI_MultiScene.instance.popUpBG.SetActive(true);
            UI_MultiScene.instance.popUpOK.SetActive(true);
            popupText.text = "해금되지 않은 거래처입니다.";
            return;
        }


        if (DataBase.water[DataBase.consumers[index].waterType] < DataBase.consumers[index].perCell)
        {
            // 물 부족
            UI_MultiScene.instance.popupIsOn = true;
            UI_MultiScene.instance.popUpBG.SetActive(true);
            UI_MultiScene.instance.popUpOK.SetActive(true);
            popupText.text = "보유 빗물이 부족합니다.";
            return;
        }

        DataBase.water[DataBase.consumers[index].waterType] -= DataBase.consumers[index].perWater;
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