using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    /*          Singleton          */
    public static LevelController instance = null;
    private int currentLevel;

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
        GameController.instance.setLevel(level);
        GameController.instance.startLevel();
    }
    
    public void startGame() {
        SceneManager.LoadScene(1);

        this.currentLevel = 1;
        /*
        GameController.instance.setLevel(currentLevel);
        GameController.instance.startLevel();
        */
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
        GameController.instance.setLevel(currentLevel);
        GameController.instance.startLevel();      
        
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
