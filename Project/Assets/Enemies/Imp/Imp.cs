using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imp : AEnemy, Iimp
{

    [SerializeField] private float projectileDamage;
    [SerializeField] private GameObject projectilePrefab;

    public void fireProjectile(Vector2 direction)
    {
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
        Debug.Log(projectile.ToString());
        projectile.setDamage(projectileDamage);
    }
}
