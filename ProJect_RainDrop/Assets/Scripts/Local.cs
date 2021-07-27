using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class Local : MonoBehaviour {
//     public int cost;
//     public bool isLock;
//
//     Local(int val)
//     {
//         isLock = DataBase.isLocalLock[val];
//         cost = DataBase.localCost[val];
//     }
//
//     public static Local[] local = new Local[4];
//
//     void Start()
//     {
//         for (int i = 1; i < local.Length; i++)
//             local[i] = new Local(i);
//         local[0].isLock = false;
//     }
// }