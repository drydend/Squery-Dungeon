using UnityEngine;

public abstract class BulletHitBehaviour : ScriptableObject
{
    protected Projectile _projectile;

    public virtual void Initialize(Projectile projectile)
    {
        _projectile = projectile;
    }

    public BulletHitBehaviour Clone()
    {
        return (BulletHitBehaviour)MemberwiseClone();
    }

    public abstract void HandleHit(IEntity target);
}

