using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimation : MonoBehaviour {
    public Sprite[] idle_0 = new Sprite[2];
    public Sprite[] idle_1 = new Sprite[2];
    public Sprite[] idle_2 = new Sprite[2];
    public Sprite[] idle_3 = new Sprite[2];
    public Sprite[] idle_4 = new Sprite[2];
    public Sprite[] idle_5 = new Sprite[2];

    public Sprite[] right_0 = new Sprite[3];
    public Sprite[] right_1 = new Sprite[3];
    public Sprite[] right_2 = new Sprite[3];
    public Sprite[] right_3 = new Sprite[3];
    public Sprite[] right_4 = new Sprite[3];
    public Sprite[] right_5 = new Sprite[3];

    public Sprite[] left_0 = new Sprite[3];
    public Sprite[] left_1 = new Sprite[3];
    public Sprite[] left_2 = new Sprite[3];
    public Sprite[] left_3 = new Sprite[3];
    public Sprite[] left_4 = new Sprite[3];
    public Sprite[] left_5 = new Sprite[3];


    Sprite[][] idle = new Sprite[6][];
    Sprite[][] leftAnimation = new Sprite[6][];
    Sprite[][] rightAnimation = new Sprite[6][];

    bool isLeftAnimationing = false;
    bool isRightAnimationing = false;
    bool isIdleAnimationing = false;

    Image obj;

    float frameSec = .2f;

    private void Start()
    {
        obj = GameObject.Find("Canvas/Player").GetComponent<Image>();
        DataBase.getCoustume();

        spriteSet();
    }

    private void Update()
    {
        // DataBase.costume = 4;
        if ((!PlayerController.leftClick && !PlayerController.rightClick) && !isIdleAnimationing)
        {
            StopAll();
            StartCoroutine(Idle());
        }
        else if (PlayerController.leftClick && !isLeftAnimationing)
        {
            StopAll();
            StartCoroutine(LeftAnimation());
        }
        else if (PlayerController.rightClick && !isRightAnimationing)
        {
            StopAll();
            StartCoroutine(RightAnimation());
        }
    }

    // 진행중인 애니메이션 중지
    void StopAll()
    {
        StopCoroutine(Idle());
        StopCoroutine(LeftAnimation());
        StopCoroutine(RightAnimation());

        isLeftAnimationing = false;
        isRightAnimationing = false;
        isIdleAnimationing = false;
    }

    // 왼쪽 이동시 애니메이션
    IEnumerator LeftAnimation()
    {
        isLeftAnimationing = true;
        for (int i = 0; PlayerController.leftClick; i++)
        {
            yield return new WaitForSeconds(frameSec);
            obj.sprite = leftAnimation[DataBase.costume][i % 3];
            // pail.sprite = leftPotAnimation[i % leftPotAnimation.Length];
        }
    }

    //오른쪽 이동시 애니메이션
    IEnumerator RightAnimation()
    {
        isRightAnimationing = true;
        for (int i = 0; PlayerController.rightClick; i++)
        {
            yield return new WaitForSeconds(frameSec);
            obj.sprite = rightAnimation[DataBase.costume][i % 3];
            // pail.sprite = rightPotAnimation[i % rightPotAnimation.Length];
        }
    }

    //정지 애니메이션
    IEnumerator Idle()
    {
        isIdleAnimationing = true;
        for (int i = 0; !PlayerController.leftClick && !PlayerController.rightClick; i++)
        {
            yield return new WaitForSeconds(frameSec);
            obj.sprite = idle[DataBase.costume][i % 2];
            //  pail.sprite = idlePot[i % idlePot.Length];
        }
    }


    void spriteSet()
    {
        idle[0] = idle_0;
        idle[1] = idle_1;
        idle[2] = idle_2;
        idle[3] = idle_3;
        idle[4] = idle_4;
        idle[5] = idle_5;
        //--------------------------------
        rightAnimation[0] = right_0;
        rightAnimation[1] = right_1;
        rightAnimation[2] = right_2;
        rightAnimation[3] = right_3;
        rightAnimation[4] = right_4;
        rightAnimation[5] = right_5;
        //--------------------------------
        leftAnimation[0] = left_0;
        leftAnimation[1] = left_1;
        leftAnimation[2] = left_2;
        leftAnimation[3] = left_3;
        leftAnimation[4] = left_4;
        leftAnimation[5] = left_5;
    }
}