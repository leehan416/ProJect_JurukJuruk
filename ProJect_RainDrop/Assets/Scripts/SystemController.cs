/* 비내리기, 시간체크 등 기본 게임 시스템 조작 스크립트 => Main Scene*/

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class SystemController : MonoBehaviour {
    public GameObject rain; //빗방물 오브젝트 


    private Color[] color = new Color[4];
    //  public static DateTime realTime = DateTime.Now;

    void Awake()
    {
        color[0] = new Color(112 / 255f, 193 / 255f, 231 / 255f, .7f);
        color[1] = new Color(153 / 255f, 222 / 255f, 224 / 255f, .7f);
        color[2] = new Color(112 / 255f, 193 / 255f, 231 / 255f, .7f);
        color[3] = new Color(221 / 255f, 190 / 255f, 160 / 255f, .7f);

        DataBase.getLevels();
        DataBase.getWaterData();
        DataBase.getLateTime();
        for (int i = 0; i < 4; i++)
        {
            if (DataBase.potLevel[i] > 0)
            {
                // Debug.Log(CalculateUnderTime());
                int value = CalculateUnderTime() / DataBase.potCycle[i] * DataBase.perSecond[DataBase.potLevel[i]];
                // Debug.Log(value);
                DataBase.potWater[i] += value;

                if (DataBase.valuePotMax[i] <= DataBase.potWater[i])
                {
                    DataBase.potWater[i] = Convert.ToInt32(DataBase.valuePotMax[DataBase.potLevel[i]]);
                }
            }
        }

        DataBase.setWaterData();
        DataBase.setLateTime();

        StartCoroutine(RainSystem());
        StartCoroutine(FixedSystem());
    }

    IEnumerator RainSystem()
    {
        // 비오는 시스템
        while (true)
        {
            yield return new WaitForSeconds(DataBase.rainCycle[DataBase.nowLocal] * ((UI_MainScene.isFever)
                ? 1 / DataBase.feverEfficiency
                : 1));
            Rainy();
        }
    }

    IEnumerator FixedSystem()
    {
        // 고정 빗물 수집 시스템 + 시간 계산 시스템
        // ingame => DataBase.potCycle 초당 계산 
        // background (outGame) => 현실 시간 계산하여 더해줌.

        int index = 0;
        int[] value = new int[4];

        for (int i = 0; i < 4; i++) // 접속시 추가되는 버그 수정
            value[i] = 0;

        while (true)
        {
            yield return new WaitForSeconds(1f);
            index++;
            //추가 양동이 시스템
            for (int local = 0; local < 4; local++)
            {
                DataBase.getWaterData();

                if (value[local] < index / DataBase.potCycle[local])
                {
                    value[local] = index / DataBase.potCycle[local];
                    DataBase.potWater[local] += DataBase.perSecond[DataBase.potLevel[local]];
                    // Debug.Log(local);
                }

                if (DataBase.potLevel[local] > 0)
                {
                    if (DataBase.potWater[local] > DataBase.valuePotMax[local])
                        DataBase.potWater[local] = Convert.ToInt32(DataBase.valuePotMax[local]);
                }

                DataBase.setLateTime();
                DataBase.setWaterData();
            }

            // UIManager.instance.PotUpdate();
            UI_MainScene.instance.updateWaterPot();
        }
    }

    void Rainy()
    {
        // 비오는 시스템
        Random random = new Random();
        rain.gameObject.GetComponent<Image>().color = color[DataBase.nowLocal];
        short width = Convert.ToInt16(this.transform.GetComponent<RectTransform>().rect.width);
        short height = Convert.ToInt16(this.transform.GetComponent<RectTransform>().rect.height);

        Instantiate(rain,
            new Vector2(random.Next(0, width), height), Quaternion.identity,
            this.transform);
        // canvas size에 맞추어 난수 발생한 위치에 비 생성
    }

    public static int CalculateUnderTime()
    {
        TimeSpan dateDiff = DateTime.Now - DataBase.lateTime;
        return Convert.ToInt32(dateDiff.TotalSeconds); // 초 차이
    }
}