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

    private List<SoundsEnum.soundEffect> sounds = new List<SoundsEnum.soundEffect>();


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

        if(damage > 0 && damage < 40) sounds.Add(SoundsEnum.soundEffect.greedy_hurt1);
        if(damage > 41 && damage < 70) sounds.Add(SoundsEnum.soundEffect.greedy_hurt2);
        if(damage > 71 && damage < 100) sounds.Add(SoundsEnum.soundEffect.greedy_hurt3);

        if (damage >= 100)
        {
            hearts--;
            sounds.Add(SoundsEnum.soundEffect.ui_heartLoose);
            if (isDead()) { 
                sounds.Add(SoundsEnum.soundEffect.greedy_death);
                character.die();
            }
                              
            this.damage = 0;
        }
        updatePlayerInfo(); // Only if it's the player


        //invincible = true;
        // TODO invicible
    }


    public void restoreDamageTaken()
    {
        damage = 0;
        //sounds.Add(SoundsEnum.soundEffect.ui_damageRestored);
        updatePlayerInfo();
    }

    // Send player's status(amount of hearts and damage taken) to GameController
    // ONLY FOR PLAYER
    private void updatePlayerInfo()
    {
        //array from sounds        
        SoundsEnum.soundEffect[] arraySounds = sounds.ToArray();
        if (player == null)
            return;
        GameController.instance.updatePlayerHealth(hearts, damage, arraySounds);
        sounds.Clear();
    }




    private bool isDead()
    {
        return damage >= 100 && hearts <= 0 ? true : false;
    }



    public void setPlayer(IPlayer player) { this.player = player; }
}
