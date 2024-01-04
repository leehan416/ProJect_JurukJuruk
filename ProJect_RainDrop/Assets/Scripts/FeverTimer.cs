using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class FeverTimer : MonoBehaviour {

    public static FeverTimer instance;

    public static bool isFever = false; // fever 확인 변수

    private int time = 0;
    private void Start() {
        if (!instance)
            instance = this;
        else
            DestroyImmediate(this);
        try {
            DontDestroyOnLoad(this.gameObject);
        } catch {

        }
    }
    public IEnumerator feverTimer() {
        if (time == 30) {
            time = 0;
        }
        // 피버시간 체크
        for (; time < DataBase.feverTime; time++) {
            yield return new WaitForSeconds(1f);
        }
        isFever = false;

        try { // 다른 씬에서 피버가 끝난 경우 
            UI_MainScene.setFeverShader();
        } catch (NullReferenceException) {
        }
    }
}