using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //          References
    private Rigidbody2D rb2d;
    private Movement_Component mvCom;
    private Health_Component healthCom;


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
        rb2d = GetComponent<Rigidbody2D>();
        mvCom = new Movement_Component(this, speed);
        healthCom = new Health_Component(this);
    }




    public void tick(float d)
    {
        deltaTime = d;
        mvCom.movement(horInput, verInput);
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
