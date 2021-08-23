using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class GachaSystem : MonoBehaviour {
    private int getProbability = 30; // 뽑기 성공 확률 getProbability/100 %

    Random random = new Random();
    public static Sprite[] animationSprite = new Sprite[14];


    public /*static*/ void gacha()
    {
        if (random.Next(1, 100) < getProbability)
        {
            // 실패
        }
        else
        {
            // 성공
        }
    }


    // 1 ~ 2 => (3 ~ 8) * 3 => 9 ~ 13
    public /*static*/ IEnumerator gachaAnimation()
    {
        for (int i = 1; i < 26; i++)
        {
            if (i <= 2)
            {
                this.GetComponent<Image>().sprite = animationSprite[i];
            }
            else if (3 <= i && i <= 20)
            {
                this.GetComponent<Image>().sprite = animationSprite[((i - 3) % 6) + 3];
            }
            else
            {
                this.GetComponent<Image>().sprite = animationSprite[i - 12];
            }

            yield return new WaitForSeconds(.15f);
        }
    }
}