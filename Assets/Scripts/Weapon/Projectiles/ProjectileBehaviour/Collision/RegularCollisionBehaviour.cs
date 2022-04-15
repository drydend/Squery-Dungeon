using UnityEngine;

[CreateAssetMenu(menuName = "Bullet collision behaviour/Regular collision behaviour", fileName = "RegularCollisionBehaviour")]
public class RegularCollisionBehaviour : BulletCollisionBehaviour
{
    public override void HandleCollision(Collision2D collision)
    {   
        _projectile.DestroyProjectile();
        _projectile.PlayCollisionParticle();
    }
}

