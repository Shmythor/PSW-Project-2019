﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Component
{
    //          References
    private Rigidbody2D rb2d;
    private GameObject dustParticles;

    //          General
    private bool enabled = true;
    private float magnitude;

    //          Stats
    private float speed;

    public Movement_Component(Rigidbody2D rb2d, float speed)
    {
        this.rb2d = rb2d;
        this.speed = speed;
    }



    public void movement(float horInput, float verInput, float magnitude)
    {
        this.magnitude = magnitude;
        if (enabled == false)
            return;
        //horInput = verInput == 0 ? horInput : 0;
        // The character is moving slower when the movement is diagonal
        float actualSpeed = horInput != 0 && verInput != 0 ? speed * 0.75f : speed;
        rb2d.velocity = new Vector2(horInput, verInput) * actualSpeed;
        initDust();
    }

    private void initDust()
    {
        if (dustParticles == null) return;
        if (magnitude == 0) dustParticles.SetActive(false);
        else dustParticles.SetActive(true);
    }



    public void enable() { enabled = true; }
    public void disable() { enabled = false; }

    public void setDustParticles(GameObject dust){
        dustParticles = dust;
    }
}
