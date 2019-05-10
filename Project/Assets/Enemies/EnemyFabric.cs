using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFabric : MonoBehaviour
{

    [Header("Enemies")]
    [SerializeField] private GameObject impPrefab;

    public static EnemyFabric instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public LinkedList<IEnemy> spawnImps(int level)
    {
        LinkedList<IEnemy> listOfEnemies = new LinkedList<IEnemy>();
        //TODO decide how many imps we want in each level
        listOfEnemies.AddFirst(Instantiate(impPrefab, new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0), Quaternion.identity).GetComponent<IEnemy>());
        return listOfEnemies;
    }

    
}
