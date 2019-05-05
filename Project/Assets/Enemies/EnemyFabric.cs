using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFabric : MonoBehaviour
{

    [Header("Enemies")]
    [SerializeField] private GameObject impPrefab;

    private GameController gameController;

    private void Awake()
    {
        
    }

    public void spawnImps(int level)
    {
        //TODO decide how many imps we want in each level
        Instantiate(impPrefab, new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0), Quaternion.identity);
    }

    
}
