﻿using System.Collections;
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

    

    /*
        Start is called on the frame when a script is enabled just before
        any of the Update methods is called the first time.
    */



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
