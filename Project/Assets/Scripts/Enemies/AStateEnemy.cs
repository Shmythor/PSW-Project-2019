using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStateEnemy : IStateEnemy
{
    /*          References          */
    protected Rigidbody2D rb2d;


    /*          General          */
    protected Vector2 movePosition;
    protected ICharacter character;

    /*          Stats          */
    protected float defSpeed;
    protected float actualSpeed;

    public Vector2 getMovementInputs() { return rb2d.velocity; }


    public AStateEnemy(Rigidbody2D rb2d, float defSpeed)
    {
        this.rb2d = rb2d;
        this.defSpeed = defSpeed;
    }



    public virtual void movement() {
        Vector2 direction = new Vector2(movePosition.x - rb2d.GetComponent<Transform>().position.x,
                                        movePosition.y - rb2d.GetComponent<Transform>().position.y);
        direction.Normalize();
        rb2d.velocity = direction * actualSpeed;
    }


    





}
