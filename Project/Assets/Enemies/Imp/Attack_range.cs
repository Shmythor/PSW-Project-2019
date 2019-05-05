using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_range : MonoBehaviour
{
    private Imp imp;

    private void Awake()
    {
        imp = GetComponentInParent<Imp>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //TODO make imp attacks other enemies
        IPlayer player = collision.GetComponent<IPlayer>();
        if(player != null)
        {
            Vector2 direction = player.getPosition();
            imp.fireProjectile(direction);
        }
    }
}
