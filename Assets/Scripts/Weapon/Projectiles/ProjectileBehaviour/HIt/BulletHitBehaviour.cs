using UnityEngine;

public abstract class BulletHitBehaviour : ScriptableObject
{
    protected Projectile _projectile;

    public virtual BulletHitBehaviour Initialize(Projectile projectile)
    {
        var hitBehaviour = (BulletHitBehaviour)MemberwiseClone();
        hitBehaviour._projectile = projectile;
        return hitBehaviour;
    }

    public abstract void HandleHit(IHitable target);
}

