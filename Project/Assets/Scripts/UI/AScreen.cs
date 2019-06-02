using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AScreen : MonoBehaviour
{

    [SerializeField] private Text caloriesText;

    void OnEnable()
    {  
        callOnEnable();
    }

    public virtual void callOnEnable() {
        GameController.instance.stopGame(); 
        UIController.instance.stopTimer();
        MusicController.instance.pauseMainSong();
        MusicController.instance.playSoundTrack(SoundsEnum.soundTrack.menu_pause);
    }

    public void setCaloriesText(int calories){ 
        caloriesText.text = calories.ToString(); 
    }

    /* void OnDisable() */
    public void exit()
    {        
        GameController.instance.setCaloriesToZero();        
        UIController.instance.setUIContainer(false);  

        LevelController.instance.loadMainMenu();
        this.gameObject.SetActive(false);
    }

    
}
