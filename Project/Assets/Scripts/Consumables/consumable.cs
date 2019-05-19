using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
   void OnTriggerEnter2D(Collider2D col) {
        if (col.transform.tag == "Player") {
            if(gameObject.tag == "energy") {
                GameController.instance.restoreEnergy();
                 Destroy(gameObject); 
            } else if(gameObject.tag == "heart") {
                GameController.instance.restoreHealth();
                 Destroy(gameObject); 
            }          
           
        }
    }
}
