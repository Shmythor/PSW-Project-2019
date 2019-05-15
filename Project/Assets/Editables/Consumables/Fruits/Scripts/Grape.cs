using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grape : Fruit
{
    
    void Awake() {
        setPropsByType();
    }

    void setPropsByType() {        
        if(this.type == 1) {
            this.calories = 5;            
        } else if (this.type == 2) {
            this.calories = 10;
        } else {
            this.calories = 15;
        }
    }


    

    

}
