

public interface IPlayer
{
    void Init();
    void tick(float d);
    void die();
    void updateInputs(float ver, float hor, float magnitude);
}
