using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DontDestroy_Rains : MonoBehaviour {

    [HideInInspector]
    public static DontDestroy_Rains instance;

    [HideInInspector]
    public GameObject rains;

    int getNumRains() {
        return GetComponentsInChildren<Rain>().Length;
    }

    void Awake() {
        if (!instance)
            instance = this;
        else
            DestroyImmediate(this);
        try {
            DontDestroyOnLoad(this.gameObject);
            rains = this.gameObject;
        } catch { }

        
     }
    

    public void removeRains() {
        for (int i = 0; i < getNumRains(); i++)
            Destroy(GetComponentsInChildren<Rain>()[i].gameObject);
    }


    public void addPower() {
        for (int i = 0; i < getNumRains();  i++) 
            GetComponentsInChildren<Rain>() [i].GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -250), ForceMode2D.Impulse); 
    }
}
