using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Market : MonoBehaviour {
    // private Text money;

    //pail
    [Header("양동이 정보")] public Text pailInfo;
    [Header("양동이 가격")] public Text pailCost;

    //tank
    [Header("물탱크 정보")] public Text tankInfo;
    [Header("물탱크 가격")] public Text tankCost;

    //pot
    [Header("추가 양동이 정보")] public Text[] potInfo = new Text[4];
    [Header("추가 양동이 가격")] public Text[] potCost = new Text[4];


    [Header("가격 버튼 text")] public Text btnTextYN; // 가격 Text
    [Header("구매 확인 text")] public Text btnTextInfo; // YN 설명 text
    [Header("확인 팝업")] public Text btnTextOK; // OK popup 설명Text

    [Header("ok 팝업")] public GameObject popUpOK; // ok PopUp
    [Header("yn 팝업")] public GameObject popUpYN; // Yes or No PopUp

    [Header("구매 버튼")] public Button yesBtn; // 구매 버튼


    void Start()

    {
        // Set UI
        // pailInfo = GameObject.Find("Canvas/BackGround/Goods/Pail_BG/Info").GetComponent<Text>(); // 정보
        // pailCost = GameObject.Find("Canvas/BackGround/Goods/Pail_BG/PailUp/Text").GetComponent<Text>(); // 가격
        // tankInfo = GameObject.Find("Canvas/BackGround/Goods/Tank_BG/Info").GetComponent<Text>(); // 정보
        // tankCost = GameObject.Find("Canvas/BackGround/Goods/Tank_BG/TankUp/Text").GetComponent<Text>(); // 가격
        //
        // for (int i = 5; i < 9; i++)
        //     potCost[i - 5] = GameObject.Find("Canvas/BackGround/Goods/Pot_BG/Pot_" + (i - 5) + "/Text")
        //         .GetComponent<Text>();
        //
        // for (int i = 9; i < 13; i++)
        //     potInfo[i - 9] = GameObject.Find("Canvas/BackGround/Goods/Pot_BG/Pot_" + (i - 9) + "/Explain")
        //         .GetComponent<Text>();
        //
        // popUpYN = GameObject.Find("Canvas/PopUp");
        // popUpOK = GameObject.Find("Canvas/PopUp(ok)");
        //
        // btnTextOK = GameObject.Find("Canvas/PopUp(ok)/Explain").GetComponent<Text>();
        // btnTextInfo = GameObject.Find("Canvas/PopUp/Explain").GetComponent<Text>();
        // btnTextYN = GameObject.Find("Canvas/PopUp/Yes/Text").GetComponent<Text>();
        //
        // yesBtn = GameObject.Find("Canvas/PopUp/Yes").GetComponent<Button>();

        //팝업 비활성화 (UI설정)
        popUpYN.SetActive(false);
        popUpOK.SetActive(false);

        //get Data
        DataBase.GetMoney();
        DataBase.GetWaterData();
        DataBase.GetLevels();


        // Set Lockers
        setPotLockers();

        //set Text
        setMarketText();
        UI_MultiScene.instance.setMoney();
        return;
    }

    //set locker 
    public void setPotLockers()
    {
        for (int i = 0; i < 4; i++)
            if (DataBase.potLevel[i] > 0)
                GameObject.Find("Canvas/BackGround/Goods/Pot_BG/Pot_" + i + "/Lock").gameObject.SetActive(false);
    }

    //set market text 
    public void setMarketText()
    {
        // get data 
        DataBase.GetWaterData();
        DataBase.GetLevels();

        // pail 업그레이드 가능시
        if (DataBase.pailLevel != DataBase.valuePerDrop.Length - 1)
        {
            pailInfo.text =
                "업그레이드시\n한 물방울 = " + DataBase.valuePerDrop[DataBase.pailLevel] + "ml >> " +
                DataBase.valuePerDrop[DataBase.pailLevel + 1] + "ml";
            pailCost.text = DataBase.upgradePail[DataBase.pailLevel + 1] + " $";
        }
        //  pail 최대 레벨일때
        else
        {
            pailInfo.text = "한계에 도달했습니다.\n" + "한 물방울 =" + DataBase.valuePerDrop[DataBase.pailLevel] + "ml";
            pailCost.text = "Max";
        }

        // tank 업그레이드 가능시
        if (DataBase.tankLevel != DataBase.valueMaxWater.Length - 1)
        {
            tankInfo.text = "업그레이드시\n" + DataBase.valueMaxWater[DataBase.tankLevel] + "L >> " +
                            DataBase.valueMaxWater[DataBase.tankLevel + 1] + "L";
            tankCost.text = DataBase.upgradeTank[DataBase.tankLevel + 1] + " $";
        }
        // tank 최대 레벨일때
        else
        {
            tankInfo.text = "한계에 도달했습니다.\n" + DataBase.valueMaxWater[DataBase.tankLevel] + "L";
            tankCost.text = "Max";
        }

        // set pot text
        for (int i = 0; i < 4; i++)
        {
            // pot 업그레이드 가능시
            if (DataBase.potLevel[i] != DataBase.valuePotMax.Length - 1)
                potCost[i].text = DataBase.unLockPot[i] + DataBase.upgradePail[DataBase.potLevel[i]] + "$";
            // pot 최대 레벨일때
            else
                potCost[i].text = "Max";
        }

        // set pot info
        for (int i = 9; i < 13; i++)
            if (DataBase.potLevel[i - 9] > 0)
                potInfo[i - 9].text = (DataBase.potCycle[i - 9] < 30)
                    ? DataBase.potCycle[i - 9] + "초당 " + DataBase.perSecond[DataBase.potLevel[i - 9]] + "ml"
                    : "1분당 " + DataBase.perSecond[i - 9] + "ml";
    }

    // up pail level
    public void upPailLevel()
    {
        // get data
        DataBase.GetLevels();
        DataBase.GetMoney();


        // 업그레이드할 돈이 있다면 
        if (DataBase.money >= DataBase.upgradePail[DataBase.pailLevel + 1])
        {
            DataBase.money -= DataBase.upgradePail[++DataBase.pailLevel];

            //set datas
            DataBase.SetLevels();
            DataBase.SetMoney();

            //set Texts
            setMarketText();
            UI_MultiScene.instance.setMoney();
        }
        //돈이 부족하다면
        else
        {
            //popup활성화
            popUpOK.SetActive(true);

            btnTextOK.text = "보유 금액이 부족합니다.";
        }
    }


    // up tank level
    public void upTankLevel()
    {
        // get data 
        DataBase.GetLevels();
        DataBase.GetMoney();

        // 업그레이드 할 돈이 있다면
        if (DataBase.money >= DataBase.upgradeTank[DataBase.tankLevel + 1])
        {
            DataBase.money -= DataBase.upgradeTank[++DataBase.tankLevel];

            // set data
            DataBase.SetLevels();
            DataBase.SetMoney();

            // set text
            setMarketText();
            UI_MultiScene.instance.setMoney();
        }
        // 돈 부족
        else
        {
            //popup 활성화
            popUpOK.SetActive(true);
            btnTextOK.text = "보유 금액이 부족합니다.";
            //돈부족 
        }
    }

    // up pot level (local)
    public void upPotLevel(int val)
    {
        //Get Datas
        DataBase.GetLevels();
        DataBase.GetMoney();
        DataBase.GetLocalData(val);

        //팝업 비활성화
        popUpYN.SetActive(false);

        // 해금 | 해금 가격과 업글가격을 합쳐서 계산하는 방식이기 때문에, 시스템 분리
        if (DataBase.potLevel[val] == 0)
        {
            if (DataBase.money >= DataBase.unLockPot[val])
            {
                DataBase.money -= DataBase.unLockPot[val]; // + DataBase.upgradePot[++DataBase.potLevel[val] + 1];
                DataBase.potLevel[val]++;

                // set data
                DataBase.SetLevels();
                DataBase.SetMoney();

                // set text 
                setMarketText();
                UI_MultiScene.instance.setMoney();

                //set Pot Lockers
                setPotLockers();
                return;
            }
        }
        // 일반 업글
        else
        {
            if (DataBase.money >= (DataBase.unLockPot[val] + DataBase.upgradePot[DataBase.potLevel[val] + 1]))
            {
                DataBase.money -= (DataBase.unLockPot[val] + DataBase.upgradePot[++DataBase.potLevel[val]]);

                //set data
                DataBase.SetLevels();
                DataBase.SetMoney();

                //set texts
                setMarketText();
                UI_MultiScene.instance.setMoney();

                //set pot lockers
                setPotLockers();
                return;
            }
        }

        // 돈부족

        // 팝업 활성화
        popUpOK.SetActive(true);
        btnTextOK.text = "보유 금액이 부족합니다.";
    }


    // pot btn
    public void upPotBtn(int val)
    {
        // get data 
        DataBase.GetLocalData(val);
        DataBase.GetMoney();

        //지역 해금 안됌
        if (DataBase.isLocalLock[val])
        {
            popUpOK.SetActive(true);
            btnTextOK.text = "지역이 해금되지 않았습니다.";
            return;
        }

        // 지역이 해금되어 있다면
        else
        {
            // 양동이 해금
            if (DataBase.potLevel[val] <= 0)
            {
                popUpYN.SetActive(true);
                btnTextInfo.text = "해금하시겠습니까?";
                btnTextYN.text = DataBase.unLockPot[val] + " $";

                // 만약 EventTrigger 가 이미 존재한다면 파괴.
                Destroy(yesBtn.GetComponent<EventTrigger>());
                //eventrigger 생성
                EventTrigger trg = yesBtn.gameObject.AddComponent<EventTrigger>();
                //엔트리 생성
                EventTrigger.Entry en = new EventTrigger.Entry();
                // 트리거타입 추가 (터치를 종료했을 때) 
                en.eventID = EventTriggerType.PointerUp;
                // 합수 설정
                en.callback.AddListener(delegate { upPotLevel(val); });
                // 트리거에 엔트리를 추가한다.
                trg.triggers.Add(en);
            }
            // 일반 업그레이드
            else
            {
                upPotLevel(val);
            }
        }
    }
}