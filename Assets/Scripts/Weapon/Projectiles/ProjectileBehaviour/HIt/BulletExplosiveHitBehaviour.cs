using UnityEngine;

[CreateAssetMenu(menuName = "Bullet hit behaviour/Explosive hit behaviour", fileName = "ExplosionHitBehaviour")]
public class BulletExplosiveHitBehaviour : BulletHitBehaviour
{
    [SerializeField]
    private float _explosionRadius;

    public float ExplosionRadius => _explosionRadius;

    public override void HandleHit(IHitable target)
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

        _projectile.DestroyProjectile();   
    }
}

