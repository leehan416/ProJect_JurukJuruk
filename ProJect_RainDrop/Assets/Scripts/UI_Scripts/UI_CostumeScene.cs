using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_CostumeScene : MonoBehaviour {
    private GameObject gachaMuchine;
    private GameObject popUp;

    private GameObject get;
    private GameObject fail;


    private void Start()
    {
        gachaMuchine = GameObject.Find("Canvas/MachineBG");
        popUp = GameObject.Find("Canvas/PopUpItem");
        get = GameObject.Find("Canvas/PopUpItem/Get");
        fail = GameObject.Find("Canvas/PopUpItem/Fail");


        gachaMuchine.SetActive(false);
        get.SetActive(false);
        fail.SetActive(false);
        popUp.SetActive(false);


        UI_MultiScene.instance.setMoney();
    }


    public void setLockers()
    {
        gachaMuchine.SetActive(true);
    }

    // 뽑기 버튼
    public void gachaBtn()
    {
    }


    // 코스튬 선택 버튼
    public void costumeBtn()
    {
    }
}