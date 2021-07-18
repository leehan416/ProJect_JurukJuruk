using UnityEngine;

public class Consumer : MonoBehaviour {
    public int perLiter = 10;
    public bool isCleaned = false;
    public Sprite image;
    public string story;

    public static Consumer[] consumerList = new Consumer[3];
    //public Sprite[] imageList = new Sprite[];
}