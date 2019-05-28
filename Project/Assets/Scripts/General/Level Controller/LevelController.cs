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
        /*          Menu and login scene        */
        if (SceneManager.GetActiveScene().buildIndex < 2)
            return;

        /*     We load the map only if we are in Game Scene             */
        if(data != null) {
            GameController.instance.startLevel(data);
            data = null;
        } else {
            Debug.Log("Voy a ponerme al nivel: " + this.currentLevel.ToString());
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
        SceneManager.LoadScene(1);

    }
}
