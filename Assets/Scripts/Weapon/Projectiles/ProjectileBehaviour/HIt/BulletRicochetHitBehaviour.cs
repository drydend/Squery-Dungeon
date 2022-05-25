using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Bullet hit behaviour/Ricochet hit behaviour", fileName = "RicochetHitBehaviour")]
public class BulletRicochetHitBehaviour : BulletHitBehaviour
{
    [SerializeField]
    private float _maxRange;
    [SerializeField]
    private int _ricochetNumber = 1;
    [SerializeField]
    private float _damageDecreasing = 0.3f;
    [SerializeField]
    private LayerMask _raycastLayers;
    [SerializeField]
    private IHitable _previourHitable;

    private int _maxRicochetNumber;
    private float _damageMultiplier = 1f;

    public int RicochetNumber => _ricochetNumber;
    public float DamageDecreasing => _damageDecreasing;

    public override BulletHitBehaviour Initialize(Projectile projectile)
    {
        _maxRicochetNumber = (int)(1 / _damageDecreasing);
        
        if(_ricochetNumber > _maxRicochetNumber)
        {
            throw new System.Exception("Ricochet number is more than max ricochet number");
        }

        return base.Initialize(projectile);
    }

    public override void HandleHit(IHitable target)
    {   
        target.RecieveHit(_projectile.Damage * _damageMultiplier, _projectile.Sender);
        _projectile.PlayHitParticle();

        if (_ricochetNumber == 0 || !target.Transform.TryGetComponent(out Enemy enemy))
        {
            _projectile.DestroyProjectile();
            return;
        }

        _damageMultiplier -= _damageDecreasing;
        _ricochetNumber--;
        var colliders = Physics2D.OverlapCircleAll(target.Transform.position, _maxRange);
        var enemies = colliders.Where(collider => collider.gameObject.TryGetComponent(out Enemy currentEnemy) 
            && currentEnemy != target.Transform.GetComponent<Enemy>());
        var closestEnemy = enemies.OrderBy(obj => Vector2.Distance(target.Transform.position, obj.transform.position))
            .FirstOrDefault();

        if(closestEnemy == null)
        {
            _projectile.DestroyProjectile();
            return;
        }

        _projectile.transform.position = target.Transform.position;
        var direction = VectorExtensions.ClampMagnitude(closestEnemy.transform.position - _projectile.transform.position, 1, 1);
        _projectile.ChangeMoveDirection(direction);
    }

}

