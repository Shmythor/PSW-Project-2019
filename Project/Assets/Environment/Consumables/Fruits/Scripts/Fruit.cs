using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Fruit : MonoBehaviour
{
    // References
    protected int calories; 
    public int type;  

    void Awake() {
        //Create a new GameObject with the same trigger and rigidbody settings as this one
        GameObject obj = new GameObject("Cube with collider");
        var bCol = obj.AddComponent<BoxCollider>();
        bCol.isTrigger = true;
    
        var rb = obj.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.tag == "Player") {
            GameController.instance.consumeCalories(calories);
            Destroy(gameObject); 
        } 
    }
    
   
    /// Sent each frame where another object is within a trigger collider
    /// attached to this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.tag == "Environment") {
            //TODO -> HABRÍA QUE REGENERAR LA FRUTA
            GameController.instance.consumeCalories(1000);
            Destroy(gameObject); 
        }        
    }

    void setType(int type) {
        this.type = type;
    }

    public int getCalories() { return calories; }
}