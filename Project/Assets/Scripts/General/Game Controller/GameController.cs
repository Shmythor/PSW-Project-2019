using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    
    [Header("Level")]
    [SerializeField] private int level, caloriesThisLevel, lastLevel = 6;

    [Header("Player")]
    [SerializeField] private GameObject playerGameObject;
    [SerializeField] private IPlayer player;

    private int calories, contOfCaloriesToRestore, fruitsToEat, fruitsEaten, totalEnergiesPicked, totalHeartsPicked;   

    private const int CALORIES_TO_RESTORE = 100;
  
    /*          Other          */
    private List<IEnemy> enemies;

    /*          Singleton          */
    public static GameController instance = null;

    public int Level {
        get => this.level;
        set => this.level = value;
    }
    
    public bool isIsLastLevel() { return level == lastLevel ? true : false; }
    public void setCaloriesToZero() { calories = 0; } /* For exiting to the menu */
    public Transform getPlayerTransform() { return playerGameObject.transform; }

    void Awake()
    {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            //There can only ever be one instance of this object!!
            Destroy(gameObject);  
        }

        initializeVariables();          
    }
    #region startLevel methods
    public void startLevel(int level)
    {       
        initializeVariables(level);        

        if (this.level > 0)
        {
            MapController.instance.setMap(this.level);

            initializePlayer();

            spawnConsumables();
            spawnEnemies();

            initializeUI();

            MusicController.instance.playMainSong();
        }
        else
            UIController.instance.setUIContainer(false);        
    }

    public void startLevel(GameDataSerializable data)
    {
        initializeVariables(data);

        MapController.instance.setMap(this.level);

        initializePlayer(data);

        spawnConsumables(data);
        spawnEnemies(data);

        initializeUI(data);

        MusicController.instance.playMainSong();
    }    

    #region StartLevel - refactor methods

        private void initializeVariables() {
            this.level = 1;

            playerGameObject = GameObject.FindGameObjectWithTag("Player");
            player = playerGameObject.transform.GetComponent<IPlayer>();           
        
            enemies = new List<IEnemy>();     
        }

        private void initializeVariables(int level) {
            this.level = level;

            playerGameObject = GameObject.FindGameObjectWithTag("Player");
            player = playerGameObject.transform.GetComponent<IPlayer>();         

            if(level == 1) {
                totalEnergiesPicked = 0;
                totalHeartsPicked = 0;
            }

            caloriesThisLevel = 0;
            contOfCaloriesToRestore = 0;
            fruitsEaten = 0;
            fruitsToEat = 0;
        }

        private void initializeVariables(GameDataSerializable data) {
            this.level = data.level;

            playerGameObject = GameObject.FindGameObjectWithTag("Player");
            player = playerGameObject.transform.GetComponent<IPlayer>();         

            this.calories = data.calories;   
            caloriesThisLevel = 0;
            contOfCaloriesToRestore = 0;
            fruitsEaten = data.fruitsEaten;
            fruitsToEat = data.fruitsToEat;
        }

        private void initializePlayer(GameDataSerializable data)
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(
                        data.greedyPosition[0], data.greedyPosition[1], data.greedyPosition[2]);
            player.updateHealthFromLoadGameData(data.hearts, data.damage);
            player.enableInputs();
        }
        private void initializePlayer()
        {
            player.enableInputs();
            player.restoreHealth(); /* updates UI elements as well */
        }
        
        private void initializeUI()
        {
            UIController.instance.setUIContainer(true);
            UIController.instance.initializeTimer();
            UIController.instance.Calories = this.calories;

            UIController.instance.setUIScreens(false);
        }
        private static void initializeUI(GameDataSerializable data)
        {
            UIController.instance.setUIContainer(true);
            UIController.instance.setUIStats(data.damage, data.time, data.hearts, data.calories);
            UIController.instance.setUIScreens(false);
            UIController.instance.restartTimer();
        }
    
        private void spawnConsumables()
        {      
            fruitsToEat = ConsumableFabric.instance.spawnConsumables(level);        
        }
        private void spawnConsumables(GameDataSerializable data)
        {
            ConsumableFabric.instance.spawnConsumables(data);
        }
        private void spawnEnemies() {        
            enemies = EnemyFabric.instance.spawnEnemies(level);        
        }
        private void spawnEnemies(GameDataSerializable data) {  
            enemies = EnemyFabric.instance.spawnEnemies(data);
        }
       
        #endregion
    #endregion
    
    #region Player stats' methods
    public void updatePlayerHealth(int hearts, float damage, SoundsEnum.soundEffect[] sounds) {       
        UIController.instance.Hearts = hearts;
        UIController.instance.Damage = damage;         

        foreach (SoundsEnum.soundEffect sound in sounds) {
            MusicController.instance.playSoundEffect(sound);
        }   
    }

    public void updatePlayerHealth(int hearts, float damage)
    {
        UIController.instance.Hearts = hearts;
        UIController.instance.Damage = damage;
        UIController.instance.Calories = this.calories;
    }


    public void consumeCalories(int calories, bool isFruit)
    {
        updateCalories(calories, isFruit);
        if (isFruit) MusicController.instance.playConsumeCaloriesSoundEffect();        
        applyLogicOfUpdateCalories();
    }


    
        private void applyLogicOfUpdateCalories()
        {
            /* Every 100 calories consumed restore all damage of Greedy */
            if (contOfCaloriesToRestore > CALORIES_TO_RESTORE)
            {
                player.restoreDamageTaken();
                contOfCaloriesToRestore -= CALORIES_TO_RESTORE;
            }

            UIController.instance.Calories = this.calories;

            /* If the player has consumed all the fruits invoke the gameWin method */
            if (fruitsEaten >= fruitsToEat)
            {
                UIController.instance.GameWin();
            }
        }
        private void updateCalories(int calories, bool isFruit)
        {
            if (isFruit) { fruitsEaten++; }

            this.calories += calories;
            caloriesThisLevel += calories;
            contOfCaloriesToRestore += calories;        
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

    public void updateSpeed(float speed, int seconds) {
        MusicController.instance.playSoundEffect(SoundsEnum.soundEffect.velocityPotion);
        player.increaseSpeedForXSeconds(speed, seconds);
    } 

    #endregion

    #region stop & restart methods

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

    #endregion

    #region SaveLoad Methods

    public void saveGameAt(SaveLoad.paths path) {
        GameDataSerializable data = new GameDataSerializable(new GameData(
            GameObject.FindGameObjectsWithTag("Consumable"),
            GameObject.FindGameObjectsWithTag("Enemy"),
            GameObject.FindGameObjectWithTag("Player"),
            this.level,
            UIController.instance.Hearts,
            UIController.instance.Calories,
            this.fruitsEaten,
            this.fruitsToEat,
            UIController.instance.Damage,
            UIController.instance.Time
        )); 

        SaveLoad.saveGameDataAt(data, path);
    }    

    public void saveTopGame() {
        TopGameData[] topData = SaveLoad.loadAllTopGameData();
        TopGameData newTGD = new TopGameData(this.totalHeartsPicked, this.totalEnergiesPicked, this.calories, UIController.instance.Time);
        
        
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
