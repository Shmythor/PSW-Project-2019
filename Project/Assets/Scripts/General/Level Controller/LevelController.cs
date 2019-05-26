using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    /*          Singleton          */
    public static LevelController instance = null;
    private int currentLevel;

    private GameDataSerializable data;

    void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);        
    }


    private void OnLevelWasLoaded(int level)
    {   
        if(data != null) {
            GameController.instance.startLevel(data);
            data = null;
        } else {
            GameController.instance.setLevel(level);
            GameController.instance.startLevel();
        }    
        
    }
    
    public void startGame() {
        SceneManager.LoadScene(1);

        this.currentLevel = 1;
    }

    public void loadGame(SaveLoad.paths path) {
        SceneManager.LoadScene(1);

        data = SaveLoad.loadGameDataAt(path);
        this.currentLevel = data.level;
    }


    public void changeLevel(int level)
    {
        this.currentLevel = level;

        GameController.instance.setLevel(currentLevel);
        GameController.instance.startLevel();
    }

    public void restartLevel()
    {
        UIController.instance.resetTimer(); 
        GameController.instance.setLevel(currentLevel);
        GameController.instance.startLevel(); 
    }

    public void nextLevel()
    {    
        currentLevel++;
        if(currentLevel<6) {
            GameController.instance.setLevel(currentLevel);
            GameController.instance.startLevel();
        }    
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
