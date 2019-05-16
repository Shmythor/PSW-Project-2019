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
    protected Meele_attack_range meeleAttack;
    protected RangeAttack rangeAttack;
    protected List<IAttack> attackComponents = new List<IAttack>();


    // Stats
    [Header("Stats")]
    [SerializeField] private float speed;
    [SerializeField] private float damageOnCollide;
    [SerializeField] private int hearts;
    [SerializeField] private float healths;
    [SerializeField] private bool canMove= true;



    void Awake()
    {
        initReferences();
        if (meeleAttack != null) meeleAttack.setDamage(damageOnCollide);
        canMove = true;
    }

    private void initReferences()
    {
        rb2d = GetComponent<Rigidbody2D>();
        healthCom = new Health_Component(this);
        state = new Searching(rb2d, speed);
        animator = GetComponentInChildren<Animator>();
        animCom = new animation_controller(animator);
        meeleAttack = GetComponentInChildren<Meele_attack_range>();
        rangeAttack = GetComponentInChildren<RangeAttack>();
        attackComponents.Add(meeleAttack);
        attackComponents.Add(rangeAttack);
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