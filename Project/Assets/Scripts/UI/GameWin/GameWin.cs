﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWin : AScreen
{
    [SerializeField] private Animator animator;

    void OnEnable() {
        this.callOnEnable();          
    }

    public override void callOnEnable() {
        base.callOnEnable();  
        MusicController.instance.playSoundEffect(SoundsEnum.soundEffect.fireworks_launch);
        MusicController.instance.playSoundEffect(SoundsEnum.soundEffect.fireworks_explosion);
        animator.SetBool("lastLevel?", GameController.instance.isIsLastLevel());           
    }

    public void nextLevel()
    {       
        if(GameController.instance.isIsLastLevel()) {
            LevelController.instance.loadMainMenu();
        } else {            
            LevelController.instance.nextLevel();      
        }              
             
    }

    

}
