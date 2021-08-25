using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IntroAnimation : MonoBehaviour {
    float introFrameSec = .15f;

    Image obj;

    public Sprite[] introAnimation = new Sprite[8];

    Text touch_to_start;

    bool isIntroAnimationing = false;
    bool isTitleAnimationing = false;

    void Start()
    {
        touch_to_start = GameObject.Find("Canvas/TouchToStart/Text").GetComponent<Text>();
        obj = GameObject.Find("Canvas/BackGround").GetComponent<Image>();
        isIntroAnimationing = true;
        StartCoroutine(Intro());
        StartCoroutine(titleAnimation());
    }

    //----------------------------------------------------------------------------------------------------------------------
    // IntroScene

    //Touch To Start 버튼 깜박임
    IEnumerator titleAnimation()
    {
        isTitleAnimationing = true;
        for (float i = 0; isIntroAnimationing; i += .1f)
        {
            yield return new WaitForSeconds(.1f);
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