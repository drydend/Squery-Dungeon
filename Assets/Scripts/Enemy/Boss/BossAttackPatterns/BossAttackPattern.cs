using UnityEngine;
using System;

public class BossAttackPattern : ScriptableObject
{
    protected Boss _boss;

    public event Action OnEnded;

    public virtual void Initialize(Boss boss)
    {
        _boss = boss;
    }

    public virtual void Start()
    {

    }

    public virtual void Stop()
    {

    }

    protected void OnAttackEnded()
    {
        OnEnded?.Invoke();
    }
}

