using UnityEngine;
using UnityEngine.UI;

public class UI_ShopScene : MonoBehaviour {
    [Header("팝업창")] public GameObject popupOK;
    [Header("팝업창 text")] public Text popupText;

    void Start()
    {
        // //Get UI
        // popupOK = GameObject.Find("Canvas/PopUp(ok)");
        // popupText = GameObject.Find("Canvas/PopUp(ok)/Text").GetComponent<Text>();

        //get Data
        DataBase.GetMoney();
        DataBase.GetWaterData();
        DataBase.GetLocalData(3);

        // dessert locker
        GameObject.Find("Canvas/RichMan/Button/Lock").SetActive(DataBase.isLocalLock[3]);

        //팝업 비활성화 (UI접근 위함)
        popupOK.SetActive(false);

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

        DataBase.GetWaterData();
        DataBase.GetMoney();

        if (index == 0)
        {
            // 빗물 => 정화된 물 => 사막물 순서로 빠짐
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
                    //물없음
                    popupOK.SetActive(true);
                    popupText.text = "보유 빗물이 부족합니다.";
                    return;
                }

                DataBase.money += DataBase.costConsummer[index];
            }
            else
            {
                //물 부족
                popupOK.SetActive(true);
                popupText.text = "보유 빗물이 부족합니다.";
                return;
            }
        }
        else if (index == 1)
        {
            if (DataBase.cleanedWater >= 1000)
            {
                DataBase.money += DataBase.costConsummer[index];
                DataBase.cleanedWater -= 1000;
            }
            else
            {
                //돈 부족
                popupOK.SetActive(true);
                popupText.text = "보유 빗물이 부족합니다.";
                return;
            }
        }
        else if (index == 2)
        {
            if (DataBase.isLocalLock[3])
            {
                popupOK.SetActive(true);
                popupText.text = "해금되지 않은 거래처입니다.";
                return;
            }

            if (DataBase.desertWater >= 1000)
            {
                DataBase.money += DataBase.costConsummer[index];
                DataBase.desertWater -= 1000;
            }
            else
            {
                //돈 부족
                popupOK.SetActive(true);
                popupText.text = "보유 빗물이 부족합니다.";
                return;
            }
        }

        //set Data
        DataBase.SetWaterData();
        DataBase.SetMoney();

        //set Text
        UI_MultiScene.instance.setWaterCounter();
        UI_MultiScene.instance.setMoney();

        //set Tank
        UI_MultiScene.instance.updateWaterTank();
    }
}