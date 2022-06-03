using System;
using UnityEngine;

public abstract class Effect : ScriptableObject
{
    public abstract event Action<Effect> OnEnded;

    public abstract void Initialize(IEntity entity);
    public abstract bool CanBeAppliedTo(IEntity entity);
    public abstract void Update();

    public Effect Clone()
    {
        return (Effect)MemberwiseClone();
    }
}

