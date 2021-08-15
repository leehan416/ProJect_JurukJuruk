using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class UI_MainScene : MonoBehaviour {
    public static bool isMain = true; // main scene 인지 확인하는 변수 (읽기 전용)

    private Text local; // local Text

    private static Slider waterPot; // 물탱크

    private GameObject feverBtn; // 고양이 버튼 

    // 배경 sprite 
    [Header("메인 맵 배경")] public Sprite[] localBG = new Sprite[4];
    [Header("고양이 이미지")] public Sprite[] catImage = new Sprite[3];

    void Start()
    {
        //Get UI
        local = GameObject.Find("Canvas/LocalBack/Local").GetComponent<Text>();
        waterPot = GameObject.Find("Canvas/BigBox/PotSlider" + DataBase.nowLocal + "/mask/Slider")
            .GetComponent<Slider>(); // PotTank
        feverBtn = GameObject.Find("Canvas/BigBox/FeverCats");

        //현 지역 전용 pot 이외엔 모두 비활성화
        for (int i = 0; i < 4; i++)
            if (DataBase.nowLocal != i)
                GameObject.Find("Canvas/BigBox/PotSlider" + i).SetActive(false);

        //현 지역의 pot의 레벨이 0이라면 비활성화
        if (DataBase.potLevel[DataBase.nowLocal] == 0)
        {
            GameObject.Find("Canvas/BigBox/EmptyExtraBottle").SetActive(false);
            GameObject.Find("Canvas/BigBox/PotSlider" + DataBase.nowLocal).SetActive(false);
        }

        //고양이 버튼 비활성화
        feverBtn.SetActive(false);

        //dataGet
        DataBase.GetWaterData();
        DataBase.GetLevels();
        DataBase.GetMoney();

        //sliderSet
        UI_MultiScene.instance.setWaterTank();
        setWaterPot();

        //TextSet
        setLocal();
        UI_MultiScene.instance.setMoney();

        setbackGround();
        UI_MultiScene.instance.setWaterCounter();

        //slider update
        updateWaterPot();
        UI_MultiScene.instance.updateWaterTank();
    }

    // pot (추가 양동이) value set
    public static void updateWaterPot()
    {
        DataBase.GetWaterData();
        waterPot.value = DataBase.potWater[DataBase.nowLocal];
    }

    // pot (추가 양동이) 초기 데이터 설정
    public void setWaterPot()
    {
        DataBase.GetWaterData();
        DataBase.GetLevels();
        waterPot.maxValue = DataBase.valuePotMax[DataBase.nowLocal]; // 촤댓값
        waterPot.minValue = 0f; // 최솟값
    }

    //pot (추가 양동이) 비우기
    public void emptyPot()
    {
        DataBase.GetWaterData();
        DataBase.GetLevels();

        //물탱크가 최대라면
        if (DataBase.valueMaxWater[DataBase.tankLevel] <= DataBase.AllWater())
        {
            return;
        }

        // 청정구역이라면 
        if (DataBase.nowLocal == 1) DataBase.cleanedWater += DataBase.potWater[DataBase.nowLocal];
        // 사막지역
        else if (DataBase.nowLocal == 3) DataBase.desertWater += DataBase.potWater[DataBase.nowLocal];
        // 나머지 지역
        else DataBase.uncleanedWater += DataBase.potWater[DataBase.nowLocal];

        // 물병 비우기
        if (DataBase.AllWater() > DataBase.valueMaxWater[DataBase.tankLevel])
        {
            // clean local
            if (DataBase.nowLocal == 1)
            {
                DataBase.cleanedWater -= DataBase.AllWater() - DataBase.valueMaxWater[DataBase.tankLevel];
            }
            //dessert local
            else if (DataBase.nowLocal == 3)
            {
                DataBase.desertWater -= DataBase.AllWater() - DataBase.valueMaxWater[DataBase.tankLevel];
            }
            // normal local
            else
            {
                DataBase.uncleanedWater -= DataBase.AllWater() - DataBase.valueMaxWater[DataBase.tankLevel];
            }
        }

        // value reset
        DataBase.potWater[DataBase.nowLocal] = 0;

        //data set
        DataBase.SetLateTime();
        DataBase.SetWaterData();

        //ui update
        updateWaterPot();
        UI_MultiScene.instance.updateWaterTank();
        UI_MultiScene.instance.setWaterCounter();
    }


    // 지역 text set
    public void setLocal()
    {
        local.text = DataBase.localName[DataBase.nowLocal];
    }

    // 배경 set
    public void setbackGround()
    {
        Image bg = GameObject.Find("Canvas/BackGround").GetComponent<Image>();
        bg.sprite = localBG[DataBase.nowLocal];
    }

    public void setFeverbtn()
    {
        Random random = new Random();
        feverBtn.SetActive(true);
        feverBtn.GetComponent<Button>().image.sprite = catImage[random.Next(0, catImage.Length - 1)];
    }

    public void clickFeverbtn()
    {
        feverBtn.SetActive(false);
    }
}