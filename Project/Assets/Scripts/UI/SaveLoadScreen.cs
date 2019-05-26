using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadScreen : MonoBehaviour
{
    public GameObject cell1, cell2, cell3;
    
    void Awake()
    {
        updateCellUI();
    }

    private void setCellData(GameObject cell, GameDataSerializable data) {
        if(data != null) {
            cell.transform.GetChild(2).GetChild(0).gameObject.GetComponent<Text>().text = "Last updated: " + data.date;
            cell.transform.GetChild(2).GetChild(1).gameObject.GetComponent<Text>().text = "Score: " + data.calories.ToString();
            cell.transform.GetChild(2).GetChild(2).gameObject.GetComponent<Text>().text = "Level: " + data.level.ToString();
            cell.transform.GetChild(2).GetChild(3).gameObject.GetComponent<Text>().text = "Hearts: " + data.hearts.ToString();
        } else {
            cell.transform.GetChild(2).GetChild(0).gameObject.GetComponent<Text>().text = "[Empty cell]";
            cell.transform.GetChild(2).GetChild(1).gameObject.GetComponent<Text>().text = "";
            cell.transform.GetChild(2).GetChild(2).gameObject.GetComponent<Text>().text = "";
            cell.transform.GetChild(2).GetChild(3).gameObject.GetComponent<Text>().text = "";
        }
    }

    public void saveGameDataAtCell(string path) {
        if(path=="cell1") GameController.instance.saveGameAt(SaveLoad.paths.path1);
        if(path=="cell2") GameController.instance.saveGameAt(SaveLoad.paths.path2);
        if(path=="cell3") GameController.instance.saveGameAt(SaveLoad.paths.path3);
        //TODO: WARNING ABOUT REPLACE SAVED DATA

        updateCellUI();
    }

      public void loadGameDataAtCell(string path) {
        if(path=="cell1") LevelController.instance.loadGame(SaveLoad.paths.path1);
        if(path=="cell2") LevelController.instance.loadGame(SaveLoad.paths.path2);
        if(path=="cell3") LevelController.instance.loadGame(SaveLoad.paths.path3);
        //TODO: WARNING ABOUT REPLACE SAVED DATA

        Debug.Log("Funciona esto");
       
    }


    private void updateCellUI() {
        GameDataSerializable[] data = SaveLoad.loadAllGameData();
        setCellData(cell1, data[0]); 
        setCellData(cell2, data[1]); 
        setCellData(cell3, data[2]);
    }

    public void close() {
        UIController.instance.restartTimer();
        GameController.instance.resumeGame();
    }

    public void closeAtMainMenu() {
       this.gameObject.SetActive(false);
    }
}
