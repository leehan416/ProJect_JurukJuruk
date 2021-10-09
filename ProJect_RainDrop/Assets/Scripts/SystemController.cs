/* 비내리기, 시간체크 등 기본 게임 시스템 조작 스크립트 => Main Scene*/

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class SystemController : MonoBehaviour {
    public GameObject rain; //빗방물 오브젝트 

    // 해상도 대응 변수
    private float width;
    private float height;

    void Awake()
    {
        // 추가 양동이 빗물 받아오기
        getOutWater();

        //사이즈 받아오기
        width = Convert.ToInt16(this.transform.GetComponent<RectTransform>().rect.width);
        height = Convert.ToInt16(this.transform.GetComponent<RectTransform>().rect.height);
    }

    private void Start()
    {
        StartCoroutine(RainSystem()); // 비오는 시스템  
        StartCoroutine(FixedSystem()); // 추가 양동이 물 채우는 시스템
    }

    // 유저가 게임 밖에 있을 때에 채워지는 물 추가
    void getOutWater()
    {
        // 유저가 마지막으로 접근했던 시간과 현재 시간의 차이를 계산하여 추가 양동이 물 추가
        DataBase.getLevels();
        DataBase.getWaterData();
        DataBase.getLateTime();
        for (int i = 0; i < DataBase.potWater.Length; i++)
        {
            if (DataBase.potLevel[i] > 0)
            {
                int value = CalculateUnderTime() / DataBase.locals[i].potCycle *
                            DataBase.perSecond[DataBase.potLevel[i]];
                DataBase.potWater[i] += value;

                if (DataBase.valuePotMax[i] <= DataBase.potWater[i]) // 최대보다 많을 때
                    DataBase.potWater[i] = Convert.ToInt32(DataBase.valuePotMax[DataBase.potLevel[i]]);
            }
        }

        // set Data
        DataBase.setWaterData();
        DataBase.setLateTime();
    }


    IEnumerator RainSystem()
    {
        // 비오는 시스템
        while (true)
        {
            yield return new WaitForSeconds(DataBase.locals[DataBase.nowLocal].rainCycle * ((UI_MainScene.isFever)
                ? 1 / DataBase.feverEfficiency
                : 1));
            Rainy();
        }
    }

    IEnumerator FixedSystem()
    {
        // 고정 빗물 수집 시스템 + 시간 계산 시스템
        // ingame => 초당 계산 
        // background (outGame) => 현실 시간 계산하여 더해줌.


        int index = 0; // 시간 (초) 변수
        int[] value = new int[DataBase.locals.Length]; // 각 지역에 대한 변수

        for (int i = 0; i < DataBase.locals.Length; i++) // 접속시 추가되는 버그 수정
            value[i] = 0;

        while (true)
        {
            yield return new WaitForSeconds(1f);
            index++; // 시간(초)

            //추가 양동이 시스템
            for (int local = 0; local < DataBase.locals.Length; local++)
            {
                // get Data
                DataBase.getWaterData();
                DataBase.getLevels();

                // 시간 증가에 따라 추가 양동이 물 증가
                if (value[local] < index / DataBase.locals[local].potCycle)
                {
                    value[local] = index / DataBase.locals[local].potCycle; // 추가하는 물의 중복 방지 처리
                    DataBase.potWater[local] += DataBase.perSecond[DataBase.potLevel[local]];
                }

                // 지역의 추가 양동이가 해금되어 있다면
                if (DataBase.potLevel[local] > 0)
                    if (DataBase.potWater[local] > DataBase.valuePotMax[local]) // 추가 양동이의 물이 최대 초과일 때
                        DataBase.potWater[local] =
                            Convert.ToInt32(DataBase.valuePotMax[DataBase.potLevel[local]]); // 물을 최대로 지정

                // set Data
                DataBase.setLateTime();
                DataBase.setWaterData();
            }

            // UI set
            UI_MainScene.updateWaterPot();
        }
    }

    void Rainy()
    {
        // 비오는 시스템
        Random random = new Random();

        // 극지방이라면 
        if (DataBase.nowLocal == 4)
        {
            // 눈 색상은 흰색
            rain.gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, .7f);
        }

        else
        {
            // 다른 지역은 각 지역 물 색상에 맞추어 지정
            rain.gameObject.GetComponent<Image>().color =
                DataBase.waterColors[DataBase.locals[DataBase.nowLocal].waterType];
        }


        if (!UI_MultiScene.instance.popupIsOn) // 종료 팝업이 뜨지 않았다면(예외 처리 => 코루틴이 TimeScale이 0 인 상태에서도 작동 할 수 있음)
            Instantiate(rain,
                new Vector2(random.Next(0, /* 해상도 대응 */ Convert.ToInt32(width)), /* 해상도 대응 */ height),
                Quaternion.identity,
                this.transform);
        // canvas size에 맞추어 난수 발생한 위치에 비 생성
    }

    // 시간 계산
    public static int CalculateUnderTime()
    {
        TimeSpan dateDiff = DateTime.Now - DataBase.lateTime;
        return Convert.ToInt32(dateDiff.TotalSeconds); // 초 차이
    }
}