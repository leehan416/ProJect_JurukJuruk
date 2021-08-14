using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CostumeScene : MonoBehaviour {
    private GameObject gachaMuchine;

    private GameObject popUp;

    private GameObject get;
    private GameObject fail;


    private void Start()
    {
        gachaMuchine = GameObject.Find("");
        popUp = GameObject.Find("");
        get = GameObject.Find("");
        fail = GameObject.Find("");


        UI_MultiScene.instance.setMoney();
    }


    public void setLockers()
    {
        
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