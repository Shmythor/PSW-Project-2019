using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class consumableFabric : MonoBehaviour
{
    public GameObject heartPrefab, energyPrefab;
    public GameObject[] GrapePrefabs, PumpkinPrefabs;
    public static consumableFabric instance = null;
    private int maxHearts = 1, maxEnergy = 2;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public int spawnFruit(int level) {
        int spawnedCalories = 0;
        for(int i = 0; i < 10; i++) {
            if(Random.Range(0f, 10.0f) >= 8f) {
                spawnedCalories += Instantiate(PumpkinPrefabs[level - 1] ,new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-9.0f, 9.0f), 0), Quaternion.identity).GetComponent<Fruit>().getCalories();
            } else {
                spawnedCalories += Instantiate(GrapePrefabs[level - 1], new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-9.0f, 9.0f), 0), Quaternion.identity).GetComponent<Fruit>().getCalories();
            }            
        }
        return spawnedCalories;
    }

    public void spawnConsumable(int level) {
        int contHearts = 0, contEnergy = 0;
        if(level != 1) {
            for(int i = 0; i < 5; i++) {
                if(Random.Range(0f, 10.0f) >= 8f && maxHearts >= contHearts) {
                    Instantiate(heartPrefab, new Vector3(Random.Range(-11.0f, 11.0f), Random.Range(-9.0f, 9.0f), 0), Quaternion.identity);
                    contHearts++;
                } 
                
                if(Random.Range(0f, 10.0f) >= 8f && maxEnergy >= contEnergy) {
                    Instantiate(energyPrefab, new Vector3(Random.Range(-11.0f, 11.0f), Random.Range(-9.0f, 9.0f), 0), Quaternion.identity);
                    contEnergy++;
                }            
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