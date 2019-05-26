using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject LoadScreen;

    void Start() {
        //console.log("Cargan cosas");
        LoadScreen.SetActive(false);
        MusicController.instance.playSoundTrack(SoundsEnum.soundTrack.menu_mainMenu);
    }

    void Update() {
       if (Input.GetKeyDown("space"))
        {
            startGame();
        }
    }

    public void startGame()
    {   
        LevelController.instance.startGame();        
    }

    public void loadGame()
    {
        LoadScreen.SetActive(true);
    }

    public void accountLogin()
    {

    }
}
