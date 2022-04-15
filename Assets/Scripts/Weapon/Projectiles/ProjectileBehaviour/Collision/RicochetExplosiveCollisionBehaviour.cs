using UnityEngine;

public class RicochetExplosiveCollisionBehaviour : RicochetCollisionBehaviour
{
    private float _explosionRadius;

    public RicochetExplosiveCollisionBehaviour(BulletExplosiveHitBehaviour explosiveHitBehaviour,
        RicochetCollisionBehaviour recochetCollisionBehaviour)
    {
        _ricochetNumber = recochetCollisionBehaviour.RicochetNumber;
        _explosionRadius = explosiveHitBehaviour.ExplosionRadius;
    }

    public override void HandleCollision(Collision2D collision)
    {
        var colliders = Physics2D.OverlapCircleAll(_projectile.transform.position, _explosionRadius);
        CameraShaker.Instance.ShakeCamera(0.1f, 0.05f);

        foreach (var collider in colliders)
        {
            if (collider.gameObject == _projectile.Sender)
                continue;

            collider.GetComponent<IHitable>()?.RecieveHit(_projectile.Damage, _projectile.gameObject);
            collider.GetComponent<IPushable>()?.ApplyForce(collider.transform.position - _projectile.transform.position, 15, 0.2f);
        }

        base.HandleCollision(collision);
    }

}
