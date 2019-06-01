﻿using System.Collections;
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
        UIController.instance.resumeGame();
    }

    public void saveGame() {
        UIController.instance.saveGame();
    }

}
