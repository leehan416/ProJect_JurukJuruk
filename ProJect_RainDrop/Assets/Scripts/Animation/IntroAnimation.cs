using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IntroAnimation : MonoBehaviour {
    float introFrameSec = .15f; // 글자 깜박임 속도 

    Image obj;
    private Image fader;
    public Sprite[] beforeAnimation = new Sprite[8];
    public Sprite[] afterAnimation = new Sprite[8];


    Text touch_to_start;

    private bool isUnlocked = false;
    // bool isIntroAnimationing = false;
    // bool isTitleAnimationing = false;


    void Start()
    {
        // get UI
        fader = GameObject.Find("Canvas/Fader").GetComponent<Image>();
        touch_to_start = GameObject.Find("Canvas/TouchToStart/Text").GetComponent<Text>();
        obj = GameObject.Find("Canvas/BackGround").GetComponent<Image>();

        // Get Data
        DataBase.getConsumerLock();


        // 과학자 해금 검사
        int value = 0;
        for (int i = 9; i < 13; i++)
            if (!DataBase.consumers[i].isLock)
                value++;

        if (value > 3)
            isUnlocked = true;


        // 애니메이샨 시직
        // isIntroAnimationing = true;
        StartCoroutine(fadeOut());
        StartCoroutine(Intro());
        StartCoroutine(titleAnimation());
    }

    // 게임 접속 => 흰 화면 fade out
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
        // isTitleAnimationing = true;
        for (float i = 0;; i += .1f)
        {
            yield return new WaitForSeconds(.05f);
            touch_to_start.color = new Color(77 / 255f, 77 / 255f, 77 / 255f, Mathf.Abs(255 * Mathf.Sin(i)) / 255f);
        }
    }

    // 비내리는 배경 애니메이션
    IEnumerator Intro()
    {
        // isIntroAnimationing = true;
        for (int i = 0;; i++)
        {
            if (isUnlocked)
                obj.sprite = afterAnimation[i % afterAnimation.Length];
            else
                obj.sprite = beforeAnimation[i % beforeAnimation.Length];
            yield return new WaitForSeconds(introFrameSec);
        }
    }
}