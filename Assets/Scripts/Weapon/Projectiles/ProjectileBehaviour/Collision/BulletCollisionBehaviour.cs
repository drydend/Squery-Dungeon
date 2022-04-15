using UnityEngine;


public abstract class BulletCollisionBehaviour : ScriptableObject
{
    protected Projectile _projectile;

    public BulletCollisionBehaviour Initialize(Projectile projectile)
    {
        var collisionBehaviour = (BulletCollisionBehaviour)MemberwiseClone();
        collisionBehaviour._projectile = projectile;
        return collisionBehaviour;
    }

    public abstract void HandleCollision(Collision2D collision);
}

