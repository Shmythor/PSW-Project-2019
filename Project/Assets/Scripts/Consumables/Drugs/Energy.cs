   
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : Drug
{
    public Energy()
    {
    }

    void OnTriggerEnter2D(Collider2D other) { 
        if (other.transform.tag == "Player") {           
            Destroy(gameObject); 
            GameController.instance.restoreEnergy(); 
        } 
                  
    }


}
