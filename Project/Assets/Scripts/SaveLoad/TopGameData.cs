using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable] 
public class TopGameData {
    public int heartsPicked, energiesPicked, calories;
    public float time;


    public TopGameData(int heartsPicked, int energiesPicked, int calories, float time)
    {        
        this.heartsPicked = heartsPicked;
        this.calories = calories;
        this.energiesPicked = energiesPicked;
        this.time = time;
    }


}


