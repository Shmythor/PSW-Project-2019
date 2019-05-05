using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    

    // References
    private Rigidbody2D rb2d;
    private Animator animator;


    // General
    private float damage;
    private float speed = 2f;
    private Vector2 playerPosition;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        movement();
        updateAnimations();
    }


    private void movement()
    {
        Vector2 direction = new Vector2(playerPosition.x - transform.position.x, playerPosition.y - transform.position.y);
        direction.Normalize();
        rb2d.velocity = direction * speed;
    }


    private void updateAnimations()
    {
        float magnitude = rb2d.velocity.y * rb2d.velocity.x;
        Mathf.Clamp(magnitude, -1, 1);
        animator.SetFloat("direction",rb2d.velocity.x);
    }

    public void setSpeed(float speed) { this.speed = speed; }
    public void setDamage(float damage) { this.damage = damage; }
    public void setDirection(Vector2 playerPosition) { this.playerPosition = playerPosition; }

    public void applyDamageToCharacter(ICharacter character)
    {
        character.reciveDamage(damage);
        destroy();
    }


    public void destroy()
    {
        Destroy(gameObject);
    }
}
