/* UI 컨트롤 스크립트 */

using System;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    public static UIManager instance;

    public Text money;
    public Text local;

    void Start()
    {
        if (!instance) instance = this;
        else DestroyImmediate(this);
        money = GameObject.Find("Canvas/Money").GetComponent<Text>();
        local = GameObject.Find("Canvas/Local").GetComponent<Text>();
    }

    public void MoneySet()
    { // 어떤 역할
        money.text = Convert.ToString(DataBase.money);
    }

    public void LocalSet()
    {
        local.text = Convert.ToString(DataBase.nowLocal);
    }

    public void MoveScene(string val)
    {
        SceneManager.LoadScene(val);
    }
}