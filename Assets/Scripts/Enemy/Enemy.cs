﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator), typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour, IHitable, IPushable
{
    [Header("Spawn animation")]
    [SerializeField]
    protected ParticleSystem _spawnChargingParticle;
    [SerializeField]
    protected ParticleSystem _spawnExplosionParticle;
    [SerializeField]
    protected float _spawnAnimationDuration = 1;

    [Header("Heals")]
    [SerializeField]
    protected float _maxHealsPoints;
    protected float _currentHealsPoints;

    [Header("Camera shake configuration")]
    [SerializeField]
    protected float _cameraShakeStrenghtOnDeath = 0.1f;
    [SerializeField]
    protected float _cameraShakeDurationOnDeath = 0.2f;
    [SerializeField]
    protected ParticleSystem _deathParticle;
    [SerializeField]
    protected ParticleSystem _hitParticle;

    [Header("Attack configuration")]
    [SerializeField]
    protected float _maxAttackDistance;
    [SerializeField]
    protected float _minAttackDistance;

    [SerializeField]
    [Range(1, 10)]
    protected int _enemyDifficulty;

    [SerializeField]
    protected LayerMask _raycastLayers;
    protected Character _target;
    protected NavMeshAgent _navMeshAgent;

    protected BaseEnemyState _currentState;
    protected List<BaseEnemyState> _allAvaibleStates;

    protected bool _isInvulnerable;
    protected bool _isDead;
    protected bool _isSpawned;
    protected bool _isPaused;

    protected Rigidbody2D _rigidbody2D;
    protected SpriteRenderer _mainSpriteRenderer;
    protected Collider2D _collider;
    protected List<SpriteRenderer> _allSpriteRendereres = new List<SpriteRenderer>();

    [SerializeField]
    private float _pushingDuration = 0.4f;
    private Coroutine _pushingCoroutine;

    public float MaxAttackDistance => _maxAttackDistance;
    public float MinAttakcDistance => _minAttackDistance;
    public float DistanceToTarget => Vector2.Distance(_target.transform.position, transform.position);
    public Character Target => _target;
    public int Difficulty => _enemyDifficulty;

    public Transform Transform => transform;

    public event Action OnDie;
    public event Action OnSpawned;

    public void Spawn()
    {
        _isSpawned = false;
        StartCoroutine(SpawnAnimation());
    }

    public virtual void Initialize(Character target)
    {
        _currentHealsPoints = _maxHealsPoints;
        _target = target;
        
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _mainSpriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<PolygonCollider2D>();
        
        _allSpriteRendereres = gameObject.GetComponentsInChildren<SpriteRenderer>().ToList();
        _allSpriteRendereres.Add(_mainSpriteRenderer);
    }

    public virtual void Attack() { }

    public void SwitchState<State>() where State : BaseEnemyState
    {
        var state = _allAvaibleStates.FirstOrDefault(item => item is State);
        if (state == null)
        {
            throw new Exception($"This state is unavaible to be set {typeof(State)}");
        }
        else if (state != _currentState)
        {
            _currentState.OnExit();
            _currentState = state;
            _currentState.OnEnter();
        }
    }

    public BaseEnemyState GetState<State>() where State : BaseEnemyState
    {
        var state = _allAvaibleStates.FirstOrDefault(x => x is State);

        if (state == null)
        {
            throw new Exception($"This state is unavaible to get from enemy: {this}");
        }

        return state;
    }

    public bool CanSeePoint(Vector2 point)
    {
        var raycastDirection = (point - (Vector2)transform.position).normalized;
        var hits = Physics2D.RaycastAll(transform.position, raycastDirection, Vector2.Distance(point, transform.position), _raycastLayers);

        return !(hits.Length > 1);
    }

    public bool CanSeeTarget()
    {
        var raycastDirection = (_target.transform.position - transform.position).normalized;
        var hits = Physics2D.RaycastAll(transform.position, raycastDirection, Mathf.Infinity, _raycastLayers);
        if (hits.Length > 1)
        {
            return hits[1].collider.TryGetComponent(out Character character);
        }
        else if (hits.Length == 1)
        {
            return hits[0].collider.TryGetComponent(out Character character);
        }
        else
        {
            return false;
        }
    }

    public virtual void RecieveHit(float damage, GameObject sender)
    {
        if (_isInvulnerable || _isDead)
        {
            return;
        }

        if (damage <= 0)
        {
            throw new Exception("Damage less than zero");
        }

        _currentHealsPoints -= damage;
        if (_currentHealsPoints <= 0)
        {
            Die();
        }
        else
        {
            var hitParticle = Instantiate(_hitParticle, transform.position, Quaternion.identity);
            var particleStartColor = hitParticle.main.startColor;
            particleStartColor = new ParticleSystem.MinMaxGradient(Color.white);
            OnRecieveHit();
        }
    }

    public virtual void ApplyForce(Vector2 direction, float force, float duration)
    {
        if (_pushingCoroutine != null)
        {
            StopCoroutine(_pushingCoroutine);
        }

        _pushingCoroutine = StartCoroutine(PushingCoroutine(direction, force, duration));
    }

    public virtual void BecomeInvulnarable()
    {
        _isInvulnerable = true;
    }

    public virtual void BecomeVulnarable()
    {
        _isInvulnerable = false;
    }

    protected virtual void OnRecieveHit() { }
        
    protected virtual IEnumerator SpawnAnimation()
    {
        foreach (var spriteRenderer in _allSpriteRendereres)
        {
            spriteRenderer.enabled = false;
        }

        _collider.enabled = false;

        var spawningParticle = Instantiate(_spawnChargingParticle, transform);
        yield return new WaitForSeconds(_spawnChargingParticle.main.duration + 0.4f);
        var spawnExplosionParticle = Instantiate(_spawnExplosionParticle, transform);

        _collider.enabled = true;
        _isSpawned = true;

        foreach (var spriteRenderer in _allSpriteRendereres)
        {
            spriteRenderer.enabled = true;
        }
    }

    protected virtual void Die()
    {   
        _isDead = true;
        Instantiate(_deathParticle, transform.position, Quaternion.identity);
        CameraShaker.Instance.ShakeCamera(_cameraShakeDurationOnDeath, _cameraShakeStrenghtOnDeath);
        OnDie?.Invoke();
        Destroy(gameObject);
    }

    private IEnumerator PushingCoroutine(Vector2 direction, float force, float duration)
    {
        var initialForce = force;
        while (force > 0)
        {
            _rigidbody2D.velocity = direction * force;
            force -= Time.deltaTime / duration * initialForce;
            yield return null;
        }

        _rigidbody2D.velocity = Vector2.zero;
    }

}
