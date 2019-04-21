using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : Fruit
{
    //Sprite green = Resources.Load("./Sprites/Grape-white", typeof(Sprite)) as Sprite, 
    //white = Resources.Load("./Sprites/Grape-white", typeof(Sprite)) as Sprite,
    //purple = Resources.Load("./Sprites/Grape-white", typeof(Sprite)) as Sprite;
    

    // References  

    void Start() {
        this.FabricFruit = GameObject.Find("FabricFruit");        
        setPropsByType();
    }    

    void setPropsByType() {        
        if(this.type == 1) {
            this.calories = 10;            
        } else if (this.type == 2) {
            this.calories = 20;
        } else {
            this.calories = 30;
        }
    }
}
