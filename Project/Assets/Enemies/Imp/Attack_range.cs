using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_range : MonoBehaviour
{
    private Imp imp;

    private void Start()
    {
        imp = GetComponentInParent<Imp>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICharacter character = collision.GetComponent<ICharacter>();
        if(character != null)
        {
            Vector2 direction = collision.transform.position;
            imp.fireProjectile(direction);
        }
    }
}
