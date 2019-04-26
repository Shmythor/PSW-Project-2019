using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{
    //          References
    private Rigidbody2D rb2d;
    private Animator animator;
    private Movement_Component mvCom;
    private Health_Component healthCom;
    private animation_controller animCom;

    //          General
    [SerializeField] private bool movement_enable;
    private float deltaTime;


    //          Inputs
    [Header("Inputs")]
    [SerializeField] private float verInput;
    [SerializeField] private float horInput;
    [SerializeField] private float moveMagnitude;
    
    
    //          Stats
    [Header("Stats")]
    [SerializeField] private float speed = 5f;


    public void Init()
    {
        animator = GetComponentInChildren<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        mvCom = new Movement_Component(rb2d, speed);
        healthCom = new Health_Component(this);
        animCom = new animation_controller(animator);
    }




    public void tick(float d)
    {
        deltaTime = d;
        mvCom.movement(horInput, verInput);
        updateAnimatorParamaters();
    }



    private void updateAnimatorParamaters()
    {
        float verDir = Mathf.Clamp(verInput, -1, 1);
        float horDIr = Mathf.Clamp(horInput, -1, 1);
        animCom.updateAnimator(verDir, horDIr);
    }



    public void updateInputs(float ver, float hor, float magnitude)
    {
        verInput = ver;
        horInput = hor;
        moveMagnitude = magnitude;
    }


    public void die()
    {

    }



    

    
}
