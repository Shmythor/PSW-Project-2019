using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AEnemy : MonoBehaviour, ICharacter
{

    // References 
    private Rigidbody2D rb2d;
    private Animator animator;
    private Health_Component healthCom;
    private IStateEnemy state;

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
    }

    
    void FixedUpdate()
    {
        state.movement();
    }



    public void die()
    {
        // TODO enemy dies
    }


    public void reciveDamage(float damage) { healthCom.reciveDamage(damage); }
}
