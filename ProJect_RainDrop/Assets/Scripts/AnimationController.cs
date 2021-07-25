using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationController : MonoBehaviour {
    private float frameSec = .2f;

    private Image player;
    public Sprite[] idle = new Sprite[2];
    public Sprite[] leftAnimation = new Sprite[3];
    public Sprite[] rightAnimation = new Sprite[3];

    private bool isLeftAnimationing = false;
    private bool isRightAnimationing = false;
    private bool isIdleAnimationing = false;

    private void Start()
    {
        player = GameObject.Find("Canvas/Player").GetComponent<Image>();
    }


    private void Update()
    {
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

    void StopAll()
    {
        StopCoroutine(Idle());
        StopCoroutine(LeftAnimation());
        StopCoroutine(RightAnimation());
        isLeftAnimationing = false;
        isRightAnimationing = false;
        isIdleAnimationing = false;
    }

    IEnumerator LeftAnimation()
    {
        isLeftAnimationing = true;
        for (int i = 0; PlayerController.leftClick; i++)
        {
            yield return new WaitForSeconds(frameSec);
            player.sprite = leftAnimation[i % leftAnimation.Length];
        }
    }

    IEnumerator RightAnimation()
    {
        isRightAnimationing = true;
        for (int i = 0; PlayerController.rightClick; i++)
        {
            yield return new WaitForSeconds(frameSec);
            player.sprite = rightAnimation[i % rightAnimation.Length];
        }
    }

    IEnumerator Idle()
    {
        isIdleAnimationing = true;
        for (int i = 0; !PlayerController.leftClick && !PlayerController.rightClick; i++)
        {
            yield return new WaitForSeconds(frameSec);
            player.sprite = idle[i % idle.Length];
        }
    }
}