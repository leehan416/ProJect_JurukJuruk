using UnityEngine;

public class UI_CostumeScene : MonoBehaviour {
    // [Header("가챠기계")] public GameObject gachaMuchine;
    // [Header("ok팝업")] public GameObject okPopUp;
    // [Header("yn팝업")] public GameObject ynPopUp;
    GameObject get;
    GameObject fail;

    void Start()
    {
        get = UI_MultiScene.instance.popUpYN.gameObject.GetComponentsInChildren<GameObject>()[0];
        fail = UI_MultiScene.instance.popUpYN.gameObject.GetComponentsInChildren<GameObject>()[1];
        UI_MultiScene.instance.setMoney();
    }


    public void setLockers()
    {
        // gachaMuchine.SetActive(true);
    }

    // 뽑기 버튼
    public void gachaBtn()
    {
    }


    // 코스튬 선택 버튼
    public void costumeBtn(int val)
    {
        DataBase.costume = val;
        DataBase.setCostume();
    }
}