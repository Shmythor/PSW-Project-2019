using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Fruit : MonoBehaviour
{
    // References
    protected int calories; 
    public int type;  

    void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.tag == "Player") {
            GameController.instance.consumeCalories(calories);
            Destroy(gameObject); 
        } else if (other.transform.tag == "Environment") {
            // Re-spawn fruit ?
            Destroy(gameObject); 
        }
    }

    void setType(int type) {
        this.type = type;
    }

    public int getCalories() { return calories; }
}