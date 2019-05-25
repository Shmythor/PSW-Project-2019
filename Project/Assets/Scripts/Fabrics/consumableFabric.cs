﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class consumableFabric : MonoBehaviour
{
    public GameObject heartPrefab, energyPrefab;
    public GameObject[] GrapePrefabs, PumpkinPrefabs;
    public static consumableFabric instance = null;
    private int maxHearts = 1, maxEnergy = 2;

    private int level;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);        
    }

    private int spawnFruits() {
        GameObject[] fruitSpawners = GameObject.FindGameObjectsWithTag("FruitSpawner"); 
        ArrayList lastPositions = new ArrayList();
        int spawnedCalories = 0, rndFarm;
       
        for(int i = 0; i < 6; i++) { 
            
            //TODO!!!! (UTILIZAR fruirSpawners.leght)
            if (level == 3) { rndFarm = (int) Random.Range(0, 5); }
            else { rndFarm = (int) Random.Range(0, 3); }
            //FIN TODO!!!
            Vector2 rndFarmSize = fruitSpawners[rndFarm].GetComponent<BoxCollider2D>().size;
            Vector3 newConsumablePosition = fruitSpawners[rndFarm].transform.position + generateRandomVector3(0f, rndFarmSize.x, -rndFarmSize.y, 0f);

            /* Comprobamos sí la nueva fruta estará cerca de otra para respawnearla o no */
            bool repeat = true;
            while(repeat) {
                repeat = false;

                foreach (Vector3 vct in lastPositions) {                        
                    Vector3 s = (Vector3)vct;
                    if(Vector3.Distance(vct, newConsumablePosition) < 1f) {
                        newConsumablePosition = fruitSpawners[rndFarm].transform.position + generateRandomVector3(0f, rndFarmSize.x, -rndFarmSize.y, 0f);
                        repeat = true;
                        break; /* Go out foreach */
                    }                        
                }     
                                
            }                
            lastPositions.Add(newConsumablePosition);

            if(Random.Range(0f, 10.0f) >= 6f) {
                spawnedCalories += Instantiate(PumpkinPrefabs[level - 1], newConsumablePosition, Quaternion.identity).GetComponent<Fruit>().getCalories();
            } else {
                spawnedCalories += Instantiate(GrapePrefabs[level - 1], newConsumablePosition, Quaternion.identity).GetComponent<Fruit>().getCalories();
            }            
        }

        return spawnedCalories;
    }

    

    public int spawnConsumables(int level) { 
        this.level = level;      
        spawnDrugs();
        return spawnFruits();
    }

    private void spawnDrugs() {        
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

    private Vector3 generateRandomVector3(float x1, float x2, float y1, float y2) {
        return new Vector3(Random.Range(x1, x2), Random.Range(y1, y2), 0);
    }
    
    public void restoreHealth() {
        GameController.instance.restoreHealth();
    }

    public void restoreEnergy() {
        GameController.instance.restoreEnergy();
    }    
}