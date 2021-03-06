﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : AComponent
{
    /*          References          */
    private ICharacter character;
    private IPlayer player;

    /*          Healths          */
    private int hearts = 3;
    private float damage = 0;

    /*          Stats          */
    private float timeInvincible = 1f;          //TODO Invincible
    private bool invincible = false;

    
    /*          Sounds          */
    private List<SoundsEnum.soundEffect> sounds = new List<SoundsEnum.soundEffect>();




    public void setPlayer(IPlayer player) { this.player = player; }
    public void setHearts(int hearts) { this.hearts = hearts; }
    public void setDamage(float damage) { this.damage = damage; }
    public int getHearts() { return hearts; }

    /*          Default constructor          */
    public HealthComponent(ICharacter character) {
        this.character = character;

    }

    /*          Constructor with parametrs          */
    public HealthComponent(ICharacter character, int hearts, float damage, float timeInvincible) {
        this.character = character;
        this.damage = damage;
        this.hearts = hearts;
        this.timeInvincible = timeInvincible;
    }
    
    /*
        Can not recive damage if the character is already invicible
        If damage is more than health the character has, it takes one heart and increases the health by 100
        Starts invicible time
        Checks if the character is dead
    */
    public void reciveDamage(float damageTaken) {
        if (invincible) return;
        damage += damageTaken;
        addPlayerDamageSound();

        if (damage >= 100) {
            hearts--;
            sounds.Add(SoundsEnum.soundEffect.ui_heartLoose);

            if (isDead()) {
                sounds.Add(SoundsEnum.soundEffect.greedy_death);
                character.die();
            }

            this.damage = 0;
        }
        updatePlayerInfo(); // Only if it's the player
    }

    private void addPlayerDamageSound()
    {
        if (damage > 0 && damage < 40) sounds.Add(SoundsEnum.soundEffect.greedy_hurt1);
        if (damage > 41 && damage < 70) sounds.Add(SoundsEnum.soundEffect.greedy_hurt2);
        if (damage > 71) sounds.Add(SoundsEnum.soundEffect.greedy_hurt3);
    }

    public void restoreHealth()
    {
        this.hearts = 3;
        this.damage = 0;
        updatePlayerInfo();
    }


    public void restoreDamageTaken()
    {
        damage = 0;
        updatePlayerInfo();
    }

    /* Send player's status(amount of hearts and damage taken) to GameController */
    /* ONLY FOR PLAYER  */
    private void updatePlayerInfo() {
        if (player == null) return;

        player.setHearts(hearts);
        if (isDead()) return;
        /*      array from sounds         */
        SoundsEnum.soundEffect[] arraySounds = sounds.ToArray();
        GameController.instance.updatePlayerHealth(hearts, damage, arraySounds);
        sounds.Clear();
    }

    public void instantKill()
    {
        character.die();
        updatePlayerInfo();

    }
    
    private bool isDead()
    {
        return damage >= 100 && hearts <= 0 ? true : false;
    }



    
}
