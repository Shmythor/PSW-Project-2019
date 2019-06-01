using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AScreen : MonoBehaviour
{

    [SerializeField] private Text caloriesText;


    public void setCaloriesText(int calories){ 
        caloriesText.text = calories.ToString(); 
    }

    public void exit()
    {        
        GameController.instance.setCaloriesToZero();        
        UIController.instance.setUIContainer(false);       

        

        LevelController.instance.loadMainMenu();
        this.gameObject.SetActive(false);
    }

    
}
