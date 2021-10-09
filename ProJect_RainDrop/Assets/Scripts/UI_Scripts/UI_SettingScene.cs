using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_SettingScene : MonoBehaviour {
    // sliders
    private Slider bgmSlider;
    private Slider fxSlider;

    //toggle => 조작 반전
    private Toggle reverse;

    private void Awake()
    {
        // get UI
        bgmSlider = GameObject.Find("Canvas/Setting_bg/BgmSlider").GetComponent<Slider>();
        fxSlider = GameObject.Find("Canvas/Setting_bg/FxSlider").GetComponent<Slider>();
        reverse = GameObject.Find("Canvas/Setting_bg/ControllerTogle").GetComponent<Toggle>();
    }

    private void Start()
    {
        DataBase.getSettingVal();
        SetSettingObj();
    }

    // value 초기 설정
    public void SetSettingObj()
    {
        bgmSlider.value = DataBase.bgmVol;
        fxSlider.value = DataBase.fxVol;
        reverse.isOn = DataBase.isReverse;
    }

    // bgm슬라이더 조작시 호출 val => volum
    public void ChangeBgmVol(float val)
    {
        SoundManager.instance.bgmSource.volume = val;
        DataBase.bgmVol = val;
        DataBase.setSettingVal();
    }

    // fx슬라이더 조작시 호출 val => volum
    public void ChangeFxVol(float val)
    {
        SoundManager.instance.fxSource.volume = val;
        DataBase.fxVol = val;
        DataBase.setSettingVal();
    }

    // toggle 클릭시 호출 val => ischecked?
    public void ChangeControllReverse(bool val)
    {
        DataBase.isReverse = val;
        DataBase.setSettingVal();
    }
}