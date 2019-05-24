using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleAttack : AAttack
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
        attacking = true;
        enemy.changeState(player, states.meeleCombat);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IPlayer player = collision.GetComponent<IPlayer>();
        if (player == null)
            return;
        attacking = false;
        enemy.changeState(player, states.searching);
    }


}
