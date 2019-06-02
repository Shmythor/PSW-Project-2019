using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{


    [Header("Level")]
    [SerializeField] private int level, caloriesThisLevel, caloriesToWin, lastLevel = 6;

    [Header("Player")]
    [SerializeField] private GameObject playerGameObject;
    [SerializeField] private IPlayer player;
    [SerializeField] private int calories, caloriesToRestore;    



    private int totalEnergiesPicked, totalHeartsPicked;



    /*          Other          */
    private List<IEnemy> enemies;

    /*          Singleton          */
    public static GameController instance = null;




    public void setLevel(int level){ this.level = level; }
    public int getLevel() { return level; }
    public bool isIsLastLevel() { return level == lastLevel ? true : false; }
    public void setCaloriesToZero() { calories = 0; } /* For exiting to the menu */
    public Transform getPlayerTransform() { return playerGameObject.transform; }


    


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);


        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        player = playerGameObject.transform.GetComponent<IPlayer>();
        this.level = 1;
        totalEnergiesPicked = 0;
        totalHeartsPicked = 0;
        
        enemies = new List<IEnemy>();        
    }
    public void startLevel(int level)
    {       
        this.level = level;

        if(level == 1) {
            totalEnergiesPicked = 0;
            totalHeartsPicked = 0;
        }

        if (this.level > 0)
        {
            
            /* Set map */        
            MapController.instance.setMap(this.level);

            /* Set Greedy */
            //RND SPAWN?
            player.enableInputs();
            player.restoreHealth(); /* updates UI elements as well */

            /* Spawn Consumables */
            caloriesThisLevel = 0;
            caloriesToRestore = 0;
            spawnConsumables();

            /* Spawn Enemies */            
            spawnEnemies();

            /* Set UI */      
            UIController.instance.setUIContainer(true); 
            UIController.instance.resetUIStats();
            UIController.instance.setCalories(calories);
            /* Set music */   
            MusicController.instance.playMainSong();

          
        }
        else
            UIController.instance.setUIContainer(false);
        UIController.instance.setUIScreens(false);
    }

    public void startLevel(GameDataSerializable data)
    {       
       /* Set map */        
        this.level = data.level;
        MapController.instance.setMap(level);

        /* Set Greedy */        
        //GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(data.greedyPosition[0], data.greedyPosition[1], data.greedyPosition[2]);
        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(0.79f, -5.99f,0);
        player.updateHealthFromLoadGameData(data.hearts, data.damage);
        player.enableInputs();

        /* Spawn Consumables */        
        caloriesThisLevel = 0;
        caloriesToRestore = 0;
        spawnConsumables(data);

        /* Spawn Enemies */            
        spawnEnemies(data);

        /* Set UI */      
        UIController.instance.setUIContainer(true);
        this.calories = data.calories;
        UIController.instance.setUIStats(data.damage, data.time, data.hearts, data.calories);   
        UIController.instance.restartTimer();
        /* Set music */   
        MusicController.instance.playMainSong();

        UIController.instance.setUIScreens(false);
    }    
    
    private void spawnConsumables()
    {
        foreach (GameObject consumable in GameObject.FindGameObjectsWithTag("Consumable"))
        {
            Destroy(consumable);
        }        
        caloriesToWin = consumableFabric.instance.spawnConsumables(level);        
    }

    private void spawnConsumables(GameDataSerializable data)
    {
         foreach (GameObject consumable in GameObject.FindGameObjectsWithTag("Consumable"))
        {
            Destroy(consumable);
        }  
        caloriesToWin = consumableFabric.instance.spawnConsumables(data);
    }

    private void spawnEnemies() {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }
        enemies = EnemyFabric.instance.spawnEnemies(level);        
    }

    private void spawnEnemies(GameDataSerializable data) {  
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }     
       
        enemies = EnemyFabric.instance.spawnEnemies(data);
    }

    #region Player stats' methods
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
        UIController.instance.setCalories(calories);
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
            UIController.instance.GameWin();
        }
    } 

    public void restoreHealth() {
        this.totalHeartsPicked++;
        player.restoreHealth();        
        MusicController.instance.playSoundEffect(SoundsEnum.soundEffect.ui_heartFill);
    }

    public void restoreEnergy() {
        this.totalEnergiesPicked++;
        player.restoreDamageTaken();
        MusicController.instance.playSoundEffect(SoundsEnum.soundEffect.ui_damageRestored);
    }   

    #endregion

    public void stopGame() {
        player.disableInputs();
        foreach (IEnemy enemy in enemies)
            enemy.stopEnemy();
    }

    public void restartGame() {
        player.enableInputs();
        foreach (IEnemy enemy in enemies)
            enemy.resumeEnemy();
    }

    #region SaveLoad Methods

     

      public void saveGameAt(SaveLoad.paths path) {
        GameDataSerializable data = new GameDataSerializable(new GameData(
            GameObject.FindGameObjectsWithTag("Consumable"),
            GameObject.FindGameObjectsWithTag("Enemy"),
            GameObject.FindGameObjectWithTag("Player"),
            this.level,
            UIController.instance.getHearts(),
            UIController.instance.getCalories(),
            UIController.instance.getDamage(),
            UIController.instance.getTime()
        )); 

        SaveLoad.saveGameDataAt(data, path);
    }    

    public void saveTopGame() {
        TopGameData[] topData = SaveLoad.loadAllTopGameData();
        TopGameData newTGD = new TopGameData(this.totalHeartsPicked, this.totalEnergiesPicked, this.calories, UIController.instance.getTime());
        
        
        for(int i = 0; i<3; i++) {
            if(compareTo(topData[i], newTGD) == 1) {
                if(i>0) {topData[i-1] = topData[i];}
                topData[i] = newTGD;
            }
        }       

        

        SaveLoad.saveTopGameDataAt(topData[0], SaveLoad.paths.top1);
        SaveLoad.saveTopGameDataAt(topData[1], SaveLoad.paths.top2);
        SaveLoad.saveTopGameDataAt(topData[2], SaveLoad.paths.top3);
    }

  

    private int compareTo(TopGameData old, TopGameData nw) {
        if (old.calories == nw.calories) {
            if(old.time < nw.time) return -1;
            if(old.time > nw.time) return 1;
            return 0;
        }
           
        if (old.calories < nw.calories) {            
            return 1;
        }

        return -1;
    }
    
    #endregion


}
