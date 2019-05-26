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
            rndFarm = (int) Random.Range(0, fruitSpawners.Length);
            
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

            int spawnType = 0;
            if(level >= 3) spawnType = 1;
            if(level >= 5) spawnType = 2;

            if(Random.Range(0f, 10.0f) >= 6f) {
                spawnedCalories += Instantiate(PumpkinPrefabs[spawnType], newConsumablePosition, Quaternion.identity).GetComponent<Fruit>().getCalories();
            } else {
                spawnedCalories += Instantiate(GrapePrefabs[spawnType], newConsumablePosition, Quaternion.identity).GetComponent<Fruit>().getCalories();
            }            
        }

        return spawnedCalories;
    }

    

    public int spawnConsumables(int level) { 
        this.level = level;      
        spawnDrugs();
        return spawnFruits();
    }

    public int spawnConsumables(GameDataSerializable data) { 
        this.level = data.level;      
        spawnDrugs(data.energyPositions, data.heartPositions);
        return spawnFruits(data.grapePositions, data.pumpkinPositions);
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

    private void spawnDrugs(float[][] energyPositions, float[][] heartPositions) {
        for(int i = 0; i < energyPositions.Length; i++) {
            Instantiate(energyPrefab, new Vector3(energyPositions[i][0], energyPositions[i][1], energyPositions[i][2]), Quaternion.identity);           
        }   

        for(int i = 0; i < heartPositions.Length; i++) {
            Instantiate(heartPrefab, new Vector3(heartPositions[i][0], heartPositions[i][1], heartPositions[i][2]), Quaternion.identity);           
        } 
    }

    private int spawnFruits(float[][] grapePositions, float[][] pumpkinPositions) {
        int caloriesToReturn = 0;

        int spawnType = 0;
        if(level >= 3) spawnType = 1;
        if(level >= 5) spawnType = 2; 

        for(int i = 0; i < grapePositions.Length; i++) {
           caloriesToReturn += Instantiate(GrapePrefabs[spawnType], new Vector3(grapePositions[i][0], grapePositions[i][1], grapePositions[i][2]), Quaternion.identity).GetComponent<Fruit>().getCalories();      
        }   

        for(int i = 0; i < pumpkinPositions.Length; i++) {
          caloriesToReturn += Instantiate(PumpkinPrefabs[spawnType], new Vector3(pumpkinPositions[i][0], pumpkinPositions[i][1], pumpkinPositions[i][2]), Quaternion.identity).GetComponent<Fruit>().getCalories();           
        } 

        return caloriesToReturn;
    }

    private Vector3 generateRandomVector3(float x1, float x2, float y1, float y2) {
        return new Vector3(Random.Range(x1, x2), Random.Range(y1, y2), 0);
    }
    
    
}