using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class consumableFabric : MonoBehaviour
{
    public GameObject heartPrefab, energyPrefab;

    private int maxHearts = 1, maxEnergy = 2;

    public GameObject[] GrapePrefabs, PumpkinPrefabs;

    

    public void spawnFruit(int level) {
        for(int i = 0; i < 10; i++) {
            if(Random.Range(0f, 10.0f) >= 8f) {
                Instantiate(PumpkinPrefabs[level - 1], new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0), Quaternion.identity);
            } else {
                Instantiate(GrapePrefabs[level - 1], new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0), Quaternion.identity);
            }            
        }        
    }

    public void spawnConsumable(int level) {
        int contHearts = 0, contEnergy = 0;

        for(int i = 0; i < 5; i++) {
            if(Random.Range(0f, 10.0f) >= 8f && maxHearts >= contHearts) {
                Instantiate(heartPrefab, new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0), Quaternion.identity);
                contHearts++;
            } 
            
            if(Random.Range(0f, 10.0f) >= 8f && maxEnergy >= contEnergy) {
                Instantiate(energyPrefab, new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0), Quaternion.identity);
                contEnergy++;
            }            
        }        
    }

    public void restoreHealth() {
        GameController.instance.restoreHealth();
    }

    public void restoreEnergy() {
        GameController.instance.restoreEnergy();
    }    
}