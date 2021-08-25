using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_LogoScene : MonoBehaviour {
    private void Start()
    {
        StartCoroutine(move());
    }


    IEnumerator move()
    {
        yield return new WaitForSeconds(1f);
        // GameObject.Find("Canvas/LOGO").SetActive(false);
        for (int i = 10; i > 1; i--)
        {
            yield return new WaitForSeconds(.1f);
            GameObject.Find("Canvas/LOGO").GetComponent<Image>().color =
                new Color(255 / 255f, 255 / 255f, 255 / 255f, (i * 10) / 100f);
        }

        GameObject.Find("Canvas/LOGO").SetActive(false);

        SceneManager.LoadScene("Intro");
    }
}