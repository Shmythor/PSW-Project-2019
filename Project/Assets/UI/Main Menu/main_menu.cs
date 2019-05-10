using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main_menu : MonoBehaviour
{
    public void startGame()
    {
        LevelController.instance.nextLevel();
    }
}
