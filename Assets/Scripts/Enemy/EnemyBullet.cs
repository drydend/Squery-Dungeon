using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

[RequireComponent(typeof(Animator))]
public class EnemyBullet : Enemy
{
    public const string AttackChargingAnimationTrigger = "Charging attack trigger";
    public const string FlyingAnimationTrigger = "Flying trigger";
    public const string HitAnimationTrigger = "Hit trigger";
    public const string StopingAnimationTrigger = "Stoping trigger";

    [SerializeField]
    private float _collisionDamage;
    [SerializeField]
    private float _attackSpeed;

    [Header("Flying state")]
    [SerializeField]
    private float _flightSpeed;
    [SerializeField]
    private ParticleSystem _flightParticlePrefab;
    [SerializeField]
    private float _flightTime;

    [Header("Charging attack state")]
    [SerializeField]
    private ParticleSystem _onEndChargingParticlePrefab;
    [SerializeField]
    private ParticleSystem _chargingParticlePrefab;
    [SerializeField]
    private float _chargingAnimationDuration = 1;
    [SerializeField]
    private float _chargingDuration = 1;

    [Header("Stoping state")]
    [SerializeField]
    private float _stopTime = 1;
    [SerializeField]
    private AnimationCurve _stopCurve;
    [SerializeField]
    private float _stopingAnimationDuration = 1;
    [SerializeField]
    private float _damageInFlight = 1f;

    private ParticleSystem _flightParticle;
    private ParticleSystem _chargingAttackParticle;
    private Vector2 _targetLastVisiblePosition;

    private Timer _attackTimer;
    private Animator _animator;
    private bool _isStoped = true;

    private bool _isFlying;
    private bool _isChargingAttack;

    public Vector2 TargetLastVisiblePosition => _targetLastVisiblePosition;

    private event Action OnAttackEnd;

    private void Start()
    {
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
    }

    private void Update()
    {
        if (!_isSpawned)
        {
            return;
        }

        _attackTimer.UpdateTick(Time.deltaTime);
        _currentState.Update();

        if (CanSeeTarget())
        {
            _targetLastVisiblePosition = Target.Transform.position;
        }

        if (_isFlying || _isChargingAttack || !_isStoped)
        {
            return;
        }

        if (CanSeeTarget())
        {
            if (DistanceToTarget < MaxAttackDistance && _attackTimer.IsFinished)
            {
                SwitchState<BulletEnemyChargingAttackState>();
            }
            else if (DistanceToTarget > MinAttakcDistance)
            {
                SwitchState<ChasingState>();
            }
            else
            {
                SwitchState<IdleState>();
            }
        }
        else
        {
            SwitchState<ChasingState>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var collisionDamage = _isFlying ? _damageInFlight : _collisionDamage;

        if (_isFlying)
        {
            var flightState = (BulletEnemyFlightState)_currentState;
            flightState.HandleCollision(collision);
        }

        if (collision.gameObject.TryGetComponent(out IHitable hitable))
        {
            hitable.RecieveHit(collisionDamage, gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        OnCollisionEnter2D(collision);
    }

    public override void ApplyForce(Vector2 direction, float force, float duration)
    {
        if (_isFlying || !_isStoped)
        {
            return;
        }

        base.ApplyForce(direction, force, duration);
    }

    public override void Initialize(Character target)
    {
        base.Initialize(target);

        _animator = GetComponent<Animator>();
        _attackTimer = new Timer(_attackSpeed);
        OnAttackEnd += _attackTimer.ResetTimer;

        OnDie += OnDied;

        _allAvaibleStates = new List<BaseEnemyState>()
        {
            new SpawningState(this),
            new IdleState(this,_navMeshAgent),
            new ChasingState(this, _navMeshAgent),
            new BulletEnemyChargingAttackState(this,_navMeshAgent, _animator, _chargingAnimationDuration, _chargingDuration),
            new BulletEnemyFlightState(this, _navMeshAgent, _rigidbody2D,_flightSpeed ,_animator),
            new BulletEnemyStopingState(this,_navMeshAgent ,_rigidbody2D, _stopTime, _stopCurve, _animator,
                _stopingAnimationDuration)
        };

        _currentState = _allAvaibleStates.FirstOrDefault(state => state is SpawningState);
        _currentState.OnEnter();
    }

    public bool TryFindIntercectionPointWithTarget(out Vector2 interceptionPoint)
    {
        var vectorFromPlayerToEnemy = (Vector2)(transform.position - _target.transform.position);

        float a = _flightSpeed * _flightSpeed - _target.MovementSpeed * _target.MovementSpeed;
        float b = 2 * Vector2.Dot(vectorFromPlayerToEnemy, _target.Velocity);
        float c = -vectorFromPlayerToEnemy.sqrMagnitude;

        float t1 = 0;
        float t2 = 0;

        if (!Utils.Math.QuadraticSolver(a, b, c, ref t1, ref t2))
        {
            interceptionPoint = Vector2.zero;
            return false;
        }
       
        if(t1 < 0 && t2 < 0)
        {
            interceptionPoint = Vector2.zero;
            return false;
        }

        float timeToInterception = 0;

        if(t1 > 0 && t2 > 0)
        {
            timeToInterception = Math.Min(t1, t2);
        }
        else
        {
            timeToInterception = Math.Max(t1, t2);
        }

        interceptionPoint = (Vector2)_target.transform.position + _target.Velocity * timeToInterception;
        return true;
    }

    public void OnBeginAttackcharging()
    {
        _chargingAttackParticle = Instantiate(_chargingParticlePrefab, transform.position, Quaternion.identity);
        _isChargingAttack = true;
    }

    public void OnEndAttackCharging()
    {
        _isChargingAttack = false;
        Destroy(_chargingAttackParticle.gameObject);
        Instantiate(_onEndChargingParticlePrefab, gameObject.transform.position, Quaternion.identity);
        SwitchState<BulletEnemyFlightState>();
    }

    public void OnBeginFlying()
    {
        _isFlying = true;
        _flightParticle = Instantiate(_flightParticlePrefab, transform);
    }

    public void OnEndFlying()
    {
        Destroy(_flightParticle.gameObject, 1f);
        SwitchState<BulletEnemyStopingState>();
        _isFlying = false;
    }

    public void OnBeginStoping()
    {
        _isStoped = false;
    }

    public void OnStoped()
    {
        OnAttackEnd?.Invoke();
        _isStoped = true;
        SwitchState<IdleState>();
    }

    private void OnDied()
    {
        Destroy(_chargingAttackParticle);
        Destroy(_flightParticle);
    }
}