using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    // Singleton
    public static LevelController instance = null;

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
 


    public void changeLevel(int level)
    {
        SceneManager.LoadScene(level, LoadSceneMode.Single);
        
    }

    public void restartLevel()
    {
        int i = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(i, LoadSceneMode.Single);
    }

    public void nextLevel()
    {
        
        int i = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(++i, LoadSceneMode.Single);
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
