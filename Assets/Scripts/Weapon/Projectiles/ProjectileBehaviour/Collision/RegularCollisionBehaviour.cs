using UnityEngine;

[CreateAssetMenu(menuName = "Bullet collision behaviour/Regular collision behaviour", fileName = "RegularCollisionBehaviour")]
public class RegularCollisionBehaviour : BulletCollisionBehaviour
{
    public override void HandleCollision(Collider2D collider)
    {   
        _projectile.DestroyProjectile();
        _projectile.PlayCollisionParticle();
    }
}

