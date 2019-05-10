using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameover : MonoBehaviour
{


    

    public void restart()
    {
        LevelController.instance.changeLevel(0);
    }

    public void exit()
    {

        LevelController.instance.changeLevel(2);
    }
}
