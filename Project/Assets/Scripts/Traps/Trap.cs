using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private int damage = 25;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICharacter character = collision.GetComponent<ICharacter>();
        if(character != null)
        {
            character.reciveDamage(damage);
        }
    }

}
