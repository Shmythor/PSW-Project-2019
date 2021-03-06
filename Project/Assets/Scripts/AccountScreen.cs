﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountScreen : MonoBehaviour
{

    public Text statsTitle, statsCalories, statsHearts, statsEnergies;
    public Text playerName;


    private TopGameData[] topData;
    
    void OnEnable()
    {
        getTopStats();
        setTopStatsOf(1);

        string name = UserNames.currentUsername;
        if(name != null) {
            playerName.text=name;
        }
    }

    private void getTopStats() {
        topData = SaveLoad.loadAllTopGameData();        
    }

    public void setTopStatsOf(int topPosition) {
        statsTitle.text = "Stats: Top " + topPosition.ToString();
        
        statsCalories.text = "Total calories: " + topData[3-topPosition].calories.ToString();
        statsHearts.text = "Total hearts: " + topData[3-topPosition].heartsPicked.ToString();
        statsEnergies.text = "Total energies: " + topData[3-topPosition].energiesPicked.ToString();
    }

    public void close() {
         this.gameObject.SetActive(false);
    }
}
