using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_IntroScene : MonoBehaviour {
    public void IntroMoveScene()
    {
        SoundManager.instance.bgmSource.clip = SoundManager.instance.musics[0];
        SoundManager.instance.bgmSource.Play();
        UI_MultiScene.instance.moveScene("Main");
    }
}