using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text texto;

    void Start() {
        //console.log("Cargan cosas");
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
        LevelController.instance.loadGame();   
    }

    public void accountLogin()
    {

    }
}
