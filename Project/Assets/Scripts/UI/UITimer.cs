using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UITimer : MonoBehaviour
{
    public Text timer;
    private float timeLeft = 59;
    private bool runTimer = true;
       
    void Start() {
        resetTimer();
    }

    void Update()
    {
        if(runTimer) {
            timer.text = timeLeft.ToString("f0");

            if(timeLeft <= 16) {
                string par = (timeLeft % 2).ToString("f0");
                if(par == "0") {                
                    timer.fontStyle = FontStyle.Bold;
                    timer.color = Color.red;    
                } else if(par == "1") {
                    timer.fontStyle = FontStyle.Normal;
                    timer.color = Color.white;    
                }                    
            } 

            if(timeLeft <= 1) {
                GameController.instance.GameOver();
                timeLeft = 0;
            } else {
                timeLeft -= Time.deltaTime;
            }     
        }   

    }

    public void resetTimer() {
        timeLeft = 59;
        timer.fontStyle = FontStyle.Normal;
        timer.color = Color.white; 
    }

    public void stopTimer() {
        runTimer = false;
    }
    
    public void restartTimer() {
        runTimer = true;
    }
}
