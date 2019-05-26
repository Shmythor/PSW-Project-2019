using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountScreen : MonoBehaviour
{

    public GameObject stats;
    public Text statsTitle;

    private TopGameData[] topData;
    
    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        getTopStats();
        setTopStatsOf(1);
    }

    private void getTopStats() {
        topData = SaveLoad.loadAllTopGameData();        
    }

    public void setTopStatsOf(int topPosition) {
        statsTitle.text = "Stats: Top " + topPosition.ToString();

        stats.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Total calories: " + topData[topPosition-1].calories.ToString();
        stats.transform.GetChild(3).gameObject.GetComponent<Text>().text = "Total hearts picked: " + topData[topPosition-1].heartsPicked.ToString();
        stats.transform.GetChild(4).gameObject.GetComponent<Text>().text = "Total energies picked: " + topData[topPosition-1].energiesPicked.ToString();
    }

    public void close() {
         this.gameObject.SetActive(false);
    }
}
