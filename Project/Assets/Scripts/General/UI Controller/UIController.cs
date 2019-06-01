using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;



public class UIController : MonoBehaviour
{

    /*          Singleton          */
    public static UIController instance = null;
    private int calories, cont, hearts;
    private float damage;
    [Header("Referencess")]
    [SerializeField] private Text heartText, caloryText;
    [SerializeField] private SimpleHealthBar healthBar;
    [SerializeField] private UITimer UITimer;

     [Header("UI")]
    [SerializeField] private GameObject gamewinScreen, gameoverScreen, pauseScreen, saveScreen;


    void setCaloriesText() { caloryText.text = calories.ToString(); }
    public void setCalories(int calories) { this.calories = calories; setCaloriesText(); }
    public void setDamageBar() { healthBar.UpdateBar(100 - this.damage, 100); }
    public void setHearts(int hearts) { heartText.text = hearts.ToString(); }
    public void setDamage(float damage){
        this.damage = damage;
        setDamageBar();
    }

    public int getCalories() {
        return calories;
    }

    public int getHearts() {
        return hearts;
    }

    public float getDamage() {
        return damage;
    }

    public float getTime() {
       return UITimer.getTime();
    }

    private void Awake()
    {
        if (instance == null) 
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        
    }

    public void resetUIStats() {
        this.stopTimer();
        this.resetTimer();
        this.restartTimer();  
    }

    public void initUIStats() {
        setCalories(0);
        setDamage(0);
        setHearts(3);        
    }

    public void setUIStats(float damage, float time, int hearts, int calories) {
        setCalories(calories);
        setDamage(damage);
        setHearts(hearts);
        setTime(time);      
    }
   

    public void restoreHealth() {
       if(this.hearts < 3) {
           setHearts(this.hearts + 1);
        }      
    }

    public void restoreEnergy() {
        setDamage(0);
    }

    public void pauseButton() {
        pauseGame(true);
    }

    
    #region UI SCREENS   

    public void setUIScreens(bool set)
    {
        gameoverScreen.SetActive(set);
        gamewinScreen.SetActive(set);
        pauseScreen.SetActive(set);
        saveScreen.SetActive(set);        
    }

    public void setUIContainer(bool set) {
        this.gameObject.SetActive(set);
    }
    
    public void GameOver()
    {       
        pauseGame(false);
        initUIStats();       
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

        /* Si terminamos el último nivel, guardamos puntuaciones */
        if(GameController.instance.getLevel() == 6) {
           GameController.instance.saveTopGame();
        }
    }

    public void pauseGame(bool activatePauseScreen)
    {       
        if (activatePauseScreen == true)
        {
            GameController.instance.stopGame();     
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


    public void resetTimer() {
        UITimer.resetTimer();
    }

    public void restartTimer() {
        UITimer.restartTimer();
    }

    public void stopTimer() {
        UITimer.stopTimer();
    }

    public void setTime(float time) {
       UITimer.setTime(time);
    }
}
