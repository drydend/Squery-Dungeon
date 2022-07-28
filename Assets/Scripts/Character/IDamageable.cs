using UnityEngine;
using System;

public interface IDamageable
{
    public event Action OnDied;

    void ApplyDamage(float damage);
}

