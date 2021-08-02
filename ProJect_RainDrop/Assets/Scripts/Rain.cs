using System;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class Rain : MonoBehaviour {
    public Sprite[] type = new Sprite[4];
    Random random = new Random();

    void Start()
    {
        gameObject.GetComponent<Image>().sprite = type[random.Next(0, 3)]; // 랜덤 이미지로 생성됨
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -200), ForceMode2D.Impulse); // 땅으로 힘 추가
    }

    void OnCollisionEnter2D(Collision2D other) // 오브젝트 충돌시 실행되는 함수
    {
        if (other.collider.gameObject.tag.Equals("Pail")) // 양동이 접촉시
        {
            DataBase.GetWaterData();
            if (DataBase.valueMaxWater[DataBase.tankLevel] > DataBase.AllWater()) // 물탱크에 자리가 있으면 
            {
                if (DataBase.nowLocal == 1)
                    DataBase.cleanedWater += DataBase.valuePerDrop[DataBase.pailLevel]; // 청정구역
                else if (DataBase.nowLocal == 3)
                    DataBase.desertWater += DataBase.valuePerDrop[DataBase.pailLevel]; // 사막구역
                else
                    DataBase.uncleanedWater += DataBase.valuePerDrop[DataBase.pailLevel]; // 나머지 구역
            }

            DataBase.SetWaterData();
            UIManager.instance.WaterTankUpdate();
            UIManager.instance.SetMainText();
        }

        Destroy(this.gameObject); // 객체를 없앤다
    }
}