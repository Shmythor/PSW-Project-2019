using UnityEngine;

public interface IPlayer
{
    //void Init();
    void tick(float d);
    void updateInputs(float ver, float hor, float magnitude);
    Vector2 getPosition(); //It lets the enemy know the position of the player
    Health_Component getHealthComponent();
}
