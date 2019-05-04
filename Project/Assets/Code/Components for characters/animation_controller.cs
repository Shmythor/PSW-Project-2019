using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation_controller
{

    Animator anim;

    
    public animation_controller(Animator anim)
    {
        this.anim = anim;
    }


    public void updateAnimator(float dirUp, float dirRight)
    {

        anim.SetFloat("direction_up", floatToInt(dirUp));
        anim.SetFloat("direction_right", floatToInt(dirRight));
    }



    private int floatToInt(float value)
    {
        if (value == 0)
            return 0;
        return value > 0 ? 1 : -1;
    }
}
