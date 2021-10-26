using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public delegate void UnactiveCosPopup();

public class UI_CostumeScene : MonoBehaviour {
    //외부 접근 함수
    public static UnactiveCosPopup unactiveCostumePopup = delegate { };

    // 뽑기 기계
    public GameObject machine;

    // 뽑기 버튼 (yes & no)
    public GameObject[] machinebtns = new GameObject[2];

    // 뽑기 결과 팝업
    public GameObject itemPopup;

    // 결과 이미지
    public Image itemSprite;

    // 성공시 뜨는 팝업
    public GameObject get;

    // 실패시 뜨는 팝업
    public GameObject fail;

    // lock
    private GameObject[] cstBox = new GameObject[5];

    //이미지
    public Sprite[] getCoustumeUI = new Sprite[5];

    private void Awake()
    {
        // 외부 접근 세팅
        unactiveCostumePopup = delegate { unactivePopup(); };
    }

    void Start()
    {
        // ui set
        DataBase.getMoney();
        DataBase.getCoustume();
        //
        // DataBase.money = 0;
        // DataBase.setMoney();


        UI_MultiScene.instance.setMoney();

        for (int i = 0; i < cstBox.Length; i++)
            cstBox[i] = GameObject.Find("Canvas/ListView/Viewport/Content/CstBox_" + i);

        setButton();
        setLockers();
    }


    // 착용중인 코스튬 버튼 세팅
    public void setButton()
    {
        for (int i = 0; i < cstBox.Length; i++)
        {
            Text text = GameObject.Find("Canvas/ListView/Viewport/Content/CstBox_" + i + "/Select/Text")
                .GetComponent<Text>();
            Image image = GameObject.Find("Canvas/ListView/Viewport/Content/CstBox_" + i + "/Select")
                .GetComponent<Image>();
            if (DataBase.costume == i + 1)
            {
                text.text = "해제하기";
                cstBox[i].gameObject.GetComponentInChildren<Button>().image.color =
                    new Color(192 / 255f, 192 / 255f, 192 / 255f, 1f);
            }
            else
            {
                text.text = "장착하기";
                image.color =
                    new Color(1f, 1f, 1f, 1f);
            }
        }
    }

    // lock 세팅
    public void setLockers()
    {
        for (int i = 0; i < cstBox.Length; i++)
            if (!DataBase.isCostumeLock[i + 1])
                GameObject.Find("Canvas/ListView/Viewport/Content/CstBox_" + i + "/Lock").SetActive(false);
    }

    // 뽑기 버튼
    public void goGacha()
    {
        UI_MultiScene.instance.popupIsOn = true;
        UI_MultiScene.instance.popUpBG.SetActive(true);
        machine.SetActive(true);
        machinebtns[0].SetActive(true);
        machinebtns[1].SetActive(true);
        DataBase.setMoney();
    }

    //뽑기
    public void gachaBtn()
    {
        DataBase.getMoney();
        // 뽑기 가능
        if (DataBase.money >= DataBase.gachaCost)
        {
            DataBase.money -= DataBase.gachaCost;

            DataBase.setMoney();
            UI_MultiScene.instance.setMoney();

            machinebtns[0].SetActive(false);
            machinebtns[1].SetActive(false);
            StartCoroutine(GachaSystem.instance.gachaAnimation());
            StartCoroutine(animationawait());
        }
        // 불가능
        else
        {
            UI_MultiScene.instance.setPopupOK("보유 금액이 부족합니다.");
            // UI_MultiScene.instance.popupIsOn = true;
            // UI_MultiScene.instance.popUpBG.SetActive(true);
            // UI_MultiScene.instance.popUpOK.SetActive(true);
            // UI_MultiScene.instance.popUpOK.GetComponentsInChildren<Text>()[1].text = "보유 금액이 부족합니다.";
        }
    }

    // 애니메이션 끝날 때 까지 기다리는 코루틴
    IEnumerator animationawait()
    {
        while (GachaSystem.instance.isAnimationing) // wait
            yield return new WaitForSeconds(.1f);
        machine.SetActive(false); // 끝나면 비활성화
        setPopUp(GachaSystem.instance.gacha()); // 결과 표시
    }

    // 결과 팝업 세팅
    public void setPopUp(int val)
    {
        DataBase.getCoustume();
        UI_MultiScene.instance.popupIsOn = true;
        UI_MultiScene.instance.popUpBG.SetActive(true);
        itemPopup.SetActive(true);

        get.SetActive(false);
        fail.SetActive(false);

        // 성공 
        if (val != 0)
        {
            get.SetActive(true);
            itemSprite.sprite = getCoustumeUI[val - 1];
            //Data set
            DataBase.isCostumeLock[val] = false;
            DataBase.setCostume();
            //UI set
            setLockers();
        }
        // 실패
        else
        {
            fail.SetActive(true);
            DataBase.getWaterData();
            DataBase.getLevels();

            if (DataBase.getAllWater() >= DataBase.valueMaxWater[DataBase.tankLevel]) ;
            else
            {
                DataBase.water[0] += 500;
                if (DataBase.getAllWater() >= DataBase.valueMaxWater[DataBase.tankLevel])
                    DataBase.water[0] -= DataBase.getAllWater() - DataBase.valueMaxWater[DataBase.tankLevel];
            }

            DataBase.setWaterData();
        }
    }


    // 팝업 끄기 (다른 시스템과는 다른 팝업이 들어가서 추가적인 함수)
    public void unactivePopup()
    {
        UI_MultiScene.instance.popupIsOn = false;
        UI_MultiScene.instance.popUpBG.SetActive(false);
        machine.SetActive(false);
        itemPopup.SetActive(false);
        UI_MultiScene.instance.popUpOK.SetActive(false);
    }

    // 코스튬 선택 버튼
    public void costumeBtn(int val)
    {
        // 해금 이전
        if (DataBase.isCostumeLock[val])
        {
            UI_MultiScene.instance.setPopupOK("해금되지 않았습니다.");
            // UI_MultiScene.instance.popupIsOn = true;
            // UI_MultiScene.instance.popUpBG.SetActive(true);
            // UI_MultiScene.instance.popUpOK.SetActive(true);
            // UI_MultiScene.instance.popUpOK.GetComponentsInChildren<Text>()[1].text = "해금되지 않았습니다.";
        }
        // 해금 되어있음
        else
        {
            if (val != DataBase.costume)
            {
                // 장착
                DataBase.costume = val;
                setButton();
            }
            else
            {
                // 해제
                DataBase.costume = 0;
                setButton();
            }

            // 코스튬 저장
            DataBase.setCostume();
        }
    }


    // private void setPopup(string text)
    // {
    //     // UI_MultiScene.instance.popUpYN.SetActive(false);
    //     UI_MultiScene.instance.popupIsOn = true;
    //     UI_MultiScene.instance.popUpBG.SetActive(true);
    //     UI_MultiScene.instance.popUpOK.SetActive(true);
    //     UI_MultiScene.instance.popUpOK.GetComponentsInChildren<Text>()[1].text = text;
    // }
}