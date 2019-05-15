using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AEnemy : MonoBehaviour, IEnemy
{

    // References 
    protected Rigidbody2D rb2d;
    protected Animator animator;
    protected Health_Component healthCom;
    protected IStateEnemy state;
    protected animation_controller animCom;
    protected Meele_attack_range meele;

    // Stats
    [Header("Stats")]
    [SerializeField] private float speed;
    [SerializeField] private float damageOnCollide;
    [SerializeField] private int hearts;
    [SerializeField] private float healths;
    [SerializeField] private bool canMove= true;



    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        healthCom = new Health_Component(this);
        state = new Searching(rb2d, speed);
        animator = GetComponentInChildren<Animator>();
        animCom = new animation_controller(animator);
        meele = GetComponentInChildren<Meele_attack_range>();
        if(meele != null) meele.setDamage(damageOnCollide);
        canMove = true;
    }


    void FixedUpdate()
    {
        if (canMove == false)
            return;
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
    public void changeState(ICharacter characterToChase, states nextState)
    {
        switch (nextState)
        {
            case states.searching:
                state = new Searching(rb2d, speed);
                break;
            case states.chasing:
                state = new Chasing(rb2d, speed, characterToChase);
                break;
            case states.meeleCombat:
                state = new MeeleCombat(rb2d, speed);
                break;
        }

    }

    public Vector2 getPosition() { return transform.position; }

    public void stopEnemyMovement()
    {
        canMove = false;
        if (meele != null)
            meele.disable();
    }

    public void resumeEnemyMovement()
    {
        canMove = true;
        if (meele != null)
            meele.enable();
    }
}

public enum states
{
    searching,
    chasing,
    meeleCombat
}