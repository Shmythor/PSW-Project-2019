using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollider : MonoBehaviour
{
    private Projectile projectile;

    private void Start()
    {
        projectile = GetComponentInParent<Projectile>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        /* The imp cannot shoot other imp or itself   */
        ICharacter character = collision.GetComponent<ICharacter>();
        Iimp imp = collision.GetComponent<Iimp>();
        if (character != null && imp == null)
        {
            projectile.applyDamageToCharacter(character);
        }
    }

    private void destroyWhenAnimationIsEnded()
    {
        projectile.destroy();
    }
}
