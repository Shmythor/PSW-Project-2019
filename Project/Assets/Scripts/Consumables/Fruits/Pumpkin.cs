using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : Fruit
{ 
    public override int calories
    {
        get => setPropsByType();
        set => calories = setPropsByType();
    }  
    public override int chanceOfSpawn => 40;
    public override string typeOfConsumable => "Pumpkin";

    

    int setPropsByType() {     
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
