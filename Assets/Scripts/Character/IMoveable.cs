public interface IMoveable
{
    public float MovementSpeed { get;}

    public void SetMomentSpeed(float value);
    public void DecreaseSpeed(float value);
    public void IncreaseSpeed(float value);
}

