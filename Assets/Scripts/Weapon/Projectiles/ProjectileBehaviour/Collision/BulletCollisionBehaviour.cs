using UnityEngine;


public abstract class BulletCollisionBehaviour : ScriptableObject
{
    protected Projectile _projectile;

    public void Initialize(Projectile projectile)
    {
        _projectile = projectile;
    }

    public BulletCollisionBehaviour Clone()
    {
        return (BulletCollisionBehaviour)MemberwiseClone();
    }

    public abstract void HandleCollision(Collider2D collider);
}

