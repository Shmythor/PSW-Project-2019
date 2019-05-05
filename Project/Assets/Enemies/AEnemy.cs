﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AEnemy : MonoBehaviour, ICharacter
{

    // References 
    private Rigidbody2D rb2d;
    private Animator animator;
    private Health_Component healthCom;
    private IStateEnemy state;
    private animation_controller animCom;

    // Stats
    [Header("Stats")]
    [SerializeField] private float speed;
    [SerializeField] private float damageOnCollide;
    [SerializeField] private int hearts;
    [SerializeField] private float healths;



    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        healthCom = new Health_Component(this);
        state = new Searching(rb2d,speed);
        animator = GetComponentInChildren<Animator>();
        animCom = new animation_controller(animator);
    }

    
    void FixedUpdate()
    {
        state.movement();
        Vector2 movementInputs = state.getMovementInputs();
        animCom.updateAnimator(movementInputs.y, movementInputs.x);
    }



    public void die()
    {
        // TODO enemy dies
    }


    public void reciveDamage(float damage) { healthCom.reciveDamage(damage); }


    // We use this function in the ChaseRadius script
    // Depending if the player is in the range it changes the current state
    public void changeState(IPlayer player, bool chasing)
    {
        if (chasing)
            state = new Chasing(rb2d, speed, player);
        else
            state = new Searching(rb2d, speed);
    }
}