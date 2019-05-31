using UnityEngine;

public interface IPlayer: ICharacter
{   
    void tick(float d);
    void updateInputs(float ver, float hor, float magnitude);
    void restoreDamageTaken();
    void disableInputs();
    void enableInputs();
    void restoreHealth();
    void updateHealthFromLoadGameData(int hearts, float damage);
    void setHearts(int hearts);
    int getHearts();
}
