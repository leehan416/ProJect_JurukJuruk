using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IntroAnimation : MonoBehaviour {
    float introFrameSec = .15f;

    Image obj;
    private Image fader;
    public Sprite[] introAnimation = new Sprite[8];

    Text touch_to_start;

    bool isIntroAnimationing = false;
    bool isTitleAnimationing = false;

    void Start()
    {
        fader = GameObject.Find("Canvas/Fader").GetComponent<Image>();
        touch_to_start = GameObject.Find("Canvas/TouchToStart/Text").GetComponent<Text>();
        obj = GameObject.Find("Canvas/BackGround").GetComponent<Image>();
        isIntroAnimationing = true;
        StartCoroutine(fadeOut());
        StartCoroutine(Intro());
        StartCoroutine(titleAnimation());
    }

    //----------------------------------------------------------------------------------------------------------------------
    // IntroScene


    IEnumerator fadeOut()
    {
        for (int i = 10; i > 1; i--)
        {
            yield return new WaitForSeconds(.08f);
            fader.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 2 * i / 10f);
        }

        fader.gameObject.SetActive(false);
    }


//Touch To Start 버튼 깜박임
    IEnumerator titleAnimation()
    {
        isTitleAnimationing = true;
        for (float i = 0; isIntroAnimationing; i += .1f)
        {
            yield return new WaitForSeconds(.05f);
            touch_to_start.color = new Color(77 / 255f, 77 / 255f, 77 / 255f, Mathf.Abs(255 * Mathf.Sin(i)) / 255f);
        }
    }

// 비내리는 배경 애니메이션
    IEnumerator Intro()
    {
        isIntroAnimationing = true;
        for (int i = 0; isIntroAnimationing; i++)
        {
            obj.sprite = introAnimation[i % introAnimation.Length];
            yield return new WaitForSeconds(introFrameSec);
        }
    }
}