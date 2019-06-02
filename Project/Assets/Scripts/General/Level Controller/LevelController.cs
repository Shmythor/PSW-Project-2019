using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    /*          Singleton          */
    public static LevelController instance = null;
    private int currentLevel = 1;

    private GameDataSerializable data;

    void Awake()
    {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            //There can only ever be one instance of this object!!
            Destroy(gameObject);  
        }

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);        
    }


    private void OnLevelWasLoaded(int level)
    {
        /*          Menu and login scene        */
        if (SceneManager.GetActiveScene().buildIndex < 2)
            return;

        /*     We load the map only if we are in Game Scene             */
        if(data != null) {
            GameController.instance.startLevel(data);
            data = null;
        } else {
            GameController.instance.startLevel(this.currentLevel);
        }    
        
    }
    
    public void startGame() {
        this.currentLevel = 1;
        SceneManager.LoadScene(2);
    }

    public void loadGame(SaveLoad.paths path) {
        SceneManager.LoadScene(2);

        data = SaveLoad.loadGameDataAt(path);
        this.currentLevel = data.level;
    }


    public void changeLevel(int level)
    {
        this.currentLevel = level;
        GameController.instance.startLevel(this.currentLevel);
    }

    public void restartLevel()
    {
        UIController.instance.resetTimer();         
        GameController.instance.startLevel(this.currentLevel);
    }

    public void nextLevel()
    {    
        this.currentLevel++;
        if(this.currentLevel<=6) {
            GameController.instance.startLevel(this.currentLevel);
        } 
    }

    public void loadMainMenu()
    {
        UIController.instance.setUIScreens(false);
        UIController.instance.setUIContainer(false);
        SceneManager.LoadScene(1);
    }
}
