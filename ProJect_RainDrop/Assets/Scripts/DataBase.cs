/* 총괄 DataBase */

using System;
using UnityEngine;

// pail => user pot 
// pot => extra pot 

public class DataBase : MonoBehaviour {
    // meony value
    public static long money = 0;

    // 물 판매량
    public static int[] soldWater = {0, 0, 0, 0};

    //Water value
    // 0 = uncleaned 1 = cleaned 2 = dessert 3 = snow
    public static long[] water = {0, 0, 0, 0};

    // 수집한 물의 양 => 피버중에는 모이지 않음
    public static int[] savedWater = {0, 0, 0, 0, 0};


    //public static long maxWater = 10000; // 최대 양
    public static int[] potWater = new int[5]; // 양동이 빗물양 [지역별]

    //--------------------------------------------------------
    public static int pailLevel = 1; // 양동이 레벨
    public static int tankLevel = 1; // 물 저장소 레벨
    public static int[] potLevel = {0, 0, 0, 0, 0}; // new int[5]; // 양동이 레벨[지역별]

    public static int cleanLevel = 1; //물 정화기 레벨

    public static int nowLocal = 0; // 현 위치

    public static int costume = 0; // 현 코스튬
    public static bool[] isCostumeLock = {false, true, true, true, true, true}; // 코스튬 해금


    // public static Consumer[] consumerList = new Consumer[3];

    //--------------------------------------------------------

    //Time value

    public static DateTime lateTime;

    //--------------------------------------------------------
    //Setting value
    public static float bgmVol = .7f;
    public static float fxVol = .7f;
    public static bool isReverse = false;


    //--------------------------------------------------------
    // System Value
    public static Local[] locals =
    {
        new Local("우리집 마당", 1f, 5, 0, 50, 0, false),
        new Local("시골집 뒷마당", 1f, 5, 1, 50, 5000, true),
        new Local("아마존 캠프", .5f, 4, 0, 100, 10000, true),
        new Local("피라미드 앞", 2.5f, 20, 2, 20, 20000, true),
        new Local("이글루 앞", .5f, 4, 3, 100, 40000, true),
    };


    public static Consumer[] consumers =
    {
        new Consumer(1000, 100, 0, false,
            "목말라 죽겠어. 물 좀 파는거 어때?\n어느 물이라도 좋아."), // 목마른 사람
        new Consumer(3000, 400, 0, true,
            "농사라는게 물이 좀 많이 들어야지.\n많으면 많을수록 좋네~", 30000, 3000), // 농부
        new Consumer(1000, 200, 1, false,
            "뭐, 물을 살 의향이 있습니다만.\n아무거나 취급하진 않습니다."), // 깐깐한 사람
        new Consumer(3000, 700, 1, true,
            "포항샘물입니다. 이정도 품질의 물이라니...!\n가능한 만큼 구매하고싶습니다! ", 30000, 5000), // 물 판매업자
        new Consumer(1000, 500, 2, false,
            "이맘때쯤엔 늘 고향이 그리워지지...\n자네, 혹시 사막의 물을 가지고 있나?"), // 사막 부자
        new Consumer(3000, 2000, 2, true,
            "흔히 보기 힘든걸 판다고 들었는데,\n물건 좀 볼 수 있을지?", 20000, 8000), // 수집가
        new Consumer(3000, 700, 3, false,
            "집을 보수할 때가 되어서.\n모아둔 눈, 안 쓰면 팔지 않을래?"), // 에스키모
        new Consumer(5000, 1500, 3, true,
            "크워어엉. 쿠워엉.\n(요즘엔 발 붙일 땅도 사라져가.)", 30000, 10000), // 북극곰
        new Consumer(1000, 0, -1, false,
            "... ... ."), // 거지
        new Consumer(5000, 800, 0, true,
            "역시 빗물의 오염도도 눈에 띄게 증가했습니다.\n모든 상황이 갈수록 악화되는군요.", 50000, 15000), // 과학자1
        new Consumer(5000, 1300, 1, true,
            "아직도 보존된 지역이 있다는게 놀랍군.\n국가 차원의 관리를 검토할 필요가 있겠어.", 50000, 20000), // 과학자2
        new Consumer(3000, 2200, 2, true,
            "그럴 수 있다면 시간을 돌리고 싶구만.\n입 아프게 경고한들 들어먹지도 않더니 말이야", 50000, 25000), // 과학자3
        new Consumer(7000, 1800, 3, true,
            "이 지경이 된다는걸 이전 세대에 전해줄수만 있다면\n... 그들은 조금 더 환경을 생각해줬을까요.", 50000, 30000), // 과학자4
    };

