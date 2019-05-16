using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack 
{
    IEnumerator cooldownForNextAttack();
    void setDamage(float damage);
    void setCooldown(float cooldown);
    void enable();
    void disable();
    void attack();
}
