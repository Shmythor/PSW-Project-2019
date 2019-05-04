using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float damage;

    public void applyDamageToCharacter(ICharacter character)
    {
        character.reciveDamage(damage);
        Destroy(gameObject);
    }

    private void Update()
    {
        
    }


    public void setDamage(float damage) { this.damage = damage; }
}
