using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Boss : MonoBehaviour, IEntity, IDamageable, IHitable, IEffectable, IMoveable
{
    [SerializeField]
    protected List<BossStage> _allStages;
    protected List<BossStage> _leftStages;
    protected BossStage _currentStage;

    protected List<Effect> _appliedEffects = new List<Effect>();

    protected float _heals;
    [SerializeField]
    protected float _maxHeals;
    [SerializeField]
    protected float _movementSpeed;
    [SerializeField]
    protected float _rotationSpeed;
    [SerializeField]
    protected float _cameraShakeOnDieingDuration;

    [SerializeField]
    protected AudioClip _hitSound;
    [SerializeField]
    protected AudioClip _deathSound;

    protected bool _isDied = false;
    protected bool _isSpawned = false;
    protected NavMeshAgent _navMeshAgent;
    protected Character _target;
    protected AudioSource _audioSource;

    [SerializeField]
    private ParticleSystem _deathParticle;

    private Coroutine _chasingCoroutine;
    private Coroutine _followingCoroutine;

    public event Action OnHealsChanged;
    public event Action OnDied;
    public event Action OnDefeated;

    public float MovementSpeed => _movementSpeed;
    public bool IsInvulnerable { get; protected set; }

    public IDamageable Damageable => this;
    public IHitable Hitable => this;
    public IMoveable Moveable => this;
    public Transform Transform => transform;
    public Character Target => _target;

    public virtual void Initialize(Character target)
    {
        _target = target;
        
        _heals = _maxHeals;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _audioSource = AudioSourceProvider.Instance.GetSoundsSource();

        foreach (var stage in _allStages)
        {
            stage.Initialize(this);
        }
    }

    public void OnSpawned()
    {
        IsInvulnerable = false;
        _isSpawned = true;
        gameObject.SetActive(true);
    }

    public void ChaseTarget(float speed)
    {
       _chasingCoroutine = StartCoroutine(ChaseTargetCoroutine());
       _navMeshAgent.isStopped = false;
       _navMeshAgent.speed = speed;
    }

    public void StopChasingTarget()
    {
        if(_chasingCoroutine == null)
        {
            return; 
        }

        StopCoroutine(_chasingCoroutine);
        _navMeshAgent.isStopped = true;
    }

    public void FollowPlayer()
    {
        _followingCoroutine = StartCoroutine(SmoothFollowPlayer());

    }
  
    public void StopFollowingPlayer()
    {   
        if(_followingCoroutine == null)
        {
            return;
        }

        StopCoroutine(_followingCoroutine);
    }

    public virtual void ApplyDamage(float damage)
    {
        if(damage < 0)
        {
            throw new Exception("Damage can`t be less than zero");
        }

        if(IsInvulnerable || _isDied)
        {
            return;
        }

        _audioSource.PlayOneShot(_hitSound);
        _heals = _heals - damage < 0 ? 0 : _heals - damage;
        OnHealsChanged?.Invoke();

        if(_heals == 0)
        {
            Die();
        }
    }

    public void ApplyEffect(Effect effect)
    {
        var clonedEffect = effect.Clone();
        clonedEffect.Initialize(this);
        _appliedEffects.Add(clonedEffect);
        clonedEffect.OnEnded += RemoveEffect;
    }

    public virtual void RemoveEffect(Effect effect)
    {
        _appliedEffects.Remove(effect);
    }
    
    public virtual void RemoveEffect(Type type)
    {
        foreach (var effect in _appliedEffects)
        {
            if (effect.GetType() == type)
            {
                _appliedEffects.Remove(effect);
            }
        }
    }

    public bool CanApplyEffect(Effect effect)
    {
        foreach (var appliedEffect in _appliedEffects)
        {
            if (appliedEffect.GetType() == effect.GetType())
            {
                return false;
            }
        }

        return effect.CanBeAppliedTo(this);
    }

    public void RecieveHit(float damage, GameObject sender)
    {
        ApplyDamage(damage);
    }

    protected virtual void Die()
    {
        _isDied = true;
        StartCoroutine(DieingCoroutine());
    }

    private IEnumerator DieingCoroutine()
    {
        StopFollowingPlayer();
        Instantiate(_deathParticle, transform.position, Quaternion.identity);
        CameraShaker.Instance.ShakeCamera(_cameraShakeOnDieingDuration, 0.2f, false);

        yield return new WaitForSeconds(_deathParticle.main.duration);
        _audioSource.PlayOneShot(_deathSound);
        OnDied?.Invoke();
        
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        OnDefeated?.Invoke();
    }

    private IEnumerator SmoothFollowPlayer()
    {
        while (true)
        {
            var directionToTarget = Target.transform.position - transform.position;
            directionToTarget.Normalize();
            var lookingDirection = Quaternion.AngleAxis(transform.rotation.eulerAngles.z, Vector3.forward ) * Vector3.left;
            lookingDirection.Normalize();

            var crossProduct = Vector3.Cross(directionToTarget,lookingDirection ).z;
            var rotationAngle = transform.rotation.eulerAngles.z + _rotationSpeed * Time.deltaTime * -crossProduct;

            transform.rotation = Quaternion.Euler(0, 0, rotationAngle);

            yield return null;
        }
    }

    private IEnumerator ChaseTargetCoroutine()
    {
        while (true)
        {
            transform.LookAt2D(_target.transform.position);

            if(Vector2.Distance(_target.transform.position, transform.position) < 3)
            {
                _navMeshAgent.SetDestination(transform.position);
            }
            else
            {
                _navMeshAgent.SetDestination(_target.transform.position);
            }

            yield return null;
        }
    }
}

