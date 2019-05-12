using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause_screen : AScreen
{

    public void resume()
    {
        GameController.instance.resumeGame();
    }



}
