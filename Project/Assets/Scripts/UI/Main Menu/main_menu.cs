using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class main_menu : MonoBehaviour
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
            LevelController.instance.nextLevel();        
    }
}
