using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {
    public static SoundManager instance;
    public AudioSource bgmSource = new AudioSource();
    public AudioSource rainSource = new AudioSource();
    public AudioSource fxSource = new AudioSource();

    public AudioClip[] musics = new AudioClip[5];
    public AudioClip rain;// = new AudioClip();
    public AudioClip[] fxs = new AudioClip[2];




    private void Start()
    {
        if (!instance) instance = this;
        else DestroyImmediate(this);
        
        DataBase.getSettingVal();
        bgmSource.volume = DataBase.bgmVol;
        rainSource.volume = DataBase.fxVol;
        fxSource.volume = DataBase.fxVol;
        rainSource.clip = rain;
        rainSource.Play();
    }



    public void playMusic(int value ) {
        bgmSource.clip = musics[value];
        fxSource.clip = fxs[0];
        bgmSource.Play();
        fxSource.Play();
    }
}