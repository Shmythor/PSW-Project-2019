using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : AScreen
{


    

    public void restart()
    {
        LevelController.instance.restartLevel();
    }


}
