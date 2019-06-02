using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack : IComponent
{
    IEnumerator cooldownForNextAttack();
    void setDamage(float damage);
    void setCooldown(float cooldown);
    void attack();
}
