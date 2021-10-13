using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

// 외부 접근
public delegate void SetFeverbtn();

public delegate void UpdateWaterPot();

public class UI_MainScene : MonoBehaviour {
    // 외부 접근용도 delegate
    public static SetFeverbtn setFeverbtn = delegate { };
    public static UpdateWaterPot updateWaterPot = delegate { };

    private Text local; // local Text
    private Text emptyText;
    public static Slider waterPot; // 물탱크

    [Header("고양이")] public GameObject feverBtn; // 고양이 버튼 
    [Header("비오는 커버")] public GameObject feverCover;

    // 배경 sprite 
    [Header("메인 맵 배경")] public Sprite[] localBG = new Sprite[5];
    [Header("고양이 이미지")] public Sprite[] catImage = new Sprite[3];

    public static bool isFever = false; // fever 확인 변수 

    private Random _random = new Random();

    // 해상도 대응 변수
    private float width;
    private float height;

    private void Awake()
    {
        // 외부 스크립트에서 함수 접근 delegate 지정
        setFeverbtn = delegate { _setFeverbtn(); };
        updateWaterPot = delegate { _updateWaterPot(); };
        // Get ui
        local = GameObject.Find("Canvas/LocalBack/Local").GetComponent<Text>();
        emptyText = GameObject.Find("Canvas/N_EmptyExtraBottle/num").GetComponent<Text>();

        // 사이즈 받아오기
        width = UI_MultiScene.instance.transform.GetComponent<RectTransform>().rect.width / 1080;
        height = UI_MultiScene.instance.transform.GetComponent<RectTransform>().rect.height / 1920;
        waterPot = GameObject.Find("Canvas/ExtraBottles/PotSlider" + DataBase.nowLocal + "/mask/Slider")
            .GetComponent<Slider>();
    }

    void Start()
    {
        //현 지역 전용 pot 이외엔 모두 비활성화
        for (int i = 0; i < DataBase.locals.Length; i++)
            if (DataBase.nowLocal != i)
                GameObject.Find("Canvas/ExtraBottles/PotSlider" + i).SetActive(false);

        //현 지역의 pot의 레벨이 0이라면 비활성화
        if (DataBase.potLevel[DataBase.nowLocal] == 0)
        {
            GameObject.Find("Canvas/N_EmptyExtraBottle").SetActive(false);
            GameObject.Find("Canvas/ExtraBottles/PotSlider" + DataBase.nowLocal).SetActive(false);
        }

        // 비우기 버튼 텍스트 색상 변경
        emptyText.color = DataBase.waterColors[DataBase.locals[DataBase.nowLocal].waterType];

        //----
        DataBase.money = 1000000000;
        DataBase.setMoney();
        //----
        //dataGet
        DataBase.getWaterData();
        DataBase.getLevels();
        DataBase.getMoney();

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
            return;

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
        SpriteRenderer bg = GameObject.Find("BackGround").GetComponent<SpriteRenderer>();
        bg.sprite = localBG[DataBase.nowLocal];
    }

    // 피버 버튼 등장
    private void _setFeverbtn()
    {
        feverBtn.SetActive(true);
        int value = _random.Next(0, catImage.Length - 1);
        feverBtn.GetComponent<Button>().image.sprite = catImage[value];
        feverBtn.transform.localScale = new Vector3( /* 해상도 대응 */width, /* 해상도 대응 */height, 0);
    }

    //fever set
    public void clickFeverbtn()
    {
        // 피버 조건 초기화
        DataBase.savedWater[DataBase.nowLocal] = 0;
        DataBase.setWaterData();
        // 피버 조건 세팅
        feverBtn.SetActive(false);
        isFever = true;
        feverCover.SetActive(true);
        // 피버 타이머 시작
        StartCoroutine(feverTimer());
    }

    public void quit()
    {
        Application.Quit(); // 게임 종료
    }

    // 피버 타이머
    private IEnumerator feverTimer()
    {
        // 피버시간 체크
        for (int i = 0; i < DataBase.feverTime; i++)
            yield return new WaitForSeconds(1f);
        feverCover.SetActive(false);
        isFever = false;
    }
}