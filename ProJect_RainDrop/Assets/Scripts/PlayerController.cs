/* 플레이어 조작관련 스크립트 */

using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {
    float playerSpeed = 320f; // 1080기준

    public static bool leftClick;
    public static bool rightClick;


    private void Start()
    {
        // UI_MultiScene.instance.transform.GetComponent<RectTransform>().rect.width 
        float width = UI_MultiScene.instance.transform.GetComponent<RectTransform>().rect.width;
        float height = UI_MultiScene.instance.transform.GetComponent<RectTransform>().rect.height;
        playerSpeed = width / 3.375f;
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(65 * width / 1080, 190 * height / 1920);
        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, 6f * height / 1920);
    }

    void Update()
    {
        float width = UI_MultiScene.instance.gameObject.GetComponent<RectTransform>().rect.width;
        if (Input.GetMouseButton(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                //UI중 터치된 부분이 없다면 실행
            }
            else
            {
                if (Input.mousePosition.x > width / 2)
                {
                    if (!DataBase.isReverse)
                    {
                        rightClick = true;
                        leftClick = false;
                        if (gameObject.transform.position.x < width)
                            transform.Translate(1 * playerSpeed * Time.deltaTime, 0, 0);
                    }
                    else
                    {
                        leftClick = true;
                        rightClick = false;
                        if (gameObject.transform.position.x > 0)
                            transform.Translate(-1 * playerSpeed * Time.deltaTime, 0, 0);
                    }
                }
                else
                {
                    if (!DataBase.isReverse)
                    {
                        leftClick = true;
                        rightClick = false;
                        if (gameObject.transform.position.x > 0)
                            transform.Translate(-1 * playerSpeed * Time.deltaTime, 0, 0);
                    }
                    else
                    {
                        rightClick = true;
                        leftClick = false;
                        if (gameObject.transform.position.x < width)
                            transform.Translate(1 * playerSpeed * Time.deltaTime, 0, 0);
                    }
                }
            }

            return;
        }
        else
        {
            leftClick = false;
            rightClick = false;
        }
    }
}