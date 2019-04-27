using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Component
{
    //          References
    private Rigidbody2D rb2d;

    //          General
    private bool enabled = true;


    //          Stats
    private float speed;

    public Movement_Component(Rigidbody2D rb2d, float speed)
    {
        this.rb2d = rb2d;
        this.speed = speed;
    }



    public void movement(float horInput, float verInput)
    {
        if (enabled == false)
            return;
        //horInput = verInput == 0 ? horInput : 0;
        // The character is moving slower when the movement is diagonal
        float actualSpeed = horInput != 0 && verInput != 0 ? speed * 0.75f : speed;
        rb2d.velocity = new Vector2(horInput, verInput) * actualSpeed;
    }



    public void enable() { enabled = true; }
    public void disable() { enabled = false; }
}
