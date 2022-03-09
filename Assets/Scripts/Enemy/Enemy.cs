using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField]
    protected ParticleSystem _spawnChargingParticle;
    [SerializeField]
    protected ParticleSystem _spawnExplosionParticle;
    [SerializeField]
    protected Character _target;
    [SerializeField]
    protected float _maxHealsPoints;
    protected float _currentHealsPoints;
    [SerializeField]    
    protected float _maxAttackDistance;
    [SerializeField]
    protected float _minAttackDistance;
    [SerializeField] [Range(1 , 10)]
    protected int _enemyDifficulty;
    [SerializeField]
    protected LayerMask _raycastLayers;
    protected NavMeshAgent _navMeshAgent;
    protected bool _isDead;
    protected bool _isSpawned;

    protected SpriteRenderer _spriteRenderer;
    protected Collider2D _collider; 

    [SerializeField]
    private float _spawnAnimationDuration = 1;

    public int Difficulty => _enemyDifficulty;
    public event Action OnDie;

    public void OnSpawned()
    {
        _isSpawned = false;
        StartCoroutine(SpawnAnimation());
    }

    public void Initialize()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<CircleCollider2D>();
    }

    protected virtual IEnumerator SpawnAnimation()
    {
        _spriteRenderer.enabled = false;
        _collider.enabled = false;
        var spawningParticle = Instantiate(_spawnChargingParticle, transform);
        yield return new WaitUntil(() => spawningParticle.isStopped);
        var spawnExplosionParticle = Instantiate(_spawnExplosionParticle, transform);
        _spriteRenderer.enabled = true;
        _collider.enabled = true;
        _isSpawned = true;
    }

    public void SetTarget(Character target)
    {
        _target = target;
    }

    public virtual void RecieveDamage(float damage, GameObject sender)
    {
        if (damage >= 0 && !_isDead)
        {
            _currentHealsPoints -= damage;
            if (_currentHealsPoints <= 0)
                Die();
        }
        else
        {
            Debug.LogError("Already dead or damage less than zero");
        }
    }

    private protected virtual void Die() 
    {
        _isDead = true;
        OnDie?.Invoke();
        Destroy(gameObject);
    }
}
