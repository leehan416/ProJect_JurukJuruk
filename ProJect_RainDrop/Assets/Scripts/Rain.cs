using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    private void Start() {
         this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,-100),ForceMode2D.Impulse);
         
    }

    //void OnColliderEnter()
    void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.gameObject.tag.Equals("Ground")) Destroy(gameObject);

        else if (other.collider.gameObject.tag.Equals("Pail")) {
            DataBase.savedWater += Convert.ToInt16(DataBase.perDrop);
            Destroy(this.gameObject);
            UIManager.instance.MoneySet();
        }
    }
}
