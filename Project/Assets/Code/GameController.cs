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
    [SerializeField] private UIController UIController;

    [Header("Music")]
    [SerializeField] private MusicController MusicController;

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
    public void updatePlayerHealth(int hearts, float damage, SoundsEnum.soundEffect[] sounds) {       
        UIController.setHearts(hearts);
        UIController.setDamage(damage);         

        foreach (SoundsEnum.soundEffect sound in sounds)
        {
            MusicController.playSoundEffect(sound);
        }   
    }


    public void consumeCalories(int calories)
    {
        this.calories += calories;
        caloriesToRestore += calories;
        if (caloriesToRestore > 100){
            player.restoreDamageTaken();
            caloriesToRestore -= 100;
        }

        UIController.setCalories(this.calories);

        
        
        float rndSound = Random.Range(1f, 3f);

        if(rndSound <= 1) MusicController.playSoundEffect(SoundsEnum.soundEffect.greedy_eat1);      
        if(rndSound>1 && rndSound<= 2) MusicController.playSoundEffect(SoundsEnum.soundEffect.greedy_eat2);
        if(rndSound>2 && rndSound<= 3) MusicController.playSoundEffect(SoundsEnum.soundEffect.greedy_eat3);        
    }
    
    public void restoreHealth() {
        UIController.restoreHealth();
        MusicController.playSoundEffect(SoundsEnum.soundEffect.ui_heartFill);
    }

    public void restoreEnergy() {
        UIController.restoreEnergy();
        MusicController.playSoundEffect(SoundsEnum.soundEffect.ui_damageRestored);
    }   
    
    
}
