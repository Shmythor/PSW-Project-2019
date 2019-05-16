using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 using System.Linq;
 // Load all sprites in atlas


public class UIController : MonoBehaviour
{
    
    private int calories, cont, hearts;
    private float damage;

    [SerializeField] private Text heartText, caloryText;
    [SerializeField] private SimpleHealthBar healthBar;
     [SerializeField] private UITimer UITimer;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    /// 

    // Singleton
    public static UIController instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        
    }


    void Start()
    {
        calories = 0; cont = 0; damage = 0;
        setCaloriesText();
        setDamageBar();       
    }


    

    void updateCalories(int c) {
        cont += c; calories += c;
        
        if(cont >= 100) {
            cont -= 100;
            restoreEnergy();            
        }
        setCaloriesText();
    }

    void setCaloriesText() {
        caloryText.text = calories.ToString();
    }

    public void setCalories(int calories) { this.calories = calories; setCaloriesText(); }
    public void setDamage(float damage) { 
        this.damage = damage;
        setDamageBar();
    }

    public void setDamageBar() {
        healthBar.UpdateBar(100 - this.damage, 100);
    }
    public void setHearts(int hearts) {
        heartText.text = hearts.ToString(); 
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
        GameController.instance.startLevel();
    }

    public void resetTime() {
        UITimer.resetTime();
    }
}
