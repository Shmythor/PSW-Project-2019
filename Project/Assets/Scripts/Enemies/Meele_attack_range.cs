using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meele_attack_range : MonoBehaviour
{

    private float damage = 50f;
    private float timeForNextAttack = 2f;
    private bool canAttack = true;
    private IEnumerator timer;
    private AEnemy enemy;
    private bool Attacking;
    private IPlayer player;
    private bool active = true;


    public void setDamage(float damage) { this.damage = damage; }



    private void Awake()
    {
        enemy = GetComponentInParent<AEnemy>();
        timer = timerForNextAttack();
    }

    private void FixedUpdate()
    {
        if (active == false)
            return;
        if (Attacking)
            attack();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPlayer player = collision.GetComponent<IPlayer>();
        if (player != null)
        {
            this.player = player;
            enemy.changeState(player, states.meeleCombat);
            Attacking = true;
        }
    }


   
    

    private void OnTriggerExit2D(Collider2D collision)
    {
        IPlayer player = collision.GetComponent<IPlayer>();
        if (player != null)
        {
            Attacking = false;
            enemy.changeState(player, states.chasing);
        }
    }


    


    private void attack()
    {
        if (canAttack == false)
            return;
        player.reciveDamage(damage);
        canAttack = false;
        StartCoroutine(timer);
        timer = timerForNextAttack();
    }



    public void enable()
    {
        active = true;
    }

    public void disable()
    {
        active = false;
    }

    IEnumerator timerForNextAttack()
    {
        while (canAttack == false)
        {
            yield return new WaitForSeconds(timeForNextAttack);
            canAttack = true;
        }
    }
}
