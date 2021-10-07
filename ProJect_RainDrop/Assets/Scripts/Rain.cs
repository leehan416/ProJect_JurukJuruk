using System;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class Rain : MonoBehaviour {
    public Sprite[] type = new Sprite[4];
    public Sprite[] typeSnow = new Sprite[4];
    public Sprite[] bigtype = new Sprite[3];
    public Sprite[] bigtypeSnow = new Sprite[2];
    Random random = new Random();
    private bool isBig = false;

    void Start()
    {
        transform.SetParent(GameObject.Find("Canvas/Rains").transform);
        if (random.Next(0, 9) == 5)
        {
            //22 40
            isBig = true;
        }

        if (DataBase.nowLocal == 4)
        {
            if (isBig) // 큰 눈
            {
                gameObject.GetComponent<Image>().sprite = bigtypeSnow[random.Next(0, 2)]; // 랜덤 이미지로 생성됨
                gameObject.transform.localScale = new Vector3(65 / 33f, 65 / 60f, 0);
            }

            else //일반 눈
            {
                gameObject.GetComponent<Image>().sprite = typeSnow[random.Next(0, 3)]; // 랜덤 이미지로 생성됨
                gameObject.transform.localScale = new Vector3(34 / 33f, 34 / 60f, 0);
            }
        }

        else
        {
            if (isBig) // 큰 빗물
            {
                gameObject.GetComponent<Image>().sprite = bigtype[random.Next(0, 2)]; // 랜덤 이미지로 생성됨
                gameObject.transform.localScale = new Vector3(57 / 33f, 90 / 60f, 0);
            }

            else //일반 빗물
                gameObject.GetComponent<Image>().sprite = type[random.Next(0, 3)]; // 랜덤 이미지로 생성됨
        }


        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -200), ForceMode2D.Impulse); // 땅으로 힘 추가
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
            if (DataBase.savedWater[DataBase.nowLocal] >= DataBase.feverWater[DataBase.nowLocal])
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