﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : AScreen
{

    private void OnEnable()
    {
        MusicController.instance.playSoundTrack(SoundsEnum.soundTrack.menu_pause);
    }

    public void resume()
    {
        GameController.instance.resumeGame();
    }



}