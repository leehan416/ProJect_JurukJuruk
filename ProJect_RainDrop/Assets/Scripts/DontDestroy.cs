using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour {
    public static DontDestroy instance;
    private void Awake()
    {
        if (!instance) instance = this;
        else DestroyImmediate(this);
        DontDestroyOnLoad(this.gameObject);
    }
}