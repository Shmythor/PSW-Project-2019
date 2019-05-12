using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : Fruit
{

    // References  

    void Awake() {  
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
