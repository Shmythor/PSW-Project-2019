using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class consumable : MonoBehaviour
{
   void OnTriggerEnter2D(Collider2D col) {
        if (col.transform.tag == "Player") {
            if(gameObject.name == "energyConsumable") {
                GameController.instance.restoreEnergy();
            } else if(gameObject.name == "heartConsumable") {
                GameController.instance.restoreHealth();
            }
            
            Destroy(gameObject); 
        }
    }
}
