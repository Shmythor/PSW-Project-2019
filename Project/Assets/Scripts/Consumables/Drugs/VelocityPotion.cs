   
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityPotion : Drug
{
   
    public override int calories => 0;
    public override float chanceOfSpawn => 5f;
    public override string typeOfConsumable => "VelocityPotion";

    private float speedToIncrease = 2f;
    private int secondsToApplySpeed = 17;

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
