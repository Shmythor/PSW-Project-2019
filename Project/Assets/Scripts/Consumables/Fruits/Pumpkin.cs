using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : Fruit
{ 
    public override int calories
    {
        get => getPropsByType();
        set => calories = getPropsByType();
    }  
    public override float chanceOfSpawn => 40f;
    public override string typeOfConsumable => "Pumpkin";    

    public int getPropsByType() {     
        this.GetComponent<SpriteRenderer>().sprite = sprites[this.type];    

        if(this.type == 0) {
            return 10;            
        } else if (this.type == 1) {
            return 20;
        } else {
            return 30;
        }
    }
}
