using System;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class Rain : MonoBehaviour {
    public Sprite[] type = new Sprite[4];
    public Sprite[] bigtype = new Sprite[3];
    Random random = new Random();
    private bool isBig = false;

    void Start()
        //22 40
        //57 90
    {
        if (random.Next(0, 9) == 5)
        {
            //22 40
            isBig = true;
            gameObject.transform.localScale = new Vector3(57 / 33f, 90 / 60f, 0);
        }

        if (isBig) // 큰 빗물
            gameObject.GetComponent<Image>().sprite = bigtype[random.Next(0, 2)]; // 랜덤 이미지로 생성됨

        else //일반 빗물
            gameObject.GetComponent<Image>().sprite = type[random.Next(0, 3)]; // 랜덤 이미지로 생성됨


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

                if (DataBase.nowLocal == 1)
                    DataBase.water[1] += value; // 청정구역
                else if (DataBase.nowLocal == 3)
                    DataBase.water[2] += value; // 사막구역
                else
                    DataBase.water[0] += value; // 나머지 구역
                // 피버 버튼 등장 조건 검사 : 물탱크가 50 % 이상 찼을 때 50퍼센트 확률
                if (!DataBase.isFeverChecked && DataBase.getWaterTankPercent() > DataBase.feverDrop)
                {
                    if (random.Next(0, 10) > 5)
                    {
                        DataBase.isFeverChecked = true;
                        UI_MainScene.instance.setFeverbtn();
                    }
                }
            }
            else
            {
                int value = Convert.ToInt32(DataBase.valueMaxWater[DataBase.tankLevel] - DataBase.getAllWater());
                if (DataBase.nowLocal == 1) // 청정구역
                    DataBase.water[1] += value;
                else if (DataBase.nowLocal == 3) // 사막구역
                    DataBase.water[2] += value;
                else // 나머지 구역
                    DataBase.water[0] += value;
            }

            DataBase.setWaterData();
            UI_MultiScene.instance.updateWaterTank();
            UI_MultiScene.instance.setWaterCounter();
        }

        Destroy(this.gameObject); // 객체를 없앤다
    }
}