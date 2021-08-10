using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_CleanScene : MonoBehaviour {
    private Text money;
    private Text[] waterCounter = new Text[3];
    private Button clickZone;

    private Slider waterTank; // 물탱크

    private Text upText; // 업그레이드 가격 Text

    private Text explain; // 현재, 다음 업그레이드 설명 Text 
    [Header("클린 스프라이트")] public Sprite[] cleanFx = new Sprite[2];


    void Start()
    {
        // UX_Manager.instance.text[5] = GameObject.Find("Canvas/Explain_Memo/Recent").GetComponent<Text>();
        // UX_Manager.instance.text[6] = GameObject.Find("Canvas/Up/Text").GetComponent<Text>();
        //
        // UX_Manager.instance.text[0] = GameObject.Find("Canvas/MoneyBack/Money").GetComponent<Text>(); // money
        // UX_Manager.instance.text[2] = GameObject.Find("Canvas/Tank/ShowAmount/ShowAmount_Basic").GetComponent<Text>();
        // UX_Manager.instance.text[3] = GameObject.Find("Canvas/Tank/ShowAmount/ShowAmount_Clean").GetComponent<Text>();
        // UX_Manager.instance.text[4] = GameObject.Find("Canvas/Tank/ShowAmount/ShowAmount_Desert").GetComponent<Text>();
        // UX_Manager.instance.slider[0] = GameObject.Find("Canvas/Tank").GetComponent<Slider>(); // waterTank
        //
        // UX_Manager.instance.yesBtn = GameObject.Find("Canvas/ClickZone").GetComponent<Button>();

        EventTrigger trgY = clickZone.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry enYH = new EventTrigger.Entry();
        EventTrigger.Entry enYU = new EventTrigger.Entry();

        enYH.eventID = EventTriggerType.PointerDown;
        enYU.eventID = EventTriggerType.PointerUp;


        //TODO : Level Value Set
        enYH.callback.AddListener(delegate
        {
            GameObject.Find("Canvas/ClickZone/onClick").GetComponent<Image>().sprite = cleanFx[1];
        });
        enYU.callback.AddListener(delegate
        {
            GameObject.Find("Canvas/ClickZone/onClick").GetComponent<Image>().sprite = cleanFx[0];
        });
        trgY.triggers.Add(enYH);
        trgY.triggers.Add(enYU);

        // GameObject.Find("Canvas/ClickZone/onClick").GetComponent<Image>().sprite = UX_Manager.instance.cleanFx[0];
        DataBase.GetMoney();
        DataBase.GetWaterData();
        DataBase.GetLevels();

        // SetCleanText();
        // UX_Manager.instance.moneySet();
        // UX_Manager.instance.waterTankSet();
        // UX_Manager.instance.waterTankUpdate();
        // UX_Manager.instance.setWaterCounter();
        // return;
    }


    public void ClickClean()
    {
        DataBase.GetLevels();
        DataBase.GetWaterData();
        // StartCoroutine(AnimationController)
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

        // UX_Manager.instance.setWaterCounter();
    }

    public void SetCleanText()
    {
        DataBase.GetLevels();
        DataBase.GetWaterData();
        if (DataBase.cleanLevel != DataBase.valueCleanWater.Length - 1)
        {
            // UX_Manager.instance.text[5].text = "현재 :  1터치 = " + DataBase.valueCleanWater[DataBase.cleanLevel] + "ml\n" +
            //                                    "업그레이드 :  1터치 = " +
            //                                    DataBase.valueCleanWater[DataBase.cleanLevel + 1] + "ml";
            // UX_Manager.instance.text[6].text = DataBase.upgradeClean[DataBase.cleanLevel + 1] + "$";
        }
        else
        {
            // UX_Manager.instance.text[5].text =
            //     "현재 :  1터치 = " + DataBase.valueCleanWater[DataBase.cleanLevel] + "ml\n" + "최고 레벨입니다.";
            // UX_Manager.instance.text[6].text = "MAX";
        }
    }

    public void UpCleanLevel()
    {
        DataBase.GetMoney();
        DataBase.GetLevels();
        if (DataBase.money >= DataBase.upgradeClean[DataBase.cleanLevel] &&
            DataBase.cleanLevel < DataBase.valueCleanWater.Length)
        {
            DataBase.GetLevels();
            DataBase.money -= DataBase.upgradeClean[++DataBase.cleanLevel];
            DataBase.SetLevels();
            DataBase.SetMoney();
            SetCleanText();
            // UX_Manager.instance.MoneySet();
        }
        else
        {
            // 돈부족
        }
    }
}