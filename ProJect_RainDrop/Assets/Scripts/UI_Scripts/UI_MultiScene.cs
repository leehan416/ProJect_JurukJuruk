using System;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UI_MultiScene : MonoBehaviour {
    public static UI_MultiScene instance; // transform에 접근하기 위한 instance
    public GameObject popUpBG; // PopUpBG
    public GameObject popUpOK; // ok PopUp

    public GameObject popUpYN; // Yes or No PopUp

    // popup on
    [HideInInspector] public bool popupIsOn;

    // 물탱크
    public Slider waterTank;

    // 돈
    public Text money;

    // 물통 옆 현재 각 빗물 Text set
    public Text[] waterCounter = new Text[4];

    private void Awake()
    {
        // 외부 접근 instance 설정
        if (!instance) instance = this;
        else Destroy(this);
    }

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
        // !! 안드로이드에서만 작동함
        if (Application.loadedLevelName == "Main")
        {
            // 팝업 상태 변경
            popupIsOn = !popupIsOn;

            // 팝업 상태에 따라 게임 시간 설정 -> 팝업 활성화시 게임 정지
            Time.timeScale = (popupIsOn) ? 0f : 1f;

            // 팝업 활성화, 비활성화
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

        //setactive rains or not
        try{ 
            if (val.Equals("Main")) {
                DontDestroy_Rains.instance.rains.SetActive(true);
                DontDestroy_Rains.instance.addPower();
            } else {
                //DataBase.
                DontDestroy_Rains.instance.rains.SetActive(false);
            }
         }
        catch(NullReferenceException){  }
        SceneManager.LoadScene(val);
    }

    // 팝업 끄기
    public void unactivePopup()
    {
        popUpBG.SetActive(false);
        try // 에러 무시
        {
            popUpOK.SetActive(false);
        }
        catch
        {
        }

        try // 에러 무시
        {
            popUpYN.SetActive(false);
        }
        catch
        {
        }

        popupIsOn = false;
    }


    public void playFx(int val)
    {
        // 접근용도
        SoundManager.instance.playFx(val);
    }


    public void setBtnFunc(Button btn, Action<int> func, int value)
    {
        // 만약 EventTrigger 가 이미 존재한다면 파괴.
        Destroy(btn.GetComponent<EventTrigger>());
        //eventrigger 생성
        EventTrigger trg = btn.gameObject.AddComponent<EventTrigger>();
        //엔트리 생성
        EventTrigger.Entry en = new EventTrigger.Entry();
        // 트리거타입 추가 (터치를 종료했을 때) 
        en.eventID = EventTriggerType.PointerUp;
        // 합수 설정
        en.callback.AddListener(delegate { func(value); });
        // 트리거에 엔트리를 추가한다.
        trg.triggers.Add(en);
    }

    public void setPopupOK(string text)
    {
        try
        {
            popUpYN.SetActive(false);
        }
        catch
        {
        }

        popupIsOn = true;
        popUpBG.SetActive(true);
        popUpOK.SetActive(true);
        popUpOK.GetComponentsInChildren<Text>()[1].text = text;
    }
}