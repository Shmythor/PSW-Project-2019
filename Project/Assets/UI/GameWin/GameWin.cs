using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWin : MonoBehaviour
{
    
    public void resume()
    {
        LevelController.instance.nextLevel();
    }

    public void exit()
    {
        LevelController.instance.loadMainMenu();
    }

}
