using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : AStateEnemy
{
    public Stop(Rigidbody2D rb2d, float defSpeed) : base(rb2d, defSpeed)
    {
        actualSpeed = 0f;
    }

}
