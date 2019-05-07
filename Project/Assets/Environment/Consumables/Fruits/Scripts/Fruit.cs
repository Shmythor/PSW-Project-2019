using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Fruit : MonoBehaviour
{
    // References
    protected int calories; 
    public int type;  

    void OnTriggerEnter2D(Collider2D col) {
        if (col.transform.tag == "Player") {
            GameController.instance.consumeCalories(calories);
            Destroy(gameObject); 
        }
    }

    void setType(int type) {
        this.type = type;
    }

}