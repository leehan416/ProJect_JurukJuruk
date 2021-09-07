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
        GameObject.Find("Canvas/RichMan/Lock").SetActive(DataBase.locals[3].isLock);

        //set Text
        UI_MultiScene.instance.setMoney();
        UI_MultiScene.instance.setWaterCounter();
        //set Tank
        UI_MultiScene.instance.setWaterTank();
        UI_MultiScene.instance.updateWaterTank();
    }


    // 물 판매 (index) => 상인 선택
    public void sellWater(int index)
    {
        // 물판매

        DataBase.getWaterData();
        DataBase.getMoney();

        if (index == 0)
        {
            // 빗물 => 정화된 물 => 사막물 순서로 빠짐
            if (DataBase.getAllWater() >= DataBase.consumers[index].perWater)
            {
                try
                {
                    for (int i = 0;; i++)
                    {
                        if (DataBase.water[i] >= DataBase.consumers[index].perWater)
                        {
                            DataBase.water[i] -= DataBase.consumers[index].perWater;
                            DataBase.money += DataBase.consumers[index].perCell;
                            break;
                        }
                    }
                }
                catch
                {
                    //물없음
                    UI_MultiScene.instance.popUpBG.SetActive(true);
                    UI_MultiScene.instance.popUpOK.SetActive(true);
                    popupText.text = "보유 빗물이 부족합니다.";
                    return;
                }
            }
            else
            {
                //물없음
                UI_MultiScene.instance.popUpBG.SetActive(true);
                UI_MultiScene.instance.popUpOK.SetActive(true);
                popupText.text = "보유 빗물이 부족합니다.";
                return;
            }
        }
        else
        {
            // 특수한 경우 검사
            if (DataBase.consumers[index].waterType > 1)
            {
                switch (DataBase.consumers[index].waterType)
                {
                    case 2: // 사막물
                        if (DataBase.locals[3].isLock)
                        {
                            // 맵이 해금되지 않았다면
                            UI_MultiScene.instance.popUpBG.SetActive(true);
                            UI_MultiScene.instance.popUpOK.SetActive(true);
                            popupText.text = "해금되지 않은 거래처입니다.";
                            return;
                        }
                        else break;

                    case 3: // 눈
                        break;
                }
            }

            // 빗물 거래
            if (DataBase.water[DataBase.consumers[index].waterType] >= DataBase.consumers[index].perWater)
            {
                DataBase.money += DataBase.consumers[index].perCell;
                DataBase.water[DataBase.consumers[index].waterType] -= DataBase.consumers[index].perWater;
            }
            else
            {
                // 물 부족
                UI_MultiScene.instance.popUpBG.SetActive(true);
                UI_MultiScene.instance.popUpOK.SetActive(true);
                popupText.text = "보유 빗물이 부족합니다.";
                return;
            }
        }


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