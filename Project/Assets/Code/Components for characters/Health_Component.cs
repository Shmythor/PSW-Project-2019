using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Component
{
    //          References
    private ICharacter character;
    private IPlayer player;

    //          Health Points
    private int hearts = 3;
    private float damage = 0;

    //          Stats
    private float timeInvincible = 1f; //TODO Invincible
    private bool invincible = false;


    //              Default constructor
    public Health_Component(ICharacter character)
    {
        this.character = character;
    }


    //              Constructor with parametrs
    public Health_Component(ICharacter character, int hearts, float damage, float timeInivcibly)
    {
        this.character = character;
        this.damage = damage;
        this.hearts = hearts;
        this.timeInvincible = timeInivcibly;
    }




    /// <summary>
    ///  Can not recive damage if the character is already invicible
    ///  If damage is more than health the character has, it takes one heart and increases the health by 100
    ///  Starts invicible time
    ///  Checks if the character is dead
    /// </summary>
    /// <param name="damageTaken"></param>
    public void reciveDamage(float damageTaken)
    {
        if (invincible)
            return;
        damage += damageTaken;
        if (damage >= 100)
        {
            hearts--;
            this.damage = 0;
        }
        updatePlayerInfo(); // Only if it's the player


        //invincible = true;
        // TODO invicible


        if (isDead())
            character.die();
    }



    public void restoreDamageTaken()
    {
        damage = 0;
        updatePlayerInfo();
    }


    // Send player's status(amount of hearts and damage taken) to GameController
    private void updatePlayerInfo()
    {
        if (player == null)
            return;
        GameController.instance.updatePlayerHealth(hearts, damage);
    }

    private bool isDead()
    {
        return damage >= 100 && hearts <= 0 ? true : false;
    }



    public void setPlayer(IPlayer player) { this.player = player; }
    public int getHearts() { return hearts; }
    public float getDamage() { return damage; }
}
