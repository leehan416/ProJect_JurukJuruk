using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_MapScene : MonoBehaviour {
    public GameObject[] locker = new GameObject[4]; // Local Locker

    private Text btnTextYN; // 가격 Text
    private Text btnTextOK; // 돈 없을때 뜨는 Text
    private Button yesBtn; // 구매 버튼


    void Start()
    {
        btnTextOK = UI_MultiScene.instance.popUpOK.GetComponentsInChildren<Text>()[1];
        yesBtn = UI_MultiScene.instance.popUpYN.GetComponentsInChildren<Button>()[1];
        btnTextYN = yesBtn.GetComponentInChildren<Text>();


        // Set Text
        UI_MultiScene.instance.setMoney();
        // Set Locker
        setMapLocker();
    }

    //Set Local Locker 
    public void setMapLocker()
    {
        for (int i = 1; i < DataBase.locals.Length; i++)
        {
            DataBase.getLocalData(i);
            locker[i].SetActive(DataBase.locals[i].isLock);
        }
    }


    // Unlock local
    private void unLockLocal(int val)
    {
        //popup 비활성화
        UI_MultiScene.instance.popUpBG.SetActive(false);
        UI_MultiScene.instance.popUpOK.SetActive(false);

        //Get Data
        DataBase.getMoney();
        DataBase.getLocalData(val);

        // 해금 가능한 돈이 있다면
        if (DataBase.money >= DataBase.locals[val].cost)
        {
            DataBase.money -= DataBase.locals[val].cost;
            DataBase.locals[val].isLock = false;
            DataBase.setMoney();
            DataBase.setLocalData(val);
            setMapLocker();
            UI_MultiScene.instance.unactivePopup();
            UI_MultiScene.instance.setMoney();
        }
        // 돈이 부족하다면
        else
        {
            UI_MultiScene.instance.popUpBG.SetActive(true);
            UI_MultiScene.instance.popUpOK.SetActive(true);
            btnTextOK.text = "보유 금액이 부족합니다.";
        }
    }

    // click local
    public void moveLocal(int val)
    {
        //Get Data
        DataBase.getLocalData(val);

        //local이 잠겨 있다면
        if (DataBase.locals[val].isLock)
        {
            // popup 활성화
            UI_MultiScene.instance.popUpBG.SetActive(true);
            UI_MultiScene.instance.popUpYN.SetActive(true);

            // 구매버튼에 가격 표시
            btnTextYN.text = DataBase.locals[val].cost + " $";

            // 만약 EventTrigger가 이미 존재한다면 파괴.
            Destroy(yesBtn.GetComponent<EventTrigger>());

            //이벤트 트리거 생성
            EventTrigger trg = yesBtn.gameObject.AddComponent<EventTrigger>();
            //엔트리 추가
            EventTrigger.Entry en = new EventTrigger.Entry();
            //행동 설정
            en.eventID = EventTriggerType.PointerUp;
            //함수 추가
            en.callback.AddListener(delegate { unLockLocal(val); });
            // 트리거에 엔트리 추가
            trg.triggers.Add(en);
        }
        // 일반 지역 이동
        else
        {
            PlayerPrefs.SetInt("NowLocal", val);
            DataBase.nowLocal = val;
            UI_MultiScene.instance.moveScene("Main");
        }
    }
}