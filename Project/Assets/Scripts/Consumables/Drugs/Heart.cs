using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Drug
{    

    public override int calories => 35;
    public override float chanceOfSpawn => 15f;
    public override string typeOfConsumable => "Heart";
        
    void OnTriggerEnter2D(Collider2D other) {          
        if (other.transform.tag == "Player") {
            restoreHealth();   
            GameController.instance.consumeCalories(calories, false);        
            Destroy(gameObject);
        }        
    }
}