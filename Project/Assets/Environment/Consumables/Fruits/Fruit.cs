using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Fruit : MonoBehaviour
{
    // References
    public int calories; 
    protected int type;  
    protected GameObject FabricFruit;

    void Start() {
        this.FabricFruit = GameObject.Find("FabricFruit");
    }    

    void OnTriggerEnter2D(Collider2D col) {
        if (col.transform.tag == "Player") {
            FabricFruit.SendMessage("consumeCalories", this.calories);
            Destroy(gameObject); 
        }
    }



}