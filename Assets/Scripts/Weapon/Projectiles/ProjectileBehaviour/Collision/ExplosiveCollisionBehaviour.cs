using UnityEngine;

public class ExplosiveCollisionBehaviour : BulletCollisionBehaviour
{
    [SerializeField]
    private float _explosionRadius;

    public float ExplosionRadius => _explosionRadius;
        
    public ExplosiveCollisionBehaviour(BulletExplosiveHitBehaviour explosiveHitBehaviour)
    {
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

        _projectile.PlayCollisionParticle();
        _projectile.DestroyProjectile();
    }
}

