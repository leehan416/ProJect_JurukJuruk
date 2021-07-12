using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Consumer : MonoBehaviour {




    public Consumer[] consumer = new Consumer[4];
    private void Start()
    {
        
    }


    public int perLiter;
    public bool isCleaned;
    public Sprite image;
    public string story;
}