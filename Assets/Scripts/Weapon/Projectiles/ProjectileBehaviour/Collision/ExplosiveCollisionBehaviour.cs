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

    public override void HandleCollision(Collider2D collider)
    {
        var colliders = Physics2D.OverlapCircleAll(_projectile.transform.position, _explosionRadius);
        CameraShaker.Instance.ShakeCamera(0.1f, 0.05f);

        foreach (var item in colliders)
        {
            if (item.gameObject == _projectile.Sender)
                continue;

            item.GetComponent<IHitable>()?.RecieveHit(_projectile.Damage, _projectile.gameObject);
            item.GetComponent<IPushable>()?.ApplyForce(item.transform.position - _projectile.transform.position, 15, 0.2f);
        }

        _projectile.PlayCollisionParticle();
        _projectile.DestroyProjectile();
    }
}

