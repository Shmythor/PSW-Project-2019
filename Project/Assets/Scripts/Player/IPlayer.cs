using UnityEngine;

public interface IPlayer: ICharacter
{   
    void tick(float d);
    void updateInputs(float ver, float hor, float magnitude);
    void restoreDamageTaken();
    void disableInputs();
    void enableInputs();
    void restoreHealth();
    void setHearts(int hearts);
}
