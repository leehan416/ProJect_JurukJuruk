using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_LogoScene : MonoBehaviour {
    private void Start()
    {
        StartCoroutine(move());
    }


    IEnumerator move()
    {
        yield return new WaitForSeconds(1f);
        GameObject.Find("Canvas/LOGO").SetActive(false);
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("Intro");
    }
}