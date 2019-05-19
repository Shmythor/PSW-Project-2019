using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chasing : AStateEnemy
{
    private GameObject projectile;

    public Chasing(Rigidbody2D rb2d, float defSpeed, ICharacter character) : base(rb2d, defSpeed)
    {
        actualSpeed = defSpeed;
        this.character = character;
        movePosition = Vector2.zero;
    }

    public override void movement()
    {
        lookForThePlayer();
        base.movement();
    }

    private void lookForThePlayer()
    {
        movePosition = character.getPosition();
    }
}
