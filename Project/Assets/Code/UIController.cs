using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    public Text caloryText;
    private int calories;

     /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        calories = 0;
        caloryText.text = calories.ToString();
    }

    void updateCalories(int c) {
        calories += c;
        if(calories >= 100) {
            restorePlayerEnergy();
            calories -= 100;
        }
        caloryText.text = calories.ToString();
    }

    void restorePlayerEnergy() {
        //TODO
    }
}
