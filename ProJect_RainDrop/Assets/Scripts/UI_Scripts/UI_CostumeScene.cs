using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_CostumeScene : MonoBehaviour {
    [Header("가챠기계")] public GameObject gachaMuchine;
    [Header("ok팝업")] public GameObject okPopUp;
    [Header("yn팝업")] public GameObject ynPopUp;

    [Header("성공")] public GameObject get;
    [Header("실패")] public GameObject fail;

    private void Start()
    {
        // gachaMuchine.SetActive(false);
        // get.SetActive(false);
        // fail.SetActive(false);
        // okPopUp.SetActive(false);
        // ynPopUp.SetActive(false);
        //
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