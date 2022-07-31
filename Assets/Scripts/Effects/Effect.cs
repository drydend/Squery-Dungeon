using System;
using UnityEngine;

public abstract class Effect : ScriptableObject
{
    [SerializeField]
    protected float _effectDuration;

    protected Timer _timer;
    protected IEntity _entity;

    public event Action<Effect> OnEnded;

    public abstract bool CanBeAppliedTo(IEntity entity);
    
    public virtual void Initialize(IEntity entity) 
    {
        _entity = entity;

        _timer = new Timer(_effectDuration);
        _timer.OnFinished += OnEffectEnded;
    }
    
    public virtual void Update()
    {
        _timer.UpdateTick(Time.deltaTime);
    }

    public Effect Clone()
    {
        return (Effect)MemberwiseClone();
    }

    protected virtual void OnEffectEnded()
    {
        OnEnded?.Invoke(this);
    }
}

