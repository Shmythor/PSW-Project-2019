using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFabric : MonoBehaviour
{
    public static EnemyFabric instance = null;
    [Header("Enemies")]
    [SerializeField] private GameObject impPrefab;
    [SerializeField] private GameObject bunnyPrefab;

   

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public List<IEnemy> spawnImps(int level)
    {
        List<IEnemy> listOfEnemies = new List<IEnemy>();
        //TODO decide how many imps we want in each level
        int num = 1;
        if(level>=4) num++;
        //if(level>=5) num++;
        if(level>=6) num++;

        for(int i = 0; i < num; i++) {
            listOfEnemies.Add(Instantiate(impPrefab, new Vector3(Random.Range(-11.0f, 11.0f), Random.Range(-9.0f, 9.0f), 0), Quaternion.identity).GetComponent<IEnemy>());
            listOfEnemies.Add(Instantiate(bunnyPrefab, new Vector3(Random.Range(-11.0f, 11.0f), Random.Range(-9.0f, 9.0f), 0), Quaternion.identity).GetComponent<IEnemy>());
        }

        return listOfEnemies;
    }

    
}