    // water Color 
    public static Color[] waterColors =
    {
        new Color(112 / 255f, 193 / 255f, 231 / 255f, .7f),
        new Color(153 / 255f, 222 / 255f, 224 / 255f, .7f),
        new Color(221 / 255f, 190 / 255f, 160 / 255f, .7f),
        new Color(178 / 255f, 178 / 255f, 178 / 255f, .7f)
    };

    public static Color[] feverColors =
    {
        new Color(0f, 0f, 0f, 1f),
        new Color(0f, 0f, 0f, 1f),
        new Color(0f, 0f, 0f, 1f),
        new Color(110 / 255f, 48 / 255f, 0 / 255f, 1f),
        new Color(15 / 255f, 135 / 255f, 1f, 1f)
    };

    // pail
    public static int[] valuePerDrop = {10, 15, 20, 23, 26, 28, 30, 32, 34, 36, 38, 40};
    public static int[] upgradePail = {0, 200, 300, 500, 700, 1000, 1500, 2200, 3000, 3900, 4900, 6000};

    //tank
    public static int[] valueMaxWater =
        {3000, 5000, 7000, 9000, 12000, 15000, 20000, 27000, 34000, 43000, 52000, 64000};

    public static int[] upgradeTank = {0, 1000, 1200, 1500, 2000, 2700, 3400, 4500, 5400, 6400, 7200, 9000};

    //clean
    public static int[] valueCleanWater = {1, 2, 3, 5, 7, 10, 15, 20};
    public static int[] upgradeClean = {0, 100, 200, 300, 500, 700, 1000, 1500};

    //pot
    public static int[] unLockPot = {1000, 3000, 4000, 5000, 6000};
    public static int[] perSecond = {5, 7, 10, 12, 13, 14, 15, 16};
    public static float[] valuePotMax = {1800, 2500, 3600, 4300, 4700, 5000, 5400, 5800};
    public static int[] upgradePot = {0, 300, 400, 500, 600, 700, 800, 900};

    //fever
    public static int feverTime = 30;
    public static float feverEfficiency = 10; // 빗물 내리는 시간 줄여주는 비율

    //gacha
    public static int gachaCost = 3000;

    //--------------------------------------------------------

    public static void setMoney()
    {
        PlayerPrefs.SetString("Money", Convert.ToString(money));
    }

    public static void getMoney()
    {
        money = Convert.ToInt64(PlayerPrefs.GetString("Money", "0"));
    }

    public static long getAllWater() // 물 총량 return
    {
        long value = 0;
        for (int i = 0; i < water.Length; i++)
            value += water[i];
        return value;
    }

    public static void setWaterData()
    {
        for (int i = 0; i < water.Length; i++)
            PlayerPrefs.SetString("Water" + i, Convert.ToString(water[i]));
        for (int i = 0; i < potWater.Length; i++)
            PlayerPrefs.SetInt("PotWater" + i, potWater[i]);
        for (int i = 0; i < soldWater.Length; i++)
            PlayerPrefs.SetInt("SoldWater" + i, soldWater[i]);
        for (int i = 0; i < savedWater.Length; i++)
            PlayerPrefs.SetInt("SavedWater" + i, savedWater[i]);
    }

    public static void getWaterData()
    {
        for (int i = 0; i < water.Length; i++)
            water[i] = Convert.ToInt64(PlayerPrefs.GetString("Water" + i, "0"));
        for (int i = 0; i < potWater.Length; i++)
            potWater[i] = PlayerPrefs.GetInt("PotWater" + i, 0);
        for (int i = 0; i < soldWater.Length; i++)
            soldWater[i] = PlayerPrefs.GetInt("SoldWater" + i, 0);
        for (int i = 0; i < savedWater.Length; i++)
            savedWater[i] = PlayerPrefs.GetInt("SavedWater" + i, 0);
    }


    public static void setLevels()
    {
        PlayerPrefs.SetInt("PailLevel", pailLevel);
        PlayerPrefs.SetInt("TankLevel", tankLevel);
        PlayerPrefs.SetInt("CleanLevel", cleanLevel);
        for (int i = 0; i < potLevel.Length; i++)
            PlayerPrefs.SetInt("PotLevel" + i, potLevel[i]);
    }

