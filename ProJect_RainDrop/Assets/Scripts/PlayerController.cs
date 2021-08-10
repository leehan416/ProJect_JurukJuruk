/* 플레이어 조작관련 스크립트 */

// using UnityEditor.Rendering;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    [HideInInspector] public float playerSpeed = 300f;

    // [HideInInspector] public Button leftBtn;
    // [HideInInspector] public Button rightBtn;

    public static bool leftClick;
    public static bool rightClick;


//public  static bool 


    void Start()
    {
        #region btnSet

        // if (!DataBase.isReverse)
        // {
        //     leftBtn = GameObject.Find("Canvas/SmallBox/LeftBtn").GetComponent<Button>();
        //     rightBtn = GameObject.Find("Canvas/SmallBox/RightBtn").GetComponent<Button>();
        // }
        //
        // else
        // {
        //     leftBtn = GameObject.Find("Canvas/SmallBox/RightBtn").GetComponent<Button>();
        //     rightBtn = GameObject.Find("Canvas/SmallBox/LeftBtn").GetComponent<Button>();
        // }
        //
        // EventTrigger trgL = leftBtn.gameObject.AddComponent<EventTrigger>();
        // EventTrigger trgR = rightBtn.gameObject.AddComponent<EventTrigger>();
        //
        // EventTrigger.Entry enRC = new EventTrigger.Entry();
        // EventTrigger.Entry enRH = new EventTrigger.Entry();
        // EventTrigger.Entry enLC = new EventTrigger.Entry();
        // EventTrigger.Entry enLH = new EventTrigger.Entry();
        //
        // enRC.eventID = EventTriggerType.PointerDown;
        // enLC.eventID = EventTriggerType.PointerDown;
        // enRH.eventID = EventTriggerType.PointerUp;
        // enLH.eventID = EventTriggerType.PointerUp;
        //
        // enRC.callback.AddListener(delegate { rightClick = true; });
        // enLC.callback.AddListener(delegate { leftClick = true; });
        // enRH.callback.AddListener(delegate { rightClick = false; });
        // enLH.callback.AddListener(delegate { leftClick = false; });
        //
        // trgL.triggers.Add(enLC);
        // trgL.triggers.Add(enLH);
        //
        // trgR.triggers.Add(enRC);
        // trgR.triggers.Add(enRH);

        #endregion
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x > UIManager.instance.transform.GetComponent<RectTransform>().rect.width / 2)
            {
                rightClick = true;
                leftClick = false;
                if (gameObject.transform.position.x <
                    UIManager.instance.transform.GetComponent<RectTransform>().rect.width)
                    transform.Translate(playerSpeed * Time.deltaTime, 0, 0);
            }
            else
            {
                leftClick = true;
                rightClick = false;
                if (this.gameObject.transform.position.x > 0)

                    transform.Translate(-playerSpeed * Time.deltaTime, 0, 0);
            }

            return;
        }
        else
        {
            leftClick = false;
            rightClick = false;
        }


        // if (Input.touchCount > 0)
        // {
        //     // 터치 하였다면,
        //     Touch touch = Input.GetTouch(0);
        //
        //     if (touch.phase == TouchPhase.Began)
        //     {
        //         
        //         Debug.Log(touch.position);
        //         // 터치가 시작되었을 때
        //     }
        //
        //     if (touch.phase == TouchPhase.Ended)
        //     {
        //         // 터치가 끝났을 때 
        //     }
        // }
        //
        //


        #region Conteroller

        // if (((Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.LeftArrow))) /* 테스트용 시스템*/ || leftClick)
        // {
        //     if (this.gameObject.transform.position.x > 0)
        //     {
        //         transform.Translate(-playerSpeed * Time.deltaTime, 0, 0);
        //     }
        // }
        //
        // if (((Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.RightArrow))) || rightClick)
        //     if (gameObject.transform.position.x <
        //         UIManager.instance.transform.GetComponent<RectTransform>().rect.width)
        //         transform.Translate(playerSpeed * Time.deltaTime, 0, 0);
        // // 키보드 A D, 화살표 좌우 입력 or 버튼 클릭

        #endregion
    }
}