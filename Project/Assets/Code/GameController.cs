using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Fabrics")]
    [SerializeField] private EnemyFabric enemyFabric;
    [SerializeField] private consumableFabric consumableFabric;

    [Header("Map")]
    [SerializeField] private GameObject TileMap;

    [Header("Level")]
    [SerializeField] private int level = 1;

    [Header("Player")]
    [SerializeField] private IPlayer player;
    [SerializeField] private int calories = 0;
    [SerializeField] private int caloriesToRestore = 0;

    [Header("UI")]
    [SerializeField] private UIController UI;

    // Singleton
    public static GameController instance = null;



    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        consumableFabric.spawnFruit(level);
        consumableFabric.spawnConsumable(level);
        
        enemyFabric.spawnImps(level);
    }

  
    // UI update stats
    public void updatePlayerHealth(int hearts, float damage)
    {
        //if(heart lose)
         //SoundController.(Sound.HeartLoss)
         
        UI.setHearts(hearts);
        UI.setDamage(damage);

        //else
         //SoundController.(Sound.HeartWin)

        
    }


    public void consumeCalories(int calories)
    {
        this.calories += calories;
        caloriesToRestore += calories;
        if (caloriesToRestore > 100){
            player.restoreDamageTaken();
            caloriesToRestore -= 100;
        }
        UI.setCalories(this.calories);
    }
    
    public void restoreHealth() {
        UI.SendMessage("restoreHealth");
    }

    public void restoreEnergy() {
        UI.SendMessage("restoreEnergy");
    }   
    
}
