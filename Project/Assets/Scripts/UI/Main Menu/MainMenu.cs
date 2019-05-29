using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject loadScreen, accountScreen;

    void Start() {
        //console.log("Cargan cosas");
        loadScreen.SetActive(false);
        MusicController.instance.playSoundTrack(SoundsEnum.soundTrack.menu_mainMenu);
    }

    void Update() {
       if (Input.GetKeyDown("space")) {
            startGame();
        }

        if (Input.GetKey("escape")) {            
            Application.Quit();
        }
    }

    public void startGame()
    {   
        LevelController.instance.startGame();        
    }

    public void loadGame()
    {
        loadScreen.SetActive(true);
    }

    public void accountLogin()
    {
        accountScreen.SetActive(true);
    }
}
