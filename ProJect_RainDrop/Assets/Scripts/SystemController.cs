/* 비내리기, 시간체크 등 기본 게임 시스템 조작 스크립트 => Main Scene*/

using System;
using System.Collections;
using UnityEngine;
using Random = System.Random;

public class SystemController : MonoBehaviour {
    public GameObject rain; //빗방물 오브젝트 

    //  public static DateTime realTime = DateTime.Now;

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            if (DataBase.potLevel[i] > 0)
            {
                DataBase.potWater[i] += System.Convert.ToInt32(CalculateUnderTime() / DataBase.potCycle[i]) *
                                        DataBase.perSecond[i];
                if (DataBase.potMax[i] < DataBase.potWater[i])
                {
                    DataBase.potWater[i] = DataBase.potMax[i];
                }
            }
        }

        DataBase.GetLevels();
        DataBase.GetWaterData();
        StartCoroutine(RainSystem());
        StartCoroutine(FixedSystem());
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
        // ingame => DataBase.potCycle 초당 계산 
        // background (outGame) => 현실 시간 계산하여 더해줌.

        //TODO 현실 시간 가져와서 계산.
        int index = 0;
        int[] value = new int[4];

        for (int i = 0; i < 4; i++) // 접속시 추가되는 버그 수정
            value[i] = 0;

        while (true)
        {
            yield return new WaitForSeconds(1f);
            index++;
            Debug.Log(index + "s");
            for (int local = 0; local < 4; local++)
            {
                if (value[local] < index / DataBase.potCycle[local])
                {
                    value[local] = index / DataBase.potCycle[local];
                    DataBase.potWater[local] += DataBase.perSecond[DataBase.potLevel[local]];
                    Debug.Log(local + " " + DataBase.potWater[local]);
                }

                if (DataBase.potLevel[local] > 0)
                {
                    if (DataBase.potWater[local] > DataBase.potMax[local])
                        DataBase.potWater[local] = DataBase.potMax[local];
                }

                DataBase.SetLateTime();
                DataBase.SetWaterData();
            }

            UIManager.instance.PotUpdate();
        }
    }

    void Rainy()
    {
        // 비오는 시스템
        Random random = new Random();
        short width = Convert.ToInt16(this.transform.GetComponent<RectTransform>().rect.width);
        short height = Convert.ToInt16(this.transform.GetComponent<RectTransform>().rect.height);
        Instantiate(rain,
            new Vector2(random.Next(0, width), height), Quaternion.identity,
            this.transform);
        // canvas size에 맞추어 난수 발생한 위치에 비 생성
    }

    public static int CalculateUnderTime()
    {
        DataBase.GetLateTime();
        TimeSpan dateDiff = DataBase.lateTime - DateTime.Now;
        return dateDiff.Seconds; // 초 차이
    }
}