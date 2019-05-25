using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AAttack : MonoBehaviour, IAttack, IComponent
{
   protected float damage;
    protected float cooldown = 1f;
    protected bool canAttack = true;
    protected bool attacking;
    protected bool active = true;
    protected IEnumerator timer;
    protected AEnemy enemy;
    protected IPlayer player;

    
    public void setDamage(float damage) { this.damage = damage; }
    public void setCooldown(float cooldown) { this.cooldown = cooldown; }

    private void Awake()
    {
        enemy = GetComponentInParent<AEnemy>();
        timer = cooldownForNextAttack();
    }


    private void FixedUpdate()
    {
        if (active == false)
            return;
        if (attacking)
            attack();
    }



    protected void startCooldown()
    {
        canAttack = false;
        StartCoroutine(timer);
        timer = cooldownForNextAttack();
    }

    public void enable() { active = true; }
    public void disable() { active = false; }


    public IEnumerator cooldownForNextAttack()
    {
        while (canAttack == false)
        {
            yield return new WaitForSeconds(cooldown);
            canAttack = true;
        }
    }

    virtual public void attack(){ } // MUST BEEN OVERRIDED
}
