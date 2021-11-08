using System;
using UnityEngine;
using Random = System.Random;

public class Rain : MonoBehaviour {
    public Sprite[] image = new Sprite[4];
    Random random = new Random();
    [HideInInspector] public bool isBig = false;
    private float width;
    private float height;

    void Start()
    {
        width = Convert.ToInt16(UI_MultiScene.instance.transform.GetComponent<RectTransform>().rect.width);
        height = Convert.ToInt16(UI_MultiScene.instance.transform.GetComponent<RectTransform>().rect.height);
        transform.SetParent(GameObject.Find("Rains").transform);
        gameObject.transform.localScale = new Vector3(1080 / width, 1920 / height, 0);

        try
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = image[random.Next(0, 3)]; // 랜덤 이미지로 생성됨
        }
        catch 
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = image[0]; // 랜덤 이미지로 생성됨
        }

        colorSet(DataBase.nowLocal);
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -200), ForceMode2D.Impulse); // 땅으로 힘 추가
    }


    void colorSet(int val)
    {
        if (val == 4)
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .7f);
        else
            gameObject.GetComponent<SpriteRenderer>().color =
                DataBase.waterColors[DataBase.locals[DataBase.nowLocal].waterType];
    }

    void OnCollisionEnter2D(Collision2D other) // 오브젝트 충돌시 실행되는 함수
    {
        if (other.collider.gameObject.tag.Equals("Pail")) // 양동이 접촉시
        {
            DataBase.getWaterData();
            if (DataBase.valueMaxWater[DataBase.tankLevel] + DataBase.valuePerDrop[DataBase.pailLevel] >
                DataBase.getAllWater()) // 물탱크에 자리가 있으면 
            {
                int value = DataBase.valuePerDrop[DataBase.pailLevel] * ((isBig) ? 5 : 1);

                // 물 채우기
                DataBase.water[DataBase.locals[DataBase.nowLocal].waterType] += value;
            }
            else
            {
                int value = Convert.ToInt32(DataBase.valueMaxWater[DataBase.tankLevel] - DataBase.getAllWater());
                DataBase.water[DataBase.locals[DataBase.nowLocal].waterType] += value;
            }

            // 피버
            if (!UI_MainScene.isFever)
                DataBase.savedWater[DataBase.nowLocal]++;
            if (DataBase.savedWater[DataBase.nowLocal] >= DataBase.locals[DataBase.nowLocal].feverWater)
                UI_MainScene.setFeverbtn();

            // 최종 확인 
            if (DataBase.getAllWater() > DataBase.valueMaxWater[DataBase.tankLevel])
                DataBase.water[DataBase.locals[DataBase.nowLocal].waterType] -=
                    DataBase.getAllWater() - DataBase.valueMaxWater[DataBase.tankLevel];
            DataBase.setWaterData();
            UI_MultiScene.instance.updateWaterTank();
            UI_MultiScene.instance.setWaterCounter();
        }

        Destroy(this.gameObject); // 객체를 없앤다
    }
}