using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_CostumeScene : MonoBehaviour {
    //TODO : 돈없을 떄 처리 해야함.
    public GameObject machine;
    public GameObject[] machinebtns = new GameObject[2];

    public GameObject itemPopup;
    public Image itemSprite;

    public GameObject get;
    public GameObject fail;

    public Sprite[] getCoustumeUI = new Sprite[5];


    void Start()
    {
        UI_MultiScene.instance.setMoney();
        DataBase.getCoustume();
        // DataBase.
        setLockers();
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
        machine.SetActive(true);
        machinebtns[0].SetActive(true);
        machinebtns[1].SetActive(true);
    }

    //뽑기
    public void gachaBtn()
    {
        machinebtns[0].SetActive(false);
        machinebtns[1].SetActive(false);
        StartCoroutine(GachaSystem.instance.gachaAnimation());
        StartCoroutine(animationawait());
    }

    IEnumerator animationawait()
    {
        while (GachaSystem.instance.isAnimationing)
            yield return new WaitForSeconds(.1f);
        UI_MultiScene.instance.popUpYN.SetActive(false);
        setPopUp(GachaSystem.instance.gacha());
    }

    public void setPopUp(int val)
    {
        DataBase.getCoustume();
        itemPopup.SetActive(true);
        get.SetActive(false);
        fail.SetActive(false);
        Debug.Log(val);
        if (val != 0)
        {
            get.SetActive(true);
            Debug.Log("!");
            itemSprite.sprite = getCoustumeUI[val];
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
                DataBase.uncleanedWater += 500;
                if (DataBase.getAllWater() >= DataBase.valueMaxWater[DataBase.tankLevel])
                    DataBase.uncleanedWater -= DataBase.getAllWater() - DataBase.valueMaxWater[DataBase.tankLevel];
            }

            DataBase.setWaterData();
        }
    }


    public void unactivePopup()
    {
        machine.SetActive(false);
        itemPopup.SetActive(false);
    }

    // 코스튬 선택 버튼
    public void costumeBtn(int val)
    {
        DataBase.costume = val;
        DataBase.setCostume();
    }
}