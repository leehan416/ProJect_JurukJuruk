/* 플레이어 조작관련 스크립트 */

// using UnityEditor.Rendering;

using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    [HideInInspector] public float playerSpeed = 300f;

    public static bool leftClick;
    public static bool rightClick;

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