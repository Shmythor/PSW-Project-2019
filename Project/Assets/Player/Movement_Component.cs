using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Component
{
    //          References
    private Rigidbody2D rb2d;

    //          General
    private bool enabled;


    //          Stats
    private float speed;

    public Movement_Component(Player player, float speed)
    {
        rb2d = player.GetComponent<Rigidbody2D>();
        this.speed = speed;
    }



    public void movement(float horInput, float verInput)
    {
        if (enabled == false)
            return;
        //horInput = verInput == 0 ? horInput : 0;
        rb2d.velocity = new Vector2(horInput, verInput) * speed;
    }



    public void enable() { enabled = true; }
    public void disable() { enabled = false; }
}
