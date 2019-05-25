using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{


    [Header("Level")]
    [SerializeField] private int level, caloriesThisLevel, caloriesToWin, lastLevel = 3;

    [Header("Player")]
    [SerializeField] private IPlayer player;
    [SerializeField] private int calories, caloriesToRestore;    

    [Header("UI")]
    [SerializeField] private GameObject mainUI, gamewinScreen, gameoverScreen, pauseScreen;



    /*          Other          */
    private List<IEnemy> enemies;

    /*          Singleton          */
    public static GameController instance = null;


    public void setLevel(int level){ this.level = level; }
    public int getLevel() { return level; }
    public bool isIsLastLevel() { return level == lastLevel ? true : false; }
    public void setCaloriesToZero() { calories = 0; } /* For exiting to the menu */


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);


        player = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<IPlayer>();
        player.restoreHealth();

        enemies = new List<IEnemy>();

        UIController.instance.initUIStats();
    }

    private void Start()
    {
                
    }


    public void startLevel()
    {       
        if (level > 0)
        {
            /* Set map */        
            MapController.instance.setMap(level);

            /* Set Greedy */
            //RND SPAWN?
            player.enableInputs();
            
            /* Spawn Consumables */
            caloriesThisLevel = 0;
            caloriesToRestore = 0;
            spawnConsumables();

            /* Spawn Enemies */            
            spawnEnemies();

            /* Set UI */      
            mainUI.SetActive(true);  
            UIController.instance.resetUIStats();    

            /* Set music */   
            MusicController.instance.playMainSong();

           
        }
        else
            mainUI.SetActive(false);
        disativateUIScreens();
    }
    
    
    private void spawnConsumables()
    {
        
        caloriesToWin = consumableFabric.instance.spawnConsumables(level);
        
    }

    private void spawnEnemies() {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }
        enemies = EnemyFabric.instance.spawnImps(level);
    }

    /*          UI update states          */
    public void updatePlayerHealth(int hearts, float damage, SoundsEnum.soundEffect[] sounds) {       
        
        UIController.instance.setHearts(hearts);
        UIController.instance.setDamage(damage);         

        foreach (SoundsEnum.soundEffect sound in sounds)
        {
            MusicController.instance.playSoundEffect(sound);
        }   
    }

    public void updatePlayerHealth(int hearts, float damage)
    {
        UIController.instance.setHearts(hearts);
        UIController.instance.setDamage(damage);
    }


    public void consumeCalories(int calories)
    {
        this.calories += calories;
        caloriesThisLevel += calories;
        caloriesToRestore += calories;
        if (caloriesToRestore > 100){
            player.restoreDamageTaken();
            caloriesToRestore -= 100;
        }

        UIController.instance.setCalories(this.calories);

        
        
        float rndSound = Random.Range(1f, 3f);

        if(rndSound <= 1) MusicController.instance.playSoundEffect(SoundsEnum.soundEffect.greedy_eat1);      
        if(rndSound>1 && rndSound<= 2) MusicController.instance.playSoundEffect(SoundsEnum.soundEffect.greedy_eat2);
        if(rndSound>2 && rndSound<= 3) MusicController.instance.playSoundEffect(SoundsEnum.soundEffect.greedy_eat3);      
        
        /* If the player has consumed all the fruits invoke the gameWin method      */
        if(caloriesThisLevel == caloriesToWin)
        {
            GameWin();
        }
    }
    
    public void restoreHealth() {
        player.restoreHealth();
        UIController.instance.restoreHealth();
        MusicController.instance.playSoundEffect(SoundsEnum.soundEffect.ui_heartFill);
    }

    public void restoreEnergy() {
        player.restoreDamageTaken();
        UIController.instance.restoreEnergy();
        MusicController.instance.playSoundEffect(SoundsEnum.soundEffect.ui_damageRestored);
    }   

    /* UI SCREENS  */
    

    private void disativateUIScreens()
    {
        gameoverScreen.SetActive(false);
        gamewinScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }
    
    public void GameOver()
    {
        pauseGame(false);
        calories = 0; 
        gameoverScreen.SetActive(true);
        gameoverScreen.GetComponent<AScreen>().setCaloriesText(calories);

        
    }

    public void GameWin()
    {
        pauseGame(false);

        MusicController.instance.playSoundEffect(SoundsEnum.soundEffect.fireworks_launch);
        MusicController.instance.playSoundEffect(SoundsEnum.soundEffect.fireworks_explosion);

        gamewinScreen.SetActive(true);
        gamewinScreen.GetComponent<AScreen>().setCaloriesText(calories);
    }

    public void pauseGame(bool activatePauseScreen)
    {
        player.disableInputs();
        foreach (IEnemy enemy in enemies)
            enemy.stopEnemy();
        if (activatePauseScreen == true)
        {
            pauseScreen.SetActive(true);
            pauseScreen.GetComponent<AScreen>().setCaloriesText(calories);
        }

    }

    public void resumeGame()
    {
        player.enableInputs();
        foreach (IEnemy enemy in enemies)
            enemy.resumeEnemy();
        pauseScreen.SetActive(false);
        MusicController.instance.resumeMainSong();
    }

}
