   
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityPotion : Drug
{
   
    public override int calories => 0;
    public override int chanceOfSpawn => 5;
    public override string typeOfConsumable => "VelocityPotion";

    private float speedToIncrease = 0.35f;
    private int secondsToApplySpeed = 5;

    void OnTriggerEnter2D(Collider2D other) { 
        if (other.transform.tag == "Player") { 
            givePlayerSpeed();         
            Destroy(gameObject);             
        }                   
    }

    private void givePlayerSpeed() {
        GameController.instance.updateSpeed(speedToIncrease, secondsToApplySpeed);
    }


}
