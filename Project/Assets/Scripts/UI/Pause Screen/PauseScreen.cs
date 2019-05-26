using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : AScreen
{
    private void OnEnable()
    {        
        
        UIController.instance.stopTimer();
        MusicController.instance.playSoundTrack(SoundsEnum.soundTrack.menu_pause);
    }

    public void resume()
    {
        UIController.instance.restartTimer();
        GameController.instance.resumeGame();
    }

    public void saveGame() {
        GameController.instance.saveGame();
    }

}
