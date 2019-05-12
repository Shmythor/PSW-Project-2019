using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause_screen : MonoBehaviour
{

    public void resume()
    {
        GameController.instance.resumeGame();
    }

    public void exit()
    {
        LevelController.instance.changeLevel(0);
    }

}
