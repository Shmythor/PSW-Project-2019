using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Component
{
    //          References
    private IPlayer player;


    //          Health Points
    private int hearts = 3;
    private float health = 100;

    //          Stats
    private float timeInvincible = 1f;
    private bool invincible = false;

    //              Default constructor
    public Health_Component(IPlayer player)
    {
        this.player = player;
    }


    //              Constructor with parametrs
    public Health_Component(IPlayer player, int hearts, float health, float timeInivcibly)
    {
        this.player = player;
        this.health = health;
        this.hearts = hearts;
        this.timeInvincible = timeInivcibly;
    }


    /// <summary>
    ///  Can not recive damage if the character is already invicible
    ///  If damage is more than health the character has, it takes one heart and increases the health by 100
    ///  Starts invicible time
    ///  Checks if the character is dead
    /// </summary>
    /// <param name="damage"></param>
    public void reciveDamage(float damage)
    {
        if (invincible)
            return;
        if (damage > health)
        {
            hearts--;
            health += 100;

        }
        health -= damage;
        invincible = true;
        if (isDead())
            player.die();
    }



    private bool isDead()
    {
        return health <= 0 && hearts <= 0 ? true : false;
    }


}
