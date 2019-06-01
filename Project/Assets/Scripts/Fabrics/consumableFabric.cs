using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class consumableFabric : MonoBehaviour
{
    public GameObject heartPrefab, energyPrefab;
    public GameObject[] GrapePrefabs, PumpkinPrefabs;
    public static consumableFabric instance = null;
    private int maxHearts = 1, maxEnergy = 2;

    private int level;
    private ArrayList lastPositions;
    private GameObject[] fruitSpawners;    


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);  

           
    }

    #region Consumables - Next level
    public int spawnConsumables(int level) { 
        this.level = level;
        fruitSpawners = GameObject.FindGameObjectsWithTag("FruitSpawner");    
             
        spawnDrugs();
        return spawnFruits();
    }

    private void spawnDrugs() {        
        int contHearts = 0, contEnergy = 0;
        if(level > 1) {
            for(int i = 0; i < 5; i++) {
                if(getProbabilityOf(80) && maxHearts >= contHearts) {
                    Instantiate(heartPrefab, generateRandomVector3(-11f, 11f, -9f, 9f), Quaternion.identity);
                    contHearts++;
                } 
                
                if(getProbabilityOf(80) && maxEnergy >= contEnergy) {
                    Instantiate(energyPrefab, generateRandomVector3(-11f, 11f, -9f, 9f), Quaternion.identity);
                    contEnergy++;
                }            
            }    
        }    
    }
   
    private int spawnFruits() {
        int spawnedCalories = 0, spawnType = setSpawnType();        
        lastPositions = new ArrayList();
       
        for(int i = 0; i < 6; i++) {           
            Vector3 newFruitPosition = calculatePositionInFarms();
            
            /* 60% Grapes & 40% Pumpkins (aprox) */
            if(getProbabilityOf(60)) {
                spawnedCalories += Instantiate(PumpkinPrefabs[spawnType], newFruitPosition, Quaternion.identity).GetComponent<Fruit>().getCalories();
            } else {
                spawnedCalories += Instantiate(GrapePrefabs[spawnType], newFruitPosition, Quaternion.identity).GetComponent<Fruit>().getCalories();
            }            
        }

        return spawnedCalories;
    }
    
    private Vector3 calculatePositionInFarms() {
        int rndFarm = (int) Random.Range(0, fruitSpawners.Length);
            
        Vector2 rndFarmSize = fruitSpawners[rndFarm].GetComponent<BoxCollider2D>().size;
        Vector3 newFruitPosition = fruitSpawners[rndFarm].transform.position + generateRandomVector3(0f, rndFarmSize.x, -rndFarmSize.y, 0f);

        /* Comprobamos si la nueva fruta estará cerca de otra para respawnearla o no */
        bool repeat = true;
        while(repeat) {
            repeat = false;

            foreach (Vector3 vct in lastPositions) {                        
                Vector3 s = (Vector3)vct;
                if(Vector3.Distance(vct, newFruitPosition) < 1f) {
                    newFruitPosition = fruitSpawners[rndFarm].transform.position + generateRandomVector3(0f, rndFarmSize.x, -rndFarmSize.y, 0f);
                    repeat = true;
                    break; /* Go out foreach */
                }                        
            }                
        }

        lastPositions.Add(newFruitPosition);

        return newFruitPosition;
    }

    
    #endregion

    #region Consumables - Load level
    
    public int spawnConsumables(GameDataSerializable data) { 
        this.level = data.level;
        fruitSpawners = GameObject.FindGameObjectsWithTag("FruitSpawner");         
        spawnDrugs(data.energyPositions, data.heartPositions);
        return spawnFruits(data.grapePositions, data.pumpkinPositions);
    }

    private void spawnDrugs(float[][] energyPositions, float[][] heartPositions) {
        for(int i = 0; i < energyPositions.Length; i++) {
            Instantiate(energyPrefab, new Vector3(energyPositions[i][0], energyPositions[i][1], energyPositions[i][2]), Quaternion.identity);           
        }   

        for(int i = 0; i < heartPositions.Length; i++) {
            Instantiate(heartPrefab, new Vector3(heartPositions[i][0], heartPositions[i][1], heartPositions[i][2]), Quaternion.identity);           
        } 
    }

    private int spawnFruits(float[][] grapePositions, float[][] pumpkinPositions) {
        int caloriesToReturn = 0, spawnType = setSpawnType();     

        for(int i = 0; i < grapePositions.Length; i++) {
           caloriesToReturn += Instantiate(GrapePrefabs[spawnType], new Vector3(grapePositions[i][0], grapePositions[i][1], grapePositions[i][2]), Quaternion.identity).GetComponent<Fruit>().getCalories();      
        }   

        for(int i = 0; i < pumpkinPositions.Length; i++) {
          caloriesToReturn += Instantiate(PumpkinPrefabs[spawnType], new Vector3(pumpkinPositions[i][0], pumpkinPositions[i][1], pumpkinPositions[i][2]), Quaternion.identity).GetComponent<Fruit>().getCalories();           
        } 

        return caloriesToReturn;
    }

    #endregion
    private Vector3 generateRandomVector3(float x1, float x2, float y1, float y2) {
        return new Vector3(Random.Range(x1, x2), Random.Range(y1, y2), 0);
    }

    private int setSpawnType() {
        int spawnType = 0;
        if(this.level >= 3) spawnType = 1;
        if(this.level >= 5) spawnType = 2;

        return spawnType;
    }

    private bool getProbabilityOf(int chance) {
        return Random.Range(0f, 100.0f) >= chance;
    }
}