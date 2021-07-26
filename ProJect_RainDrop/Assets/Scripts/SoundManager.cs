using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {
    public static SoundManager instance;
    public AudioSource bgmSource = new AudioSource();
    public AudioSource fxSource = new AudioSource();
    public AudioClip[] musics = new AudioClip[1];
    public AudioClip[] fxs = new AudioClip[1];


    private void Start()
    {
        if (!instance) instance = this;
        else DestroyImmediate(this);
    }
}