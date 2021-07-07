/* 플레이어 조작관련 스크립트 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public static PlayerController instance;
    
    [HideInInspector] public float playerSpeed = 10f;

    [HideInInspector] public Button leftBtn;
    [HideInInspector] public Button rightBtn;

    [HideInInspector] public bool leftClick;
    [HideInInspector] public bool rightClick;


    void Start()
    {
        if (!instance) instance = this;
        else DestroyImmediate(this);

        #region btnSet

        leftBtn = GameObject.Find("Canvas/LeftBtn").GetComponent<Button>();
        rightBtn = GameObject.Find("Canvas/RightBtn").GetComponent<Button>();

        EventTrigger trgL = leftBtn.gameObject.AddComponent<EventTrigger>();
        EventTrigger trgR = rightBtn.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry enRC = new EventTrigger.Entry();
        EventTrigger.Entry enRH = new EventTrigger.Entry();
        EventTrigger.Entry enLC = new EventTrigger.Entry();
        EventTrigger.Entry enLH = new EventTrigger.Entry();

        enRC.eventID = EventTriggerType.PointerDown;
        enLC.eventID = EventTriggerType.PointerDown;
        enRH.eventID = EventTriggerType.PointerUp;
        enLH.eventID = EventTriggerType.PointerUp;

        enRC.callback.AddListener(delegate { rightClick = true; });
        enLC.callback.AddListener(delegate { leftClick = true; });
        enRH.callback.AddListener(delegate { rightClick = false; });
        enLH.callback.AddListener(delegate { leftClick = false; });

        trgL.triggers.Add(enLC);
        trgL.triggers.Add(enLH);
        
        trgR.triggers.Add(enRC);
        trgR.triggers.Add(enRH);

        #endregion
    }

    void Update()
    {
        #region Conteroller

        if (((Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.LeftArrow))) || leftClick)
            transform.Translate(-playerSpeed, 0, 0);
        if (((Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.RightArrow))) || rightClick)
            transform.Translate(playerSpeed, 0, 0);
        // 키보드 A D, 화살표 좌우 입력 or 버튼 클릭

        #endregion
    }
}