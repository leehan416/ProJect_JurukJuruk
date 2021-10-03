using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_MultiScene : MonoBehaviour {
    // transform에 접근하기 위한 instance
    public static UI_MultiScene instance;

    public GameObject popUpBG; // PopUpBG
    public GameObject popUpOK; // ok PopUp
    public GameObject popUpYN; // Yes or No PopUp

    // popup on
    [HideInInspector] public bool popupIsOn = false;

    // 물통 옆 현재 각 빗물 Text set
    public Slider waterTank;
    public Text money;

    public Text[] waterCounter = new Text[4];


    private void Update()
    {
        // 안드로이드인 경우
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) //뒤로가기 키 입력
                backBtn();
        }
    }

    public void backBtn()
    {
        if (Application.loadedLevelName == "Main")
        {
            popupIsOn = !popupIsOn;

            Time.timeScale = (popupIsOn) ? 0f : 1f;

            popUpBG.SetActive(popupIsOn);
            popUpYN.SetActive(popupIsOn);
        }
        // 인트로에선 바로 종료
        else if (Application.loadedLevelName == "Intro") Application.Quit(); // 게임 종료
        // 코스튬씬에선 따로 작동
        else if (Application.loadedLevelName == "Costume")
        {
            if (!GachaSystem.instance.isAnimationing && popupIsOn)
                UI_CostumeScene.unactiveCostumePopup();
            else if (!popupIsOn) moveScene("Main");
        }
        // 아니라면
        else
        {
            if (popupIsOn) unactivePopup();
            else moveScene("Main");
        }
    }

    private void Awake()
    {
        // 외부 접근 instance 설정
        if (!instance) instance = this;
        else Destroy(this);
    }

    public void setWaterCounter()
    {
        DataBase.getWaterData(); // 빗물 데이터 가져오기

        //text set
        for (int i = 0; i < DataBase.water.Length; i++)
            waterCounter[i].text = DataBase.water[i].ToString();
    }


    // 물탱크 초기 세팅
    public void setWaterTank()
    {
        DataBase.getWaterData(); // 빗물 데이터 가져오기
        DataBase.getLevels(); // 레벨 데이터 가져오기
        waterTank.maxValue = DataBase.valueMaxWater[DataBase.tankLevel]; // maxvalue = 현재 레벨의 빗물 최대 양
        waterTank.minValue = 0f; // min = 0 
    }

    // 물탱크 static value 변경
    public void updateWaterTank()
    {
        DataBase.getWaterData(); // 빗물 데이터 가져오기
        waterTank.value = DataBase.getAllWater(); //  value = 빗물 전체 합
    }

    // 현재 돈 text set
    public void setMoney()
    {
        DataBase.getMoney(); // 돈 데이터 가져오기
        money.text = Convert.ToString(DataBase.money) + " $"; // text set
    }

    //Scene 이동 
    public void moveScene(string val)
    {
        SceneManager.LoadScene(val);
    }


    // public void popupBGActive()
    // {
    //     popUpBG.SetActive(true);
    // }

    public void unactivePopup()
    {
        popUpBG.SetActive(false);
        try // 빨간 에러 보기 싫어서 만든 알고리즘 
        {
            popUpOK.SetActive(false);
        }
        catch
        {
        }

        try // 빨간 에러 보기 싫어서 만든 알고리즘 
        {
            popUpYN.SetActive(false);
        }
        catch
        {
        }

        popupIsOn = false;
    }
}