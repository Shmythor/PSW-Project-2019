using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameover : AScreen
{


    

    public void restart()
    {
        LevelController.instance.restartLevel();
    }


}
