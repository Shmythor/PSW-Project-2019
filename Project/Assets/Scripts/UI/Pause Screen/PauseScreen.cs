using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : AScreen
{
    public void resume()
    {
        UIController.instance.restartTimer();
        UIController.instance.resumeGame();
    }

    public void saveGame() {
        UIController.instance.saveGame();
    }

}
