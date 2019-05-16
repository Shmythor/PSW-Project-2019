using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meele_attack_range : AAttack
{



    public override void attack()
    {
        player.reciveDamage(damage);
        startCooldown();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPlayer player = collision.GetComponent<IPlayer>();
        if (player == null)
            return;
        this.player = player;
        Attacking = true;
        enemy.changeState(player, states.chasing);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IPlayer player = collision.GetComponent<IPlayer>();
        if (player == null)
            return;
        Attacking = false;
        enemy.changeState(player, states.searching);
    }


}
