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
        anim.SetFloat("direction_up",dirUp);
        anim.SetFloat("direction_right",dirRight);

    }
}
