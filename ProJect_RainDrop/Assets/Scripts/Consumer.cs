
using UnityEngine;

public class Consumer : MonoBehaviour {
    public int perLiter = 10;
    public bool isCleaned = false;
    public Sprite image;
    public string story;

    static public Consumer[] consumerList = new Consumer[4];
    public Sprite[] imageList = new Sprite[4];


    void Start()
    {
        for (int i = 0; i < consumerList.Length; i++)
        {
            consumerList[i].perLiter *= 2;
        }
    }
}