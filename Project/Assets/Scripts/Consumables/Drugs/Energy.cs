   
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : Drug
{
   

    void OnTriggerEnter2D(Collider2D other) { 
        if (other.transform.tag == "Player") { 
            restoreEnergy();           
            Destroy(gameObject);             
        } 
                  
    }


}
