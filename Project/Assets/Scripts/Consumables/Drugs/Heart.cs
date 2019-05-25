using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Drug
{    
        
    void OnTriggerEnter2D(Collider2D other) {          
        if (other.transform.tag == "Player") {
            GameController.instance.restoreHealth();           
            Destroy(gameObject);
        }        
    }
}