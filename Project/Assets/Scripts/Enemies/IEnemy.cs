using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy : ICharacter
{
    void changeState(ICharacter characterToChase, states nextState);
    void stopEnemy();
    void resumeEnemy();
    Vector2 getVelocity();
}
