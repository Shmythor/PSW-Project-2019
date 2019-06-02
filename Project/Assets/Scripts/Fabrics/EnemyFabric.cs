using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFabric : MonoBehaviour
{
    public static EnemyFabric instance = null;
    [Header("Enemies")]
    [SerializeField] private GameObject impPrefab;
    [SerializeField] private GameObject bunnyPrefab;


    Transform player;

   

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }


    public List<IEnemy> spawnEnemies(int level)
    {

        player = GameController.instance.getPlayerTransform();

        List<IEnemy> listOfEnemies = new List<IEnemy>();
        int amountOfEnemies = 1;
        if(level>=4) amountOfEnemies = 2;
        if(level>=6) amountOfEnemies = 3;

        for(int i = 0; i < amountOfEnemies; i++) {
            listOfEnemies.Add(Instantiate(impPrefab, getRandomPosition(), Quaternion.identity).GetComponent<IEnemy>());
            listOfEnemies.Add(Instantiate(bunnyPrefab, getRandomPosition(), Quaternion.identity).GetComponent<IEnemy>());
        }

        return listOfEnemies;
    }




    public List<IEnemy> spawnEnemies(GameDataSerializable data)
    {
        List<IEnemy> listOfEnemies = new List<IEnemy>();        
        float[][] bunnyPositions = data.bunnyPositions, impPositions = data.impPositions;
        for(int i = 0; i < impPositions.Length; i++) {
            listOfEnemies.Add(Instantiate(impPrefab, new Vector3(impPositions[i][0], impPositions[i][1], impPositions[i][2]), Quaternion.identity).GetComponent<IEnemy>()); 
        }

        for(int i = 0; i < bunnyPositions.Length; i++) {
            listOfEnemies.Add(Instantiate(bunnyPrefab, new Vector3(bunnyPositions[i][0], bunnyPositions[i][1], bunnyPositions[i][2]), Quaternion.identity).GetComponent<IEnemy>());            
        }

        return listOfEnemies;
    }

    private Vector3 getRandomPosition()
    {
        Vector3 position = new Vector3(Random.Range(-11.0f, 11.0f), Random.Range(-9.0f, 9.0f), 0);
        while (Vector3.Distance(position, player.position) < 5)
        {
            position = new Vector3(Random.Range(-11.0f, 11.0f), Random.Range(-9.0f, 9.0f), 0);
        }

        return position;
    }
}
