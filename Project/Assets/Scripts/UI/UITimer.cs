using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UITimer : MonoBehaviour
{
    float timeLeft = 19;
    public Text timer;
    
    // Start is called before the first frame update
    void Start()
    {
        timer.fontStyle = FontStyle.Normal;
        timer.color = Color.white;   
    }

    // Update is called once per frame
    void Update()
    {
        
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
