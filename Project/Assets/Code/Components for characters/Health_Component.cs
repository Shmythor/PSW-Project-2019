using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Component
{
    //          References
    private ICharacter character;


    //          Health Points
    private int hearts = 3;
    private float health = 100;

    //          Stats
    private float timeInvincible = 1f; //TODO Invincible
    private bool invincible = false;

    //              Default constructor
    public Health_Component(ICharacter character)
    {
        this.character = character;
    }


    //              Constructor with parametrs
    public Health_Component(ICharacter character, int hearts, float health, float timeInivcibly)
    {
        this.character = character;
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
        //invincible = true;
        // TODO invicible
        if (isDead())
            character.die();
    }



    private bool isDead()
    {
        return health <= 0 && hearts <= 0 ? true : false;
    }


}
