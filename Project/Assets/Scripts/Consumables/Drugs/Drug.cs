using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drug : Consumable
{

    public override float chanceOfSpawn => 0f;
    public override string typeOfConsumable => "";
    public override int calories
    {
        get => 0;
        set => calories = 0;
    }
    
    void Awake() {
    /* Create a new GameObject with the same trigger and rigidbody settings as this one */
        GameObject obj = new GameObject("Cube with collider");
        var bCol = obj.AddComponent<BoxCollider>();
        bCol.isTrigger = true;
    
        var rb = obj.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    
    protected void restoreHealth() {
        GameController.instance.restoreHealth();
    }

    protected void restoreEnergy() {
        GameController.instance.restoreEnergy();
    }

}