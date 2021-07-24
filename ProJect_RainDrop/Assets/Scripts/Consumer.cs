using System;
using UnityEngine;

public class Consumer : MonoBehaviour {
    
    
    
    public int perLiter = 10;
    
    public bool isCleaned = false;
    
    public Sprite image;
    
    public string story;
    
    
    public static Consumer[] consumerList = new Consumer[3];
    

    void Start()
    {
        consumerList[0].perLiter = 100;
        consumerList[0].isCleaned = false;
    }
}