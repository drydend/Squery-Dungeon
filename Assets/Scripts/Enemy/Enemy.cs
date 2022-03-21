using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class Enemy : MonoBehaviour, IHitable, IPushable
{
    [Header("Spawn animation")]
    [SerializeField]
    protected ParticleSystem _spawnChargingParticle;
    [SerializeField]
    protected ParticleSystem _spawnExplosionParticle;
    [SerializeField]
    protected float _spawnAnimationDuration = 1;

    [Header("Recieving damage animation")]
    [SerializeField]
    protected float _recievingDamageAnimationDuration;
    [SerializeField]
    protected Color _recievingDamageColor;
    [SerializeField]
    protected AnimationCurve _recievingDamageAnimation;


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

    protected Rigidbody2D _rigidbody2D;
    protected Animator _animator;
    protected SpriteRenderer _spriteRenderer;
    protected Collider2D _collider;

    [SerializeField]
    private float _pushingDuration = 0.4f;
    private bool _isPushed;
    private Coroutine _pushingCoroutine;

    public float CameraShakeStrenghtOnDeath => _cameraShakeStrenghtOnDeath;
    public float CameraShakeDurationOnDeath => _cameraShakeDurationOnDeath;
    public float MaxAttackDistance => _maxAttackDistance;
    public float MinAttakcDistance => _minAttackDistance;
    public float DistanceToTarget => Vector2.Distance(_target.transform.position, transform.position);
    public Character Target => _target;
    public int Difficulty => _enemyDifficulty;

    public event Action OnDie;
    public event Action OnSpawned;

    public void OnSpawn()
    {
        StartCoroutine(SpawnAnimation());
    }

    public virtual void Attack()
    {

    }

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

    public bool CanSeeTarget()
    {
        var raycastDirection = -(transform.position - _target.transform.position).normalized;
        var ray = Physics2D.RaycastAll(transform.position, raycastDirection, Mathf.Infinity, _raycastLayers);
        if (ray.Length > 1)
        {
            return ray[1].collider.TryGetComponent(out Character character);
        }
        else if (ray.Length == 1)
        {
            return ray[0].collider.TryGetComponent(out Character character);
        }
        else
        {
            return false;
        }
    }

    public virtual void Initialize(Character target)
    {
        _currentHealsPoints = _maxHealsPoints;
        _target = target;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<PolygonCollider2D>();
    }

    public virtual void RecieveHit(float damage, GameObject sender)
    {
        if (damage >= 0 && !_isDead)
        {
            if (!_isInvulnerable)
            {
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
                    StartCoroutine(RecievingDamageAnimation());
                }

            }
        }
        else
        {
            Debug.LogError("Already dead or damage less than zero");
        }
    }

    public void ApplyForce(Vector2 direction, float force)
    {
        if (_pushingCoroutine != null)
        {
            StopCoroutine(_pushingCoroutine);
        }

        _pushingCoroutine = StartCoroutine(PushingCoroutine(direction, force));

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

    protected virtual IEnumerator RecievingDamageAnimation()
    {
        _isInvulnerable = true;
        float timeFromStart = 0;
        Color currentColor = _spriteRenderer.color;
        while (timeFromStart < 1)
        {
            var animationValue = _recievingDamageAnimation.Evaluate(timeFromStart);
            _spriteRenderer.color = Color.Lerp(currentColor, _recievingDamageColor, animationValue);

            timeFromStart += Time.deltaTime / _recievingDamageAnimationDuration;
            yield return null;
        }
        _isInvulnerable = false;
    }

    protected virtual void Die()
    {
        _isDead = true;
        Instantiate(_deathParticle, transform.position, Quaternion.identity);
        OnDie?.Invoke();
        Destroy(gameObject);
    }
    
    private IEnumerator PushingCoroutine(Vector2 direction, float force)
    {
        var initialForce = force;
        while (force > 0)
        {
            _rigidbody2D.velocity = direction * force;
            force -= Time.deltaTime / _pushingDuration * initialForce;
            yield return null;
        }

        _rigidbody2D.velocity = Vector2.zero;
    }

    protected void OnCollisionEnter2DBase(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Character character))
        {
            var forceDirection = Vector2.ClampMagnitude(collision.transform.position - transform.position, 1);
            character.ApplyForce(forceDirection, 25);
            character.RecieveHit(1, gameObject);
        }
    }

}
