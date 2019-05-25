using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drug : Consumable
{
    void Awake() {
    /* Create a new GameObject with the same trigger and rigidbody settings as this one */
        GameObject obj = new GameObject("Cube with collider");
        var bCol = obj.AddComponent<BoxCollider>();
        bCol.isTrigger = true;
    
        var rb = obj.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    
    


}