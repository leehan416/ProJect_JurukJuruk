/* 비내리기, 시간체크 등 기본 게임 시스템 조작 스크립트 => Main Scene*/

using System;
using System.Collections;
using UnityEngine;
using Random = System.Random;

public class SystemController : MonoBehaviour {
  
    public GameObject rain; //빗방물 오브젝트 

    public static DateTime realTime = DateTime.Now;

    void Start()
    {
        realTime = DateTime.Now; // 현재 시간 가져오기 
        StartCoroutine(RainSystem());
    }

    IEnumerator RainSystem()
    {
        // 비오는 시스템
        while (true)
        {
            yield return new WaitForSeconds(DataBase.rainCycle[DataBase.nowLocal]);
            Rainy();
        }
    }


    //private int latem=0;
    private int val = 0;

    IEnumerator FixedSystem()
    {
        // 고정 빗물 수집 시스템
        //TODO 현실 시간 가져와서 계산.
        while (DataBase.nowLocal > 0)
        {
            yield return new WaitForSeconds(1f);
            if (DataBase.potWater[DataBase.nowLocal] <= DataBase.potMax[DataBase.nowLocal])
            {
                DataBase.potWater[DataBase.nowLocal] += DataBase.perSecond[DataBase.nowLocal];
            }
        }
    }

    void Rainy()
    {
        // 비오는 시스템
        Random random = new Random();
        Instantiate(rain,
            new Vector2(random.Next(0, Convert.ToInt16(this.transform.GetComponent<RectTransform>().rect.width)),
                Convert.ToInt16(this.transform.GetComponent<RectTransform>().rect.height)), Quaternion.identity,
            this.transform);
        // canvas size에 맞추어 난수 발생한 위치에 비 생성
    }

    public static int CalculateUnderTime()
    {
        DateTime lateTime = Convert.ToDateTime(PlayerPrefs.GetString("", Convert.ToString(realTime)));
        realTime = DateTime.Now;
        TimeSpan dateDiff = lateTime - realTime;
        return dateDiff.Minutes; // 분 차이
    }
}