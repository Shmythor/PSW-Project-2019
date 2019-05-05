using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meele_attack_range : MonoBehaviour
{

    private float damage = 50f;
    private float timeForNextAttack = 2f;
    private bool canAttack = true;
    private IEnumerator timer;


    public void setDamage(float damage) { this.damage = damage; }



    private void Start()
    {
        timer = timerForNextAttack();
    }


    // Deal damage to the player 
    // Activating cooldown for the next attack
    private void OnTriggerStay2D(Collider2D collision)
    {
        IPlayer player = collision.GetComponent<IPlayer>();
        if(player != null)
        {
            if (canAttack == false)
                return;
            player.reciveDamage(damage);
            canAttack = false;
            StartCoroutine(timer);
            timer = timerForNextAttack();
        }
    }


    IEnumerator timerForNextAttack()
    {
        while (canAttack == false)
        {
            yield return new WaitForSeconds(timeForNextAttack);
            canAttack = true;
            Debug.Log("Can attack");
        }
    }
}
