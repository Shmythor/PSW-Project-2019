using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Collider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        // The imp cannot shoot other imp or itself
        ICharacter character = collision.GetComponent<ICharacter>();
        Iimp imp = collision.GetComponent<Iimp>();
        if (character != null && imp == null)
        {
            GetComponentInParent<Projectile>().applyDamageToCharacter(character);
        }
    }
}
