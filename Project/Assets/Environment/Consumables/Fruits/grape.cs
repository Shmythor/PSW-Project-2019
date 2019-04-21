using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grape : Fruit
{
    
    //Sprite green = Resources.Load("./Sprites/Grape-white", typeof(Sprite)) as Sprite, 
    //white = Resources.Load("./Sprites/Grape-white", typeof(Sprite)) as Sprite,
    //purple = Resources.Load("./Sprites/Grape-white", typeof(Sprite)) as Sprite;
    

    void Start() {
        this.FabricFruit = GameObject.Find("FabricFruit");
        this.type = 1;
        setPropsByType();
    }

    public Grape(int type) {
        this.type = type;
        setPropsByType();
    }

    void setPropsByType() {        
        if(this.type == 1) {
            this.calories = 5;
            //GetComponent<SpriteRenderer>().sprite = null;
            
        } else if (this.type == 2) {
            this.calories = 10;
            //GetComponent(SpriteRenderer).sprite = white;
        } else {
            this.calories = 15;
            //GetComponent(SpriteRenderer).sprite = purple;
        }
    }


    

    

}
