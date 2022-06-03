using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _baseSpeed;
    [SerializeField]
    private float _baseDamage;
    private float _additiveDamage;
    private float _additiveSpeed;

    [SerializeField]
    private BulletCollisionBehaviour _collisionBehaviour;
    [SerializeField]
    private BulletHitBehaviour _hitBehaviour;
    [SerializeField]
    private ParticleSystem _hitParticle;
    [SerializeField]
    private ParticleSystem _collisionParticle;

    private List<Effect> _effectsToApply;

    private Vector2 _direction;
    private GameObject _sender;
    private bool _hitEnemy = false;

    private float _speedMultiplier = 1f;
    private float _damageMultiplier = 1f;

    private Rigidbody2D _rigidbody2D;

    public ParticleSystem HitParticle => _hitParticle;
    public ParticleSystem CollisionParticle => _collisionParticle;
    
    public BulletHitBehaviour HitBehaviour => _hitBehaviour;
    public BulletCollisionBehaviour CollisionBehaviour => _collisionBehaviour;
    
    public GameObject Sender => _sender;
    public Vector2 MoveDirection => _direction;
    public float Damage => (_baseDamage + _additiveDamage) * _damageMultiplier;
    public float Speed => (_baseSpeed + _additiveSpeed) * _speedMultiplier;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject == _sender || collider.isTrigger)
        {
            return;
        }

        if(collider.gameObject.TryGetComponent(out IEffectable effectable))
        {
            foreach (var effect in _effectsToApply)
            {
                if (effectable.CanApplyEffect(effect))
                {
                    effectable.ApplyEffect(effect.Clone());
                }
            }
        }

        if (collider.gameObject.TryGetComponent(out IHitable hitable))
        {
            _hitBehaviour.HandleHit(hitable);
        }
        else
        {
            _collisionBehaviour.HandleCollision(collider);
        }
    }

    public void Initialize(Vector2 direction, GameObject sender, List<Effect> effectsToApply ,float speedMultiplier,
        float damageMultiplier, float additiveDamage, float additiveSpeed)
    {   
        _additiveDamage = additiveDamage;
        _additiveSpeed = additiveSpeed;
        _sender = sender;
        _direction = direction;
        _effectsToApply = effectsToApply;
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _hitBehaviour = _hitBehaviour.Initialize(this);
        _collisionBehaviour = _collisionBehaviour.Initialize(this);
        Move();
    }

    public void DestroyProjectile()
    {
        PlayHitParticle();
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void PlayCollisionParticle()
    {
        if (_collisionParticle != null)
        {
            Instantiate(_collisionParticle, transform.position, Quaternion.identity);
        }
    }

    public void PlayHitParticle()
    {
        if (_hitParticle != null)
        {
            Instantiate(_hitParticle, transform.position, Quaternion.identity);
        }
    }

    public void ChangeMoveDirection(Vector2 direction)
    {
        _direction = direction;
        Move();
    }

    public void SetBaseDamage(float value)
    {
        if (value < 0)
        {
            throw new Exception("Value can`t be less than zero");
        }

        _baseDamage = value;
    }

    public void SetCollisionBehaviour(BulletCollisionBehaviour collisionBehaviour)
    {
        _collisionBehaviour = collisionBehaviour;
        _collisionBehaviour = _collisionBehaviour.Initialize(this);
    }

    public void SetCollisionParticle(ParticleSystem collisioinParticle)
    {
        _collisionParticle = collisioinParticle;
    }

    public void SetHitBehaviour(BulletHitBehaviour hitBehaviour)
    {
        _hitBehaviour = hitBehaviour;
        _hitBehaviour = _hitBehaviour.Initialize(this);
    }

    public void SetHitParticle(ParticleSystem hitParticle)
    {
        _hitParticle = hitParticle;
    }

    public Projectile Clone()
    {
        return (Projectile)MemberwiseClone();
    }

    private void Move()
    {
        transform.LookAt2D(transform.position + (Vector3)_direction);
        _rigidbody2D.velocity = _direction * Speed;
    }
}
