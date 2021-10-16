using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {
    float playerSpeed = 320f; // 1080기준

    public RuntimeAnimatorController[] skin = new RuntimeAnimatorController[6];

    private Animator animator;
    // public static bool leftClick;
    // public static bool rightClick;
// 해상도 대응 변수

    private void Awake()
    {
        animator = GetComponent<Animator>();
        // gameObject.GetComponent<SpriteRenderer>().sprite = skin[1].GetComponent<SpriteRenderer>().sprite;


        gameObject.GetComponent<Animator>().runtimeAnimatorController = skin[ DataBase.costume];
    }


    void Update()
    {
        // 유저 컨트롤
        if (Input.GetMouseButton(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                //UI중 터치된 부분이 없다면 실행
            }
            else
            {
                if (Input.mousePosition.x > Screen.width / 2)
                {
                    if (!DataBase.isReverse)
                    {
                        animator.SetBool("rightMove", true);
                        animator.SetBool("leftMove", false);
                        // rightClick = true;
                        // leftClick = false;
                        if (gameObject.transform.position.x < 500)
                            transform.Translate(1 * playerSpeed * Time.deltaTime, 0, 0);
                    }
                    else
                    {
                        animator.SetBool("rightMove", false);
                        animator.SetBool("leftMove", true);
                        // leftClick = true;
                        // rightClick = false;
                        if (gameObject.transform.position.x > -500)
                            transform.Translate(-1 * playerSpeed * Time.deltaTime, 0, 0);
                    }
                }
                else
                {
                    if (!DataBase.isReverse)
                    {
                        animator.SetBool("rightMove", false);
                        animator.SetBool("leftMove", true);
                        // leftClick = true;
                        // rightClick = false;
                        if (gameObject.transform.position.x > -500)
                            transform.Translate(-1 * playerSpeed * Time.deltaTime, 0, 0);
                    }
                    else
                    {
                        animator.SetBool("rightMove", true);
                        animator.SetBool("leftMove", false);
                        // rightClick = true;
                        // leftClick = false;
                        if (gameObject.transform.position.x < 500)
                            transform.Translate(1 * playerSpeed * Time.deltaTime, 0, 0);
                    }
                }
            }
            // return;
        }
        else
        {
            animator.SetBool("rightMove", false);
            animator.SetBool("leftMove", false);
            // leftClick = false;
            // rightClick = false;
        }
    }
}