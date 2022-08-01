using UnityEngine;

[CreateAssetMenu(menuName = "Bullet hit behaviour/Regular hit behaviour", fileName = "RegularHitBehaviour")]
public class RegularHitBehaviour : BulletHitBehaviour
{
    public override void HandleHit(IEntity entity)
    {
        entity.Hitable?.RecieveHit(_projectile.Damage, _projectile.Sender);
        _projectile.DestroyProjectile();
    }
}


