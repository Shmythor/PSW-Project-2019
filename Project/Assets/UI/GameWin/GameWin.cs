using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWin : AScreen
{
    
    public void nextLevel()
    {
        LevelController.instance.nextLevel();
    }

 

}
