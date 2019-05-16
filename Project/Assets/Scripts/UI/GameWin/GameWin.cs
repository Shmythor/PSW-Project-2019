using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWin : AScreen
{
    [SerializeField]private Animator animator;

    private void OnEnable() {
        MusicController.instance.playSoundEffect(SoundsEnum.soundEffect.fireworks_launch);
        MusicController.instance.playSoundEffect(SoundsEnum.soundEffect.fireworks_explosion);
        animator.SetBool("lastLevel?", GameController.instance.isIsLastLevel());
    }

    public void nextLevel()
    {       

        LevelController.instance.nextLevel();
    }

 

}
