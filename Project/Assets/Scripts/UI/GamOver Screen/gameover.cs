using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : AScreen
{
    private void OnEnable()
    {        
        UIController.instance.stopTimer();        
    }

    public void restart()
    {       
        LevelController.instance.restartLevel();
    }

}
