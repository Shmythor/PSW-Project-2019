
using UnityEngine;



public interface ICharacter 
{
    void reciveDamage(float damage);
    void die();
    Vector2 getPosition(); 
}
