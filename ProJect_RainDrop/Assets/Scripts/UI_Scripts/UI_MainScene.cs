using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;


// setfever
//updateWaterPot


public delegate void SetFeverbtn();

public delegate void UpdateWaterPot();


public class UI_MainScene : MonoBehaviour {
    // 외부 접근용도 delegate
    public static SetFeverbtn setFeverbtn = delegate { };
    public static UpdateWaterPot updateWaterPot = delegate { };

    private Text local; // local Text

    public static Slider waterPot; // 물탱크

    private Text emptyText;

    [Header("고양이")] public GameObject feverBtn; // 고양이 버튼 

    [Header("비오는 커버")] public GameObject feverCover;

    // 배경 sprite 
    [Header("메인 맵 배경")] public Sprite[] localBG = new Sprite[4];
    [Header("고양이 이미지")] public Sprite[] catImage = new Sprite[3];

    // [HideInInspector] public bool popupIsOn = false;
    // public GameObject Rains; // 빗물 집합소
    public static bool isFever = false; // fever 확인 변수 

    private Random _random = new Random();


    private void Awake()
    {
        setFeverbtn = delegate { _setFeverbtn(); };
        updateWaterPot = delegate { _updateWaterPot(); };
        local = GameObject.Find("Canvas/LocalBack/Local").GetComponent<Text>();


        emptyText = GameObject.Find("Canvas/BigBox/N_EmptyExtraBottle/num").GetComponent<Text>();
    }

    void Start()
    {
        waterPot = GameObject.Find("Canvas/BigBox/PotSlider" + DataBase.nowLocal + "/mask/Slider")
            .GetComponent<Slider>();

        //현 지역 전용 pot 이외엔 모두 비활성화
        for (int i = 0; i < 4; i++)
            if (DataBase.nowLocal != i)
                GameObject.Find("Canvas/BigBox/PotSlider" + i).SetActive(false);

        //현 지역의 pot의 레벨이 0이라면 비활성화
        if (DataBase.potLevel[DataBase.nowLocal] == 0)
        {
            GameObject.Find("Canvas/BigBox/N_EmptyExtraBottle").SetActive(false);
            GameObject.Find("Canvas/BigBox/PotSlider" + DataBase.nowLocal).SetActive(false);
        }

        // 비우기 버튼 텍스트 색상 변경
        emptyText.color = DataBase.waterColors[DataBase.locals[DataBase.nowLocal].waterType];

        //dataGet
        DataBase.getWaterData();
        DataBase.getLevels();
        DataBase.getMoney();

        //피버 등장 판별
        if (DataBase.getWaterTankPercent() > DataBase.feverDrop) DataBase.isFeverChecked = true;

        //sliderSet
        UI_MultiScene.instance.setWaterTank();
        setWaterPot();

        //slider update
        updateWaterPot();
        UI_MultiScene.instance.updateWaterTank();

        //TextSet
        setLocal();
        UI_MultiScene.instance.setMoney();
        setbackGround();
        UI_MultiScene.instance.setWaterCounter();

        isFever = false;
    }

    // pot (추가 양동이) value set
    private void _updateWaterPot()
    {
        DataBase.getWaterData();
        emptyText.text = DataBase.potWater[DataBase.nowLocal].ToString();
        waterPot.value = DataBase.potWater[DataBase.nowLocal];
    }

    // pot (추가 양동이) 초기 데이터 설정
    public void setWaterPot()
    {
        DataBase.getWaterData();
        DataBase.getLevels();
        waterPot.maxValue = DataBase.valuePotMax[DataBase.potLevel[DataBase.nowLocal]]; // 촤댓값
        waterPot.minValue = 0f; // 최솟값
    }

    //pot (추가 양동이) 비우기
    public void emptyPot()
    {
        DataBase.getWaterData();
        DataBase.getLevels();

        //물탱크가 최대라면
        if (DataBase.valueMaxWater[DataBase.tankLevel] <= DataBase.getAllWater())
        {
            return;
        }

        DataBase.water[DataBase.locals[DataBase.nowLocal].waterType] += DataBase.potWater[DataBase.nowLocal];

        // 물병 비우기
        if (DataBase.getAllWater() > DataBase.valueMaxWater[DataBase.tankLevel])
        {
            DataBase.water[DataBase.locals[DataBase.nowLocal].waterType] -=
                DataBase.getAllWater() - DataBase.valueMaxWater[DataBase.tankLevel];
        }

        // value reset
        DataBase.potWater[DataBase.nowLocal] = 0;

        //data set
        DataBase.setLateTime();
        DataBase.setWaterData();

        //ui update
        updateWaterPot();
        UI_MultiScene.instance.updateWaterTank();
        UI_MultiScene.instance.setWaterCounter();
    }


    // 지역 text set
    private void setLocal()
    {
        local.text = DataBase.locals[DataBase.nowLocal].localName;
    }

    // 배경 set
    private void setbackGround()
    {
        Image bg = GameObject.Find("Canvas/BackGround").GetComponent<Image>();
        bg.sprite = localBG[DataBase.nowLocal];
    }


    private void _setFeverbtn()
    {
        feverBtn.SetActive(true);
        feverBtn.GetComponent<Button>().image.sprite = catImage[_random.Next(0, catImage.Length - 1)];
    }

    public void clickFeverbtn()
    {
        feverBtn.SetActive(false);
        isFever = true;
        feverCover.SetActive(true);
        StopCoroutine("feverTimer");
        StartCoroutine(feverTimer());
    }


    public void quit()
    {
        Application.Quit(); // 게임 종료
    }


    private IEnumerator feverTimer()
    {
        // 피버시간 체크
        if (isFever)
        {
            for (int i = 0; i < DataBase.feverTime; i++)
                yield return new WaitForSeconds(1f);

            feverCover.SetActive(false);
            isFever = false;
        }
        // 고양이 등장 시간 체크 
        else
        {
            for (int i = 0; i < DataBase.catSustainTime; i++)
                yield return new WaitForSeconds(1f);

            feverBtn.SetActive(false);
        }
    }
}