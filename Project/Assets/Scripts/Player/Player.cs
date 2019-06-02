using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{

    [Header("References")]
    [SerializeField] private GameObject initialMap;
    [SerializeField] private GameObject Dust;
    private Rigidbody2D rb2d;
    private Animator animator;
    private MovementComponent mvCom;
    private HealthComponent healthCom;
    private AnimationController animCom;
    private LinkedList<IComponent> components;

    [Header("General")]
    [SerializeField] private bool movement_enable;
    private float deltaTime;


    [Header("Inputs")]
    [SerializeField] private float verInput;
    [SerializeField] private float horInput;
    [SerializeField] private float moveMagnitude;
    
    
    [Header("Stats")]
    [SerializeField] private float speed = 4f;
    [SerializeField] private int hearts = 3;


    public Vector2 getPosition() { return transform.position; }
    public void setHearts(int hearts) { this.hearts = hearts; }
    public int getHearts() { return this.hearts; }


    void Awake ()
    {
        initReferences();        
    }

    private void initReferences()
    {
        components = new LinkedList<IComponent>();
        animator = GetComponentInChildren<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        mvCom = new MovementComponent(rb2d, speed);
        mvCom.enable();
        mvCom.setDustParticles(Dust);
        healthCom = new HealthComponent(this);
        healthCom.setPlayer(this);
        animCom = new AnimationController(animator);
        components.AddFirst(mvCom);
        components.AddFirst(healthCom);
        components.AddFirst(animCom);
    }

    private void Start()
    {
        initialMap = MapController.instance.getActiveMap();

        Camera.main.GetComponent<MainCamera>().setBound(initialMap);
        healthCom.setHearts(hearts);
        healthCom.reciveDamage(0); /* For updating the UI */
    }

    /*          Syncronyse with inputManager in place of Update         */
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
        //Debug.Log
        UIController.instance.GameOver();
        
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
        if (damage >= hearts * 100)
            healthCom.instantKill();
        healthCom.reciveDamage(damage);
    }


    public void disableInputs()
    {
        foreach (IComponent component in components)
            component.disable();
    }

    public void enableInputs()
    {
        foreach (IComponent component in components)
            component.enable();
    }

    public void updateHealthFromLoadGameData(int hearts, float damage) {
        healthCom.setHearts(hearts);
        healthCom.setDamage(damage);
    }

    
    public void increaseSpeedForXSeconds(float speedRate, int seconds) {
        StartCoroutine(logicOfIncreaseSpeed(speedRate, seconds));
    } 

    private IEnumerator logicOfIncreaseSpeed(float speedRate, int seconds) {
        float oldSpeed = this.speed;
        mvCom.setSpeed(oldSpeed + (oldSpeed * speedRate));
        yield return new WaitForSeconds(seconds);
        mvCom.setSpeed(oldSpeed);
    }


    
}
