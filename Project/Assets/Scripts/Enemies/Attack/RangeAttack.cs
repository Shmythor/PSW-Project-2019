using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : AAttack
{
    private Imp imp;


    private void Start()
    {
        imp = GetComponentInParent<Imp>();

    }



    public override void attack()
    {
        //TODO make imp attacks other enemies

        if (canAttack == false)
            return;
        Vector2 direction = player.getPosition();
        imp.fireProjectile(direction);
        startCooldown();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPlayer player = collision.GetComponent<IPlayer>();
        if (player == null)
            return;
        this.player = player;
        Attacking = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IPlayer player = collision.GetComponent<IPlayer>();
        if (player == null)
            return;
        Attacking = false;
    }


}
