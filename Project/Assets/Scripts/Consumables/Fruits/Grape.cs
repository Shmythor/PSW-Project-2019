using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grape : Fruit
{

    public override float chanceOfSpawn => 60f;
    public override string typeOfConsumable => "Grape";

    public override int calories
    {
        get => getPropsByType();
        set => calories = getPropsByType();
    }    

    public int getPropsByType() {     
        this.GetComponent<SpriteRenderer>().sprite = sprites[this.type];
        
        if(this.type == 0) {                        
            return 5;            
        } else if (this.type == 1) {
            return 10;
        } else {
            return 15;
        }
    }
}
