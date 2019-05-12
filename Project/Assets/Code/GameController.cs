using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{


    [Header("Level")]
    [SerializeField] private int level;
    [SerializeField] private int caloriesThisLevel;
    [SerializeField] private int caloriesToWin;

    [Header("Player")]
    [SerializeField] private IPlayer player;
    [SerializeField] private int calories = 0;    
    [SerializeField] private int caloriesToRestore = 0;

    [Header("UI")]
    [SerializeField] private GameObject main_UI;
    [SerializeField] private GameObject gamewinScreen;
    [SerializeField] private GameObject gameoverScreen;
    [SerializeField] private GameObject pauseScreen;



    // Other
    private LinkedList<IEnemy> enemies;

    // Singleton
    public static GameController instance = null;


    public void setLevel(int level)
    {
        this.level = level;
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // ONLY FOR DEBUG
        // startLevel();
    }


    public void startLevel()
    {
        if (level > 0)
        {
            caloriesThisLevel = 0;
            caloriesToRestore = 0;
            main_UI.SetActive(true);
            player = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<IPlayer>();
            player.restoreHealth();
            spawn();
            UIController.instance.setCalories(this.calories);
        }
        else
            main_UI.SetActive(false);
        disativateUIScreens();
    }
    

    private void spawn()
    {
        enemies = new LinkedList<IEnemy>();
        caloriesToWin = consumableFabric.instance.spawnFruit(level);
        consumableFabric.instance.spawnConsumable(level);
        enemies = EnemyFabric.instance.spawnImps(level);
    }

    // UI update stats
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
        
        // If the player has consumed all the fruits invoke the gameWin method
        if(caloriesThisLevel == caloriesToWin)
        {
            GameWin();
        }
    }
    
    public void restoreHealth() {
        UIController.instance.restoreHealth();
        MusicController.instance.playSoundEffect(SoundsEnum.soundEffect.ui_heartFill);
    }

    public void restoreEnergy() {
        UIController.instance.restoreEnergy();
        MusicController.instance.playSoundEffect(SoundsEnum.soundEffect.ui_damageRestored);
    }   
    

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
        gamewinScreen.SetActive(true);
        gamewinScreen.GetComponent<AScreen>().setCaloriesText(calories);
    }

    public void pauseGame(bool activatePauseScreen)
    {
        player.disableInputs();
        foreach (IEnemy enemy in enemies)
            enemy.stopEnemyMovement();
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
            enemy.resumeEnemyMovement();
        pauseScreen.SetActive(false);
    }


    public void setCaloriesToZero() { calories = 0; } // For exiting to the menu


}
