using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // References
    Rigidbody2D rb2d;



    private float deltaTime;
    private float verInput;
    private float horInput;
    [SerializeField] private float moveMagnitude;
    [SerializeField] private States movementStates = States.idle;
    [SerializeField] private float speed = 5f;


    public void Init()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }




    public void tick(float d)
    {
        deltaTime = d;
        updateStates();
        switch (movementStates)
        {
            case States.idle:

            break;
            case States.moving:
                movement();
            break;
        }
    }

    private void movement()
    {
        //rb2d.AddForce(new Vector2(horInput, verInput) * speed);
        rb2d.velocity = new Vector2(horInput, verInput) * speed;
    }

    private void updateStates()
    {
        movementStates = moveMagnitude > 0 ? States.moving : States.idle;
    }

    public void updateInputs(float ver, float hor, float magnitude)
    {
        verInput = ver;
        horInput = hor;
        moveMagnitude = magnitude;
    }



    enum States
    {
        idle,
        moving
    }

    

    
}
