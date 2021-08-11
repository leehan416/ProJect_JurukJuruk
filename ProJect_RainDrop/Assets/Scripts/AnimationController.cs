using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationController : MonoBehaviour {
    float introFrameSec = .15f;
    float frameSec = .2f;

    Image obj;
    public Sprite[] idle = new Sprite[2];
    public Sprite[] leftAnimation = new Sprite[3];
    public Sprite[] rightAnimation = new Sprite[3];

    public Sprite[] introAnimation = new Sprite[8];

    private Text touch_to_start;

    bool isLeftAnimationing = false;
    bool isRightAnimationing = false;
    bool isIdleAnimationing = false;

    bool isIntroAnimationing = false;
    bool isCleaningAnimationing = false;
    bool isTitleAnimationing = false;

    void Start()
    {
        try
        {
            obj = GameObject.Find("Canvas/Player").GetComponent<Image>();
            return;
        }
        catch (Exception e)
        {
        }

        try
        {
            touch_to_start = GameObject.Find("Canvas/TouchToStart/Text").GetComponent<Text>();
            // Debug.Log(touch_to_start.color);
            obj = GameObject.Find("Canvas/BackGround").GetComponent<Image>();
            isIntroAnimationing = true;
            StartCoroutine(IntroAnimation());
            StartCoroutine(titleAnimation());
        }
        catch (Exception e) { }
    }


    private void Update()
    {
        if (isIntroAnimationing)
        {
            return;
        }


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

    IEnumerator titleAnimation()
    {
        isTitleAnimationing = true;
        for (float i = 0; isIntroAnimationing; i += .1f)
        {
            yield return new WaitForSeconds(.2f);
            //touch_to_start = new Color(77, 77, 77, Mathf.Abs(255 * Mathf.Sin(i)));
            touch_to_start.color = new Color(77 / 255f, 77 / 255f, 77 / 255f, Mathf.Abs(255 * Mathf.Sin(i)) / 255f);

            // Debug.Log(touch_to_start.color);
            // new Color(255/ 255f, 10/ 255f, 10/ 255f, 255 / 255f);
        }
    }


    IEnumerator LeftAnimation()
    {
        isLeftAnimationing = true;
        for (int i = 0; PlayerController.leftClick; i++)
        {
            yield return new WaitForSeconds(frameSec);
            obj.sprite = leftAnimation[i % leftAnimation.Length];
        }
    }

    IEnumerator RightAnimation()
    {
        isRightAnimationing = true;
        for (int i = 0; PlayerController.rightClick; i++)
        {
            yield return new WaitForSeconds(frameSec);
            obj.sprite = rightAnimation[i % rightAnimation.Length];
        }
    }

    IEnumerator Idle()
    {
        isIdleAnimationing = true;
        for (int i = 0; !PlayerController.leftClick && !PlayerController.rightClick; i++)
        {
            yield return new WaitForSeconds(frameSec);
            obj.sprite = idle[i % idle.Length];
        }
    }

    IEnumerator IntroAnimation()
    {
        isIntroAnimationing = true;
        for (int i = 0; isIntroAnimationing; i++)
        {
            obj.sprite = introAnimation[i % introAnimation.Length];
            yield return new WaitForSeconds(introFrameSec);
        }
    }
}