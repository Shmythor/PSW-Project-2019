using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 using System.Linq;
 // Load all sprites in atlas


public class UIController : MonoBehaviour
{
    public Text caloryText, damageText;
    private int calories, cont, hearts;
    private float damage;

    [SerializeField] private GameObject[] heartPrefabs;
    [SerializeField] private Sprite[] sprites;

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
        setDamageText();
        
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

     void setDamageText() {
        damageText.text = damage.ToString() + "%";
    }

    public void setCalories(int calories) { this.calories = calories; setCaloriesText(); }
    public void setDamage(float damage) { this.damage = damage; setDamageText(); }
    public void setHearts(int hearts) {
        //perdemos vida

        foreach (GameObject heart in heartPrefabs)
        {
            heart.GetComponent<Image>().sprite = sprites[0];
        }
        for(int i = 0; i < hearts; i++)
        {
            heartPrefabs[i].GetComponent<Image>().sprite = sprites[1];
        }

        
        this.hearts = hearts;     
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
}
