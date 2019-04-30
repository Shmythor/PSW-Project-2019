using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer, ICharacter
{
    //          References
    [Header("References")]
    [SerializeField] private GameObject initialMap;
    [SerializeField] private GameObject Dust;
    private Rigidbody2D rb2d;
    private Animator animator;
    private Movement_Component mvCom;
    private Health_Component healthCom;
    private animation_controller animCom;
    

    //          General
    [Header("General")]
    [SerializeField] private bool movement_enable;
    private float deltaTime;


    //          Inputs
    [Header("Inputs")]
    [SerializeField] private float verInput;
    [SerializeField] private float horInput;
    [SerializeField] private float moveMagnitude;
    
    
    //          Stats
    [Header("Stats")]
    [SerializeField] private float speed = 4f;

    


    

    void Start () {
        animator = GetComponentInChildren<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        mvCom = new Movement_Component(rb2d, speed);
        mvCom.setDustParticles(Dust);
        healthCom = new Health_Component(this);
        animCom = new animation_controller(animator);
        
        Camera.main.GetComponent<MainCamera>().SetBound(initialMap);
    }


    public void tick(float d)
    {
        deltaTime = d;
        mvCom.movement(horInput, verInput, moveMagnitude);
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
        // TODO player dies
    }



    public void reciveDamage(float damage)
    {
        healthCom.reciveDamage(damage);
    }

    
    public Vector2 getPosition() { return transform.position;  }

}