    public static void getLevels()
    {
        pailLevel = PlayerPrefs.GetInt("PailLevel", 0);
        tankLevel = PlayerPrefs.GetInt("TankLevel", 0);
        cleanLevel = PlayerPrefs.GetInt("CleanLevel", cleanLevel);
        for (int i = 0; i < potLevel.Length; i++)
            potLevel[i] = PlayerPrefs.GetInt("PotLevel" + i, 0);
    }

    public static void getSettingVal()
    {
        bgmVol = PlayerPrefs.GetFloat("BgmVol", .7f);
        fxVol = PlayerPrefs.GetFloat("FxVol", .4f);
        isReverse = Convert.ToBoolean(PlayerPrefs.GetInt("IsReverse", 0));
    }

    public static void setSettingVal()
    {
        PlayerPrefs.SetFloat("BgmVol", bgmVol);
        PlayerPrefs.SetFloat("FxVol", fxVol);
        PlayerPrefs.SetInt("IsReverse", Convert.ToInt32(isReverse));
        // Debug.Log(PlayerPrefs.GetInt("IsReverse"));
    }


    public static void getLocalData(int val)
    {
        if (val == 0) return;
        locals[val].isLock = Convert.ToBoolean(PlayerPrefs.GetInt("local+" + val + "_isLock", 1));
    }

    public static void setLocalData(int val)
    {
        PlayerPrefs.SetInt("local+" + val + "_isLock", (locals[val].isLock) ? 1 : 0);
    }


    public static void getLateTime()
    {
        lateTime = Convert.ToDateTime(PlayerPrefs.GetString("LateTime", Convert.ToString(DateTime.Now)));
    }

    public static void setLateTime()
    {
        PlayerPrefs.SetString("LateTime", Convert.ToString(DateTime.Now));
    }

    public static void setCostume()
    {
        PlayerPrefs.SetInt("Costume", costume);
        for (int i = 1; i < isCostumeLock.Length; i++)
            PlayerPrefs.SetInt("IsCostumeLock" + i, Convert.ToInt32(isCostumeLock[i]));
    }

    public static void getCoustume()
    {
        costume = PlayerPrefs.GetInt("Costume", 0);
        for (int i = 1; i < isCostumeLock.Length; i++)
            isCostumeLock[i] = Convert.ToBoolean(PlayerPrefs.GetInt("IsCostumeLock" + i, 1));
    }

    public static void getConsumerLock()
    {
        for (int i = 0; i < consumers.Length; i++)
        {
            if (!consumers[i].isLock) // 이미 해금되어 있으면
                continue; // 건너뛰기
            else
                consumers[i].isLock = Convert.ToBoolean(PlayerPrefs.GetInt("IsConsumerLock" + i, 1));
        }
    }

    public static void setConsumerLock()
    {
        for (int i = 0; i < consumers.Length; i++)
        {
            PlayerPrefs.SetInt("IsConsumerLock" + i, Convert.ToInt32(consumers[i].isLock));
        }
    }
}

// Data Class

public class Local {
    public String localName; // 지역이름
    public int potCycle; // 추가 양동이 사이클
    public float rainCycle; // 빗물 사이클
    public int waterType; // 떨어지는 빗물 종류
    public int cost; // 해금 가격
    public bool isLock; // 잠겨있음?
    public int feverWater;

    public Local(String _localName, float _rainCycle, int _potCycle, int _waterType, int _feverWater, int _cost,
        bool _isLock)
    {
        localName = _localName;
        rainCycle = _rainCycle;
        potCycle = _potCycle;
        feverWater = _feverWater;
        waterType = _waterType;
        cost = _cost;
        isLock = _isLock;
    }
}

public class Consumer {
    public int perWater; // 판매 물량
    public int perCell; // 판매 비용
    public int waterType; // 판매 물 종류
    public bool isLock; // 잠겨있음?
    public int limitOption;
    public int cost;
    public string text;

    public Consumer(int _perWater, int _perCell, int _waterType, bool _isLock, string _text)
    {
        perWater = _perWater;
        perCell = _perCell;
        waterType = _waterType; // -1 = 전부다 받음
        isLock = _isLock;
        text = _text;
    }

    public Consumer(int _perWater, int _perCell, int _waterType, bool _isLock, string _text, int _limitOption,
        int _cost)
    {
        perWater = _perWater;
        perCell = _perCell;
        waterType = _waterType; // -1 = 전부다 받음
        isLock = _isLock;
        limitOption = _limitOption;
        cost = _cost;
        text = _text;
    }
}