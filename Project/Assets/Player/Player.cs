using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer
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
    private LinkedList<IComponent> components;

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

    


    

    void Awake () {
        components = new LinkedList<IComponent>();
        animator = GetComponentInChildren<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        mvCom = new Movement_Component(rb2d, speed);
        mvCom.enable();
        mvCom.setDustParticles(Dust);
        healthCom = new Health_Component(this);
        healthCom.setPlayer(this);
        animCom = new animation_controller(animator);
        
        
        components.AddFirst(mvCom);
        components.AddFirst(healthCom);
        components.AddFirst(animCom);
    }

    private void Start()
    {
        Camera.main.GetComponent<MainCamera>().SetBound(initialMap);
        healthCom.reciveDamage(0);
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

        GameController.instance.GameOver();
        //Destroy(gameObject);       // TODO player dies
    }


    public void restoreHealth()
    {
        healthCom.restoreHealth();
    }

    public void restoreDamageTaken()
    {
        healthCom.restoreDamageTaken();
    }

    public void reciveDamage(float damage)
    {
        healthCom.reciveDamage(damage);
    }


    public void disableInputs()
    {
        foreach (IComponent com in components)
            com.disable();
    }

    public void enableInputs()
    {
        foreach (IComponent com in components)
            com.enable();
    }




    public Vector2 getPosition() { return transform.position;  }
    public Health_Component getHealthComponent() { return healthCom; }
}
