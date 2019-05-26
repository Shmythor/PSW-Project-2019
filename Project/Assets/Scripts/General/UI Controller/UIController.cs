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
        GameController.instance.pauseGame(true);
    }

    public void spawnDEBUG()
    {
        GameController.instance.startLevel(SaveLoad.loadGameData());
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

    public void setTime(float time) {
       UITimer.setTime(time);
    }
}
