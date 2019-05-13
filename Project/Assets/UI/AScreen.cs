using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AScreen : MonoBehaviour
{

    [SerializeField] private Text caloriesText;

    public void exit()
    {
        GameController.instance.setCaloriesToZero();
        LevelController.instance.loadMainMenu();
    }

    public void setCaloriesText(int calories)
    {
        caloriesText.text = "Score: " + calories.ToString();
    }
}
