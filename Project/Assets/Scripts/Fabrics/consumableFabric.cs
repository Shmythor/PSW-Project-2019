using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class consumableFabric : MonoBehaviour
{
    public List<GameObject> ConsumablePrefabs;
    public static consumableFabric instance = null;
    private int maxHearts, maxEnergy, spawnType;

    private int level, numOfFruitsSpawned;
    private ArrayList lastPositions;
    private GameObject[] fruitSpawners;    


    private void Awake()
    {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            //There can only ever be one instance of this object!!
            Destroy(gameObject);  
        }        
    }

    #region Consumable Fabric -- Public methods  

    public int spawnConsumables(int level) {
        cleanLevelBeforeSpawn();
        initVariables(level);

        foreach (GameObject consumable in ConsumablePrefabs)
        {
            spawnConsumablesOf(consumable);
        }

        return numOfFruitsSpawned;
    }

    public void spawnConsumables(GameDataSerializable data) {
        cleanLevelBeforeSpawn();
        initVariables(data.level);
        
        spawnConsumablesOf(data);
    }

    
    #endregion

    /* Next level */
    private void spawnConsumablesOf(GameObject consumable) {
        string typeOfConsumable = consumable.GetComponent<Consumable>().typeOfConsumable;

        switch(typeOfConsumable) {
            case "Grape":
                spawnGrapes(consumable);
                break;
            case "Pumpkin":
                spawnPumpkins(consumable);
                break;
            case "Energy":
                spawnEnergies(consumable);
                break;
            case "Heart":
                spawnHearts(consumable);
                break;
            case "VelocityPotion":
                spawnVelocityPotions(consumable);
                break;
        }
    }

    /* Load level */
    private void spawnConsumablesOf(GameDataSerializable data) {
        spawnGrapes(data.grapePositions);
        spawnPumpkins(data.pumpkinPositions);
        spawnEnergies(data.energyPositions);
        spawnHearts(data.heartPositions);
    }

    #region Consumables - Next level
    
    private void spawnEnergies(GameObject energyPrefab) { 
        Energy energy = energyPrefab.GetComponent<Energy>();
        int contEnergies = 0;

        if(level > 1) {
            for(int i = 0; i < 2; i++) {
                if(getProbabilityOf(energy.chanceOfSpawn) && maxEnergy > contEnergies) {
                    Instantiate(energyPrefab, generateRandomVector3(-11f, 11f, -9f, 9f), Quaternion.identity);
                    contEnergies++;
                }            
            }    
        }    
    }

    private void spawnHearts(GameObject heartPrefab) {       
        Heart heart = heartPrefab.GetComponent<Heart>();
        int contHearts = 0;

        if(level > 1) {
            for(int i = 0; i < 2; i++) {
                if(getProbabilityOf(heart.chanceOfSpawn) && maxHearts > contHearts) {
                    Instantiate(heartPrefab, generateRandomVector3(-11f, 11f, -9f, 9f), Quaternion.identity);
                    contHearts++;
                }       
            }    
        }    
    }

    private void spawnVelocityPotions(GameObject velocityPrefab) {       
        VelocityPotion velocity = velocityPrefab.GetComponent<VelocityPotion>();

        if(getProbabilityOf(velocity.chanceOfSpawn)) {
           Instantiate(velocityPrefab, generateRandomVector3(-11f, 11f, -9f, 9f), Quaternion.identity);
        }    
    }
   
    private void spawnGrapes(GameObject grapePrefab) { 
        Grape grape = grapePrefab.GetComponent<Grape>();        
        lastPositions = new ArrayList();
       
        for(int i = 0; i < 4; i++) {           
            Vector3 newFruitPosition = calculatePositionInFarms();
            
            if(getProbabilityOf(grape.chanceOfSpawn)) {
                Grape clone = (Grape) Instantiate(grapePrefab, newFruitPosition, Quaternion.identity).GetComponent<Grape>(); 

                clone.setType(spawnType);
                numOfFruitsSpawned++;                 
            }            
        }
    }

    private void spawnPumpkins(GameObject pumpkinPrefab) {
        Pumpkin pumpkin = pumpkinPrefab.GetComponent<Pumpkin>();
        lastPositions = new ArrayList();
       
        for(int i = 0; i < 2; i++) {           
            Vector3 newFruitPosition = calculatePositionInFarms();            
           
            if(getProbabilityOf(pumpkin.chanceOfSpawn)) {
                Pumpkin clone = (Pumpkin) Instantiate(pumpkinPrefab, newFruitPosition, Quaternion.identity).GetComponent<Pumpkin>();    

                clone.setType(spawnType);
                numOfFruitsSpawned++;            
            }            
        }
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
    
    private void spawnHearts(float[][] heartPositions) {
        GameObject heartPrefab = ConsumablePrefabs.Find(cons => cons.GetComponent<Consumable>().typeOfConsumable == "Heart");

        for(int i = 0; i < heartPositions.Length; i++) {
            Instantiate(heartPrefab, new Vector3(heartPositions[i][0], heartPositions[i][1], heartPositions[i][2]), Quaternion.identity).GetComponent<Heart>();           
        } 
    }
    private void spawnEnergies(float[][] energyPositions) {
        GameObject energyPrefab = ConsumablePrefabs.Find(cons => cons.GetComponent<Consumable>().typeOfConsumable == "Energy");

        for(int i = 0; i < energyPositions.Length; i++) {
            Instantiate(energyPrefab, new Vector3(energyPositions[i][0], energyPositions[i][1], energyPositions[i][2]), Quaternion.identity).GetComponent<Energy>();           
        }
    }

    private void spawnGrapes(float[][] grapePositions) {
        GameObject grapePrefab = ConsumablePrefabs.Find(cons => cons.GetComponent<Consumable>().typeOfConsumable == "Grape"); 

        for(int i = 0; i < grapePositions.Length; i++) {
            Grape clone = (Grape) Instantiate(grapePrefab, new Vector3(grapePositions[i][0], grapePositions[i][1], grapePositions[i][2]), Quaternion.identity).GetComponent<Grape>(); 

            clone.setType(spawnType);
        }   

    }

    private void spawnPumpkins(float[][] pumpkinPositions) {
        GameObject pumpkinPrefab = ConsumablePrefabs.Find(cons => cons.GetComponent<Consumable>().typeOfConsumable == "Pumpkin");  

        for(int i = 0; i < pumpkinPositions.Length; i++) {
            Pumpkin clone = (Pumpkin) Instantiate(pumpkinPrefab, new Vector3(pumpkinPositions[i][0], pumpkinPositions[i][1], pumpkinPositions[i][2]), Quaternion.identity).GetComponent<Pumpkin>();   

            clone.setType(spawnType);
        } 

    }
    
    #endregion
        
    #region Common methods
    
    private Vector3 generateRandomVector3(float x1, float x2, float y1, float y2) {
        return new Vector3(Random.Range(x1, x2), Random.Range(y1, y2), 0);
    }

    private int getSpawnType() {
        int spawnType = 0;
        if(this.level >= 3) spawnType = 1;
        if(this.level >= 5) spawnType = 2;

        return spawnType;
    }

    private bool getProbabilityOf(int chance) {
        return Random.Range(0f, 100.0f) >= chance;
    }

    private void initVariables(int level) {
        this.level = level;        
        numOfFruitsSpawned = 0; maxHearts = 1; maxEnergy = 2;        
        spawnType = getSpawnType();     
        fruitSpawners = GameObject.FindGameObjectsWithTag("FruitSpawner");  
    }

    private void cleanLevelBeforeSpawn() {
        foreach (GameObject consumable in GameObject.FindGameObjectsWithTag("Consumable"))
        {
            Destroy(consumable);
        }  
    }
    
    #endregion
}