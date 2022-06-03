using UnityEngine;

public interface IDamageable
{   
    Transform Transform { get; }

    void ApplyDamage(float damage);
}

