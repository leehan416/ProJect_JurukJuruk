/* 비내리기, 시간체크 등 기본 게임 시스템 조작 스크립트 => Main Scene*/

using System;
using System.Collections;
using UnityEngine;
using Random = System.Random;

public class SystemController : MonoBehaviour {
    public GameObject rain; //빗방물 오브젝트 

    String realTime = System.DateTime.Now.ToString(); // 현실 시간 가져옴 


    private void Start()
    {
        realTime = System.DateTime.Now.ToString("yyyy:MM:dd:HH:mm"); // 현재 시간 가져오기 
        StartCoroutine(RainSystem());
    }


    void CalculateUnderTime()
    {
        // 1일 이상은 계산하지 않음.
        String lateTime = PlayerPrefs.GetString("", realTime); // 마지막 시간 가져오기

        int timeDistance = 0; // 시간차 변수 


        timeDistance +=
            (Convert.ToInt32(realTime.Split(':')[3]) > Convert.ToInt32(lateTime.Split(':')[3]))
                ? (Convert.ToInt32(realTime.Split(':')[3]) - Convert.ToInt32(lateTime.Split(':')[3]))
                : (Convert.ToInt32(realTime.Split(':')[3]) + (60 - Convert.ToInt32(lateTime.Split(':')[3])));
        //for 문 돌려서 반복할까?

        //DataBase.potMax[DataBase.nowLocal]

        
        
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

    IEnumerator FixedSystem()
    {
        // 고정 빗물 수집 시스템
        //TODO 현실 시간 가져와서 계산.
        while (DataBase.nowLocal > 0)
        {
            yield return new WaitForSeconds(1f);
            DataBase.savedWater += Convert.ToInt16(DataBase.perSecond);
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
}