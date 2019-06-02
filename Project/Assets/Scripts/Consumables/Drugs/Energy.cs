   
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : Drug
{
   
    public override int calories => 20;
    public override float chanceOfSpawn => 25f;
    public override string typeOfConsumable => "Energy";

    void OnTriggerEnter2D(Collider2D other) { 
        if (other.transform.tag == "Player") { 
            restoreEnergy();
            GameController.instance.consumeCalories(calories, false);           
            Destroy(gameObject);             
        } 
                  
    }


}
