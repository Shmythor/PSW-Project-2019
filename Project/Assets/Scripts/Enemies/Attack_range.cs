using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_range : MonoBehaviour
{
    private Imp imp;
    private bool Attacking;
    private bool canAttack = true;
    private ICharacter characterToShoot;
    private float cooldown = 1f;
    private IEnumerator timer;


    public void setCooldown(float cooldown) { this.cooldown = cooldown; }


    private void Start()
    {
        imp = GetComponentInParent<Imp>();
        timer = timerForNextAttack();
    }


    private void FixedUpdate()
    {
        if (Attacking)
            shoot();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPlayer player = collision.GetComponent<IPlayer>();
        if (player != null)
        {
            Attacking = true;
            characterToShoot = player;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        IPlayer player = collision.GetComponent<IPlayer>();
        if(player != null)
            Attacking = false;
    }


    private void shoot()
    {
        //TODO make imp attacks other enemies
            if (canAttack == false)
                return;
            Vector2 direction = characterToShoot.getPosition();
            imp.fireProjectile(direction);
            canAttack = false;
            StartCoroutine(timer);
            timer = timerForNextAttack();
        
    }

    IEnumerator timerForNextAttack()
    {
        while (canAttack == false)
        {
            yield return new WaitForSeconds(cooldown);
            canAttack = true;
        }
    }
}
