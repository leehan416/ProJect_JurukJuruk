using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_MapScene : MonoBehaviour {
    private Text money; // money Text 
    private Text btnTextYN; // 가격 Text
    private Text btnTextOK; // 돈 없을때 뜨는 Text

    private GameObject popUpOK; // ok PopUp
    private GameObject popUpYN; // Yes or No PopUp

    private GameObject[] locker = new GameObject[4]; // Local Locker

    private Button yesBtn; // 구매 버튼

    void Start()
    {
        //Set UI
        locker[1] = GameObject.Find("Canvas/List/Countryside/lock").gameObject;
        locker[2] = GameObject.Find("Canvas/List/Amazon/lock").gameObject;
        locker[3] = GameObject.Find("Canvas/List/Dessert/lock").gameObject;

        popUpYN = GameObject.Find("Canvas/PopUp");
        popUpOK = GameObject.Find("Canvas/PopUp(ok)");

        yesBtn = GameObject.Find("Canvas/PopUp/Yes").GetComponent<Button>();

        money = GameObject.Find("Canvas/MoneyBack/Money").GetComponent<Text>(); // money
        btnTextOK = GameObject.Find("Canvas/PopUp(ok)/Explain").GetComponent<Text>(); // popup text
        btnTextYN = GameObject.Find("Canvas/PopUp/Yes/Text").GetComponent<Text>(); // popup money

        //팝업 비활성화 (UI 발아오기 위함)
        popUpYN.SetActive(false);
        popUpOK.SetActive(false);

        // Set Text
        UI_MultiScene.setMoney(money);
        // Set Locker
        setMapLocker();
    }

    //Set Local Locker 
    public void setMapLocker()
    {
        for (int i = 1; i < DataBase.isLocalLock.Length; i++)
        {
            DataBase.GetLocalData(i);
            locker[i].SetActive(DataBase.isLocalLock[i]);
        }
    }


    // Unlock local
    private void unLockLocal(int val)
    {
        //popup 비활성화
        popUpYN.SetActive(false);

        //Get Data
        DataBase.GetMoney();
        DataBase.GetLocalData(val);

        // 해금 가능한 돈이 있다면
        if (DataBase.money >= DataBase.localCost[val])
        {
            DataBase.money -= DataBase.localCost[val];
            DataBase.isLocalLock[val] = false;
            DataBase.SetMoney();
            DataBase.SetLocalData(val);
            setMapLocker();
        }
        // 돈이 부족하다면
        else
        {
            popUpOK.SetActive(true);
            btnTextOK.text = "보유 금액이 부족합니다.";
        }
    }

    // click local
    public void moveLocal(int val)
    {
        //Get Data
        DataBase.GetLocalData(val);

        //local이 잠겨 있다면
        if (DataBase.isLocalLock[val])
        {
            // popup 활성화
            popUpYN.SetActive(true);

            // 구매버튼에 가격 표시
            btnTextYN.text = DataBase.localCost[val] + " $";

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
            UI_MultiScene.moveScene("Main");
        }
    }
}