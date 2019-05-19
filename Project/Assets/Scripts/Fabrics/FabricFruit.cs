using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabricFruit : MonoBehaviour
{
    /* Start is called before the first frame update */
    public GameObject[] GrapePrefabs, PumpkinPrefabs;
    public GameObject UIController;
    public int level;
    public static FabricFruit instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void spawnFruit(int level) {
        for(int i = 0; i < 10; i++) {
            if(Random.Range(0f, 10.0f) >= 8f) {
                Instantiate(PumpkinPrefabs[level - 1], new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0), Quaternion.identity);
            } else {
                Instantiate(GrapePrefabs[level - 1], new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0), Quaternion.identity);
            }            
        }        
    }

    public void consumeCalories(int calories) {
        UIController.SendMessage("updateCalories", calories);
    }

    

    
}
