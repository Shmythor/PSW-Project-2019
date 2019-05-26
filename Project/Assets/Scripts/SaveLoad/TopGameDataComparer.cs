using System.Collections;
using System.Collections.Generic;

public class TopGameDataComparer : IComparer<TopGameData>
{
    public int Compare(TopGameData a, TopGameData b)
    {
        if(a == null && b != null) {return -1;}
        if(a != null && b == null) {return 1;}
        if(a == null && b == null) {return 0;}


//        Debug.Log("VOY A VOMPROBAR: " + a.calories.ToString());
        if (a.calories == b.calories) {
            if(a.time < b.time) return 1;
            if(a.time > b.time) return -1;
            return 0;
        }
           
        if (a.calories > b.calories) {            
            return 1;
        }

        return -1;
    }
}