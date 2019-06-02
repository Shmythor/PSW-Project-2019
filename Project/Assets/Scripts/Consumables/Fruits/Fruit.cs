using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Fruit : Consumable
{
    /*          References          */
    public int type;    
    public override int calories => 0;
    public override int chanceOfSpawn => 0;
    public override string typeOfConsumable => "";

    public Sprite[] sprites;


    public void setType(int type) { this.type = type; }
    public int getCalories() { return calories; }

    void Awake() {
    /* Create a new GameObject with the same trigger and rigidbody settings as this one */
        GameObject obj = new GameObject("Cube with collider");
        var bCol = obj.AddComponent<BoxCollider>();
        bCol.isTrigger = true;
    
        var rb = obj.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.tag == "Player") {
            GameController.instance.consumeCalories(calories, true);
            Destroy(gameObject); 
        } 
    }
    
    /*
        Sent each frame where another object is within a trigger collider
        attached to this object (2D physics only).
    */
    
    


}