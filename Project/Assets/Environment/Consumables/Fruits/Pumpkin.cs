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
    public Pumpkin() {
        this.type = 1;
        setPropsByType();
    }

    public Pumpkin(int type) {
        this.type = type;
        setPropsByType();
    }

    void Start() {
        this.FabricFruit = GameObject.Find("FabricFruit");
        this.type = 1; 
        setPropsByType();
    }

    void setPropsByType() {        
        if(this.type == 1) {
            this.calories = 10;
            GetComponent<SpriteRenderer>().sprite = null;
            
        } else if (this.type == 2) {
            this.calories = 20;
            //GetComponent(SpriteRenderer).sprite = white;
        } else {
            this.calories = 30;
            //GetComponent(SpriteRenderer).sprite = purple;
        }
    }
}
