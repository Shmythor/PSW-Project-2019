using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWin : AScreen
{
    private void OnEnable() {
        MusicController.instance.playSoundEffect(SoundsEnum.soundEffect.fireworks_launch);
        MusicController.instance.playSoundEffect(SoundsEnum.soundEffect.fireworks_explosion);
    }

    public void nextLevel()
    {       

        LevelController.instance.nextLevel();
    }

 

}
