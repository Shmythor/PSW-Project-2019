using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : AComponent
{

    private Animator anim;

    
    public AnimationController(Animator anim)
    {
        this.anim = anim;
    }


    public void updateAnimator(float dirUp, float dirRight)
    {
        if (enabled == false)
        {
            dirUp = 0f;
            dirRight = 0f;
        }
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
