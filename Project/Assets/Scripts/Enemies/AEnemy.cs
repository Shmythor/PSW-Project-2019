using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AEnemy : MonoBehaviour, IEnemy
{

    /*          References          */
    protected Rigidbody2D rb2d;
    protected Animator animator;
    protected HealthComponent healthCom;
    protected IStateEnemy state;
    protected AnimationController animCom;
    protected MeeleAttack meeleAttack;
    protected RangeAttack rangeAttack;
    protected List<IAttack> attackComponents = new List<IAttack>();


    /*          Stats          */
    [Header("Stats")]
    [SerializeField] private float speed;
    [SerializeField] private float damageOnCollide;
    [SerializeField] private int hearts;
    [SerializeField] private float healths;
    [SerializeField] private bool canMove= true;

    public void setSpeed(float speed) { this.speed = speed; }
    public void setDamageOnCollide(float damage) { this.damageOnCollide = damage; }
    public void setHearts(int hearts) { this.hearts = hearts; }
    public Vector2 getPosition() { return transform.position; }
    public Vector2 getVelocity() { return rb2d.velocity; }


    void Awake()
    {
        initReferences();
        initVariables();
        if (meeleAttack != null) meeleAttack.setDamage(damageOnCollide);
        canMove = true;
    }

    private void initVariables()
    {
        healthCom.setHearts(hearts);
    }

    private void initReferences()
    {
        rb2d = GetComponent<Rigidbody2D>();
        healthCom = new HealthComponent(this);
        state = new Searching(rb2d, speed);
        animator = GetComponentInChildren<Animator>();
        animCom = new AnimationController(animator);
        meeleAttack = GetComponentInChildren<MeeleAttack>();
        rangeAttack = GetComponentInChildren<RangeAttack>();
        if(meeleAttack != null) attackComponents.Add(meeleAttack);
        if(rangeAttack != null) attackComponents.Add(rangeAttack);
    }

    void FixedUpdate()
    {
        if (canMove == false) { return;}
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



    public void stopEnemy()
    {
        canMove = false;
        foreach (IAttack attack in attackComponents)
            attack.disable();
    }

    public void resumeEnemy()
    {
        canMove = true;
        foreach (IAttack attack in attackComponents)
            attack.enable();
    }
}

public enum states
{
    searching,
    chasing,
    meeleCombat
}