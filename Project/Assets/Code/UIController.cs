using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    public Text caloryText, damageText;
    private int calories, cont, hearts;
    private float damage;

    [SerializeField] private GameObject heart1, heart2, heart3;
    

     /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
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
            restorePlayerEnergy();            
        }
        setCaloriesText();
    }

    void setCaloriesText() {
        caloryText.text = calories.ToString();
    }

     void setDamageText() {
        damageText.text = damage.ToString() + "%";
    }

    void restorePlayerEnergy() {
        damage = 0;
        setDamageText();
    }


    public void setCalories(int calories) { this.calories = calories; setCaloriesText(); }
    public void setDamage(float damage) { this.damage = damage; setDamageText(); }
    public void setHearts(int hearts) { 
        this.hearts = hearts;
        if(hearts == 2) {
            heart3.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/objects_7");
        } else if(hearts == 1) {
            heart2.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/objects_7");
        } else if(hearts == 0) {
            heart1.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/objects_7");
        }
      /*TODO UI CORAZONES*/ 

    }


}
