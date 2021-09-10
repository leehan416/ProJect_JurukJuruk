using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public delegate void UnactiveCosPopup();

public class UI_CostumeScene : MonoBehaviour {
    public static UnactiveCosPopup unactiveCostumePopup = delegate { };

    public GameObject machine;
    public GameObject[] machinebtns = new GameObject[2];

    public GameObject itemPopup;
    public Image itemSprite;

    public GameObject get;
    public GameObject fail;

    public GameObject[] CstBox = new GameObject[5];

    public Sprite[] getCoustumeUI = new Sprite[5];

    private void Awake()
    {
        unactiveCostumePopup = delegate { unactivePopup(); };
    }

    void Start()
    {
        DataBase.getMoney();
        DataBase.getCoustume();

        UI_MultiScene.instance.setMoney();

        setLockers();
        setButton();
    }


    public void setButton()
    {
        for (int i = 0; i < CstBox.Length; i++)
        {
            if (DataBase.costume == i + 1)
            {
                CstBox[i].gameObject.GetComponentsInChildren<Text>()[1].text = "해제하기";
                CstBox[i].gameObject.GetComponentInChildren<Button>().image.color =
                    new Color(192 / 255f, 192 / 255f, 192 / 255f, 1f);
            }
            else
            {
                CstBox[i].gameObject.GetComponentsInChildren<Text>()[1].text = "장착하기";
                CstBox[i].gameObject.GetComponentInChildren<Button>().image.color =
                    new Color(1f, 1f, 1f, 1f);
            }
        }
    }


    public void setLockers()
    {
        for (int i = 1; i < 6; i++)
            if (!DataBase.isCostumeLock[i])
                GameObject.Find("Canvas/ListVIew/Viewport/Content/CstBox_" + i + "/Lock").SetActive(false);
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
        else
        {
            UI_MultiScene.instance.popupIsOn = true;
            UI_MultiScene.instance.popUpBG.SetActive(true);
            UI_MultiScene.instance.popUpOK.SetActive(true);
            UI_MultiScene.instance.popUpOK.GetComponentsInChildren<Text>()[1].text = "보유 금액이 부족합니다.";
        }
    }

    IEnumerator animationawait()
    {
        while (GachaSystem.instance.isAnimationing)
            yield return new WaitForSeconds(.1f);
        machine.SetActive(false);
        setPopUp(GachaSystem.instance.gacha());
    }

    public void setPopUp(int val)
    {
        DataBase.getCoustume();
        UI_MultiScene.instance.popupIsOn = true;
        UI_MultiScene.instance.popUpBG.SetActive(true);

        itemPopup.SetActive(true);

        get.SetActive(false);
        fail.SetActive(false);

        if (val != 0)
        {
            get.SetActive(true);
            itemSprite.sprite = getCoustumeUI[val - 1];
            DataBase.isCostumeLock[val] = false;
            DataBase.setCostume();
            setLockers();
        }
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
        if (DataBase.isCostumeLock[val])
        {
            UI_MultiScene.instance.popupIsOn = true;
            UI_MultiScene.instance.popUpBG.SetActive(true);
            UI_MultiScene.instance.popUpOK.SetActive(true);
            UI_MultiScene.instance.popUpOK.GetComponentsInChildren<Text>()[1].text = "해금되지 않았습니다.";
        }
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

            DataBase.setCostume();
        }
    }
}