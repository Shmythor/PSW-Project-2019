using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;



public class UIController : MonoBehaviour
{
    [Header("Referencess")]
    [SerializeField] private Text heartText, caloryText;
    [SerializeField] private SimpleHealthBar healthBar;
    [SerializeField] private UITimer UITimer;

    [Header("UI")]
    [SerializeField] private GameObject gamewinScreen, gameoverScreen, pauseScreen, saveScreen;


    /*          Singleton          */
    public static UIController instance = null;

    #region Setters & Getters 
    private int calories;
    public int Calories {
        get => calories;
        set {
            calories = value;
            caloryText.text = calories.ToString();
        }
    }

    private int hearts;
    public int Hearts {
        get => hearts;
        set {
            hearts = value;
            heartText.text = hearts.ToString();
        }
    }

    private float damage;
    public float Damage {
        get => damage;
        set {
            damage = value;
            healthBar.UpdateBar(100 - this.damage, 100);
        }
    }
    #endregion
   
    void Awake()
    {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            //There can only ever be one instance of this object!!
            Destroy(gameObject);  
        }
    }

    #region UIContainer

    public void initializeTimer() {
        this.stopTimer();
        this.resetTimer();
        this.restartTimer();  
    }
    public void initUIStats() {
        Calories = 0;
        Damage = 0;
        Hearts = 3;    
    }
    public void setUIStats(float damage, float time, int hearts, int calories) {
        Calories = calories;
        Damage = damage;
        Hearts = hearts;   
        Time = time;      
    }
    public void setUIContainer(bool set) {
        this.gameObject.SetActive(set);
    }
    
    #endregion    

    #region UIScreens: active & inactive   

    public void setUIScreens(bool set)
    {
        gameoverScreen.SetActive(set);
        gamewinScreen.SetActive(set);
        pauseScreen.SetActive(set);
        saveScreen.SetActive(set);        
    }    
    public void GameOver()
    {       
        pauseGame(false);
        initUIStats();       
        gameoverScreen.SetActive(true);
        gameoverScreen.GetComponent<GameOver>().setCaloriesText(calories);        
    }
    public void GameWin()
    {
        pauseGame(false);

        MusicController.instance.playSoundEffect(SoundsEnum.soundEffect.fireworks_launch);
        MusicController.instance.playSoundEffect(SoundsEnum.soundEffect.fireworks_explosion);

        gamewinScreen.SetActive(true);
        gamewinScreen.GetComponent<AScreen>().setCaloriesText(calories);

        /* Si terminamos el último nivel, guardamos puntuaciones */
        if(GameController.instance.Level == 6) {
           GameController.instance.saveTopGame();
        }
    }
    public void pauseGame(bool activatePauseScreen)
    {
        GameController.instance.stopGame();
        if (activatePauseScreen == true)
        { 
            pauseScreen.SetActive(true);
            pauseScreen.GetComponent<AScreen>().setCaloriesText(calories);
        }
    }
    public void saveGame() {
        pauseScreen.SetActive(false);
        saveScreen.SetActive(true);        
    }   
    public void resumeGame()
    {
        GameController.instance.restartGame();        
        pauseScreen.SetActive(false);
        saveScreen.SetActive(false);
        MusicController.instance.resumeMainSong();
    }

    #endregion

    #region Timer methods 
        
    private float time;
    public float Time {
        get => UITimer.getTime();
        set {
            UITimer.setTime(value);
        }
    }
    public void resetTimer() {
        UITimer.resetTimer();
    }
    public void restartTimer() {
        UITimer.restartTimer();
    }
    public void stopTimer() {
        UITimer.stopTimer();
    }

    #endregion

}
