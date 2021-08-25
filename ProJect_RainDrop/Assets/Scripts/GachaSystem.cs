using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class GachaSystem : MonoBehaviour {
    public static GachaSystem instance;
    private int getProbability = 30; // 뽑기 성공 확률 getProbability / 100 %

    Random random = new Random();
    public Sprite[] animationSprite = new Sprite[13];
    [HideInInspector] public bool isAnimationing = false;

    private void Start()
    {
        if (!instance) instance = this;
        else Destroy(this);
    }

    public int gacha()
    {
        UI_MultiScene.instance.unactivePopup();

        if (random.Next(1, 100) < getProbability)
        {
            //UI_MultiScene.instance.popUpOK.SetActive(true);
            return 0;
            // 실패
        }
        else
        {
            // 성공
            int i = 0;
            DataBase.getCoustume();

            for (int k = 1; k < DataBase.isCostumeLock.Length; k++)
                if (DataBase.isCostumeLock[k])
                {
                    Debug.Log("모두 해금됨.");
                    return 0;
                }

            while (i == 0 || !DataBase.isCostumeLock[i])
            {
                i = random.Next(1, 5);
            }

            return i;
        }
    }


    // 1 ~ 2 => (3 ~ 8) * 3 => 9 ~ 13
    public IEnumerator gachaAnimation()
    {
        isAnimationing = true;
        for (int i = 1; i < 26; i++)
        {
            if (i <= 2)
            {
                GameObject.Find("Canvas/MachineBG/Machine").GetComponent<Image>().sprite = animationSprite[i];
            }
            else if (3 <= i && i <= 20)
            {
                GameObject.Find("Canvas/MachineBG/Machine").GetComponent<Image>().sprite =
                    animationSprite[((i - 3) % 6) + 3];
            }
            else
            {
                GameObject.Find("Canvas/MachineBG/Machine").GetComponent<Image>().sprite = animationSprite[i - 13];
            }

            yield return new WaitForSeconds(.15f);
        }

        isAnimationing = false;
        GameObject.Find("Canvas/MachineBG/Machine").GetComponent<Image>().sprite = animationSprite[0];
    }
}