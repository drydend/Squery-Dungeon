using UnityEngine;
using System.Linq;
using System.Collections.Generic;

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

    private List<IEntity> _hitEnties;

    private int _maxRicochetNumber;
    private float _damageMultiplier = 1f;

    public int RicochetNumber => _ricochetNumber;
    public float DamageDecreasing => _damageDecreasing;

    public override void Initialize(Projectile projectile)
    {
        base.Initialize(projectile);

        _hitEnties = new List<IEntity>();
        _maxRicochetNumber = (int)(1 / _damageDecreasing);
        
        if(_ricochetNumber > _maxRicochetNumber)
        {
            _ricochetNumber = _maxRicochetNumber;
        }
    }

    public override void HandleHit(IEntity entity)
    {   
        entity.Hitable.RecieveHit(_projectile.Damage * _damageMultiplier, _projectile.Sender);
        _hitEnties.Add(entity);
        _projectile.PlayHitParticle();

        if (_ricochetNumber == 0 || !entity.Transform.TryGetComponent(out Enemy enemy))
        {
            _projectile.DestroyProjectile();
            return;
        }

        _damageMultiplier -= _damageDecreasing;
        _ricochetNumber--;
        var colliders = Physics2D.OverlapCircleAll(entity.Transform.position, _maxRange, _raycastLayers);
        var enemies = colliders.Where(collider => !_hitEnties.Contains(collider.gameObject.GetComponent<IEntity>()) &&
        collider.gameObject.TryGetComponent(out Enemy currentEnemy) 
            && currentEnemy != entity.Transform.GetComponent<Enemy>());

        var closestEnemy = enemies.OrderBy(obj => Vector2.Distance(entity.Transform.position, obj.transform.position))
            .FirstOrDefault();

        if(closestEnemy == null)
        {
            _projectile.DestroyProjectile();
            return;
        }

        _projectile.transform.position = entity.Transform.position;
        var direction = VectorExtensions.ClampMagnitude(closestEnemy.transform.position - _projectile.transform.position, 1, 1);
        _projectile.ChangeMoveDirection(direction);
    }

}

