using UnityEngine;
using UnityEngine.UI;

public class UI_MapScene : MonoBehaviour {
    private GameObject[] locker = new GameObject[5]; // Local Locker
    private Text btnTextYN; // 가격 Text
    private Button yesBtn; // 구매 버튼

    private void Awake()
    {
        // Set UI
        yesBtn = UI_MultiScene.instance.popUpYN.GetComponentsInChildren<Button>()[1];
        btnTextYN = yesBtn.GetComponentInChildren<Text>();
        for (int i = 1; i < 5; i++)
            locker[i] = GameObject.Find("Canvas/ListView/Viewport/Content/List" + i + "/lock");
    }

    void Start()
    {
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
        UI_MultiScene.instance.popupIsOn = false;
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

            // sound (FX)
            SoundManager.instance.playFx(1);
        }
        // 돈이 부족하다면
        else
        {
            // 팝업 활성화
            UI_MultiScene.instance.setPopupOK("보유 금액이 부족합니다.");
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
            UI_MultiScene.instance.popupIsOn = true;
            UI_MultiScene.instance.popUpBG.SetActive(true);
            UI_MultiScene.instance.popUpYN.SetActive(true);

            // 구매버튼에 가격 표시
            btnTextYN.text = DataBase.locals[val].cost + " $";

            // 버튼 함수 설정
            UI_MultiScene.instance.setBtnFunc(yesBtn, unLockLocal, val);
        }
        // 일반 지역 이동
        else
        {
            // sound (FX)
            SoundManager.instance.playFx(0);

            // 위치 저장
            PlayerPrefs.SetInt("NowLocal", val);
            // 위치 변경
            DataBase.nowLocal = val;

            // sound (change music)
            SoundManager.instance.playMusic(val);

            //비 모두 제거
            DontDestroy_Rains.instance.removeRains();

            // 씬 이동
            UI_MultiScene.instance.moveScene("Main");
        }
    }
}