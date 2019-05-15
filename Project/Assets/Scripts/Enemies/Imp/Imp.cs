using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imp : AEnemy, Iimp
{
    [Header("Projectile")]
    [SerializeField] private float projectileDamage;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileCooldown;

    private Attack_range rangeAttack;


    private void Start()
    {
        rangeAttack = GetComponentInChildren<Attack_range>();
        rangeAttack.setCooldown(projectileCooldown);
        meele.disable();
    }

    public void fireProjectile(Vector2 direction)
    {
        Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.setDirection(direction);
        projectile.setDamage(projectileDamage);
        projectile.setSpeed(projectileSpeed);
    }
}
