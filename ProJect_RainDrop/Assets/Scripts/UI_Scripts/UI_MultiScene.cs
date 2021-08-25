using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_MultiScene : MonoBehaviour {
    // transform에 접근하기 위한 instance
    public static UI_MultiScene instance;

    //TODO 팝업 unactive 기능 제작해야함


    [Header("ok PopUp")] public GameObject popUpOK; // ok PopUp
    [Header("Yes or No PopUp")] public GameObject popUpYN; // Yes or No PopUp


    // 물통 옆 현재 각 빗물 Text set
    public Slider waterTank;
    public Text money;

    public Text[] waterCounter = new Text[3];


    private void Update()
    {
        // 안드로이드인 경우
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape)) //뒤로가기 키 입력
            {
                try
                {
                    bool test = UI_MainScene.isMain;
                    // main
                    Application.Quit(); // 게임 종료
                }
                catch (Exception e)
                {
                    // main 아님
                    moveScene("Main");
                }
            }
        }
    }

    private void Start()
    {
        if (!instance) instance = this;
        else Destroy(this);
    }

    public void setWaterCounter()
    {
        DataBase.getWaterData(); // 빗물 데이터 가져오기

        //text set
        waterCounter[0].text = DataBase.uncleanedWater.ToString();
        waterCounter[1].text = DataBase.cleanedWater.ToString();
        waterCounter[2].text = DataBase.desertWater.ToString();
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


    public void unactivePopup()
    {
        popUpOK.SetActive(false);
        popUpYN.SetActive(false);
    }
}