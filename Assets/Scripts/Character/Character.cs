using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour, IHitable, IPushable, IDamageable, IMoveable, IEffectable, IEntity
{
    [SerializeField]
    private AudioClip _dashSound;
    [SerializeField]
    private AudioClip _hitSound;

    private RangeWeapon _weapon;

    private CharacterConfiguration _config;

    private float _currentHealsPoints;

    [SerializeField]
    private AnimationCurve _colorAlfaOnInvulnerable;
    private Coroutine _currentDashingCoroutine;
    private bool _isDashing;
    private bool _isInvulnerable;
    private bool _isPushed;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    private AudioSource _audioSource;

    [SerializeField]
    private AudioSourceProvider _audioSourceProvider;
    [SerializeField]
    private PolygonCollider2D _colliderForEnemy;

    public float CurrentHealsPoints => _currentHealsPoints;

    public Transform Transform => transform;
    public Vector2 Velocity => _rigidbody2D.velocity;
    public float MovementSpeed => _config.MovementSpeed;

    public IDamageable Damageable => this;
    public IHitable Hitable => this;
    public IMoveable Moveable => this;

    private List<Effect> _appliedEffects = new List<Effect>();

    public event Action OnEndedDash;
    public event Action OnHealsChanged;
    public event Action OnDied;

    private void Update()
    {
        for (int i = 0; i < _appliedEffects.Count; i++)
        {
            _appliedEffects[i].Update();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IPushable pushable))
        {
            var pushForce = _isDashing ? _config.DashingPushForce : _config.DefaultPushForce;
            var forceDirection = Vector2.ClampMagnitude(collision.transform.position - transform.position, 1);
            pushable.ApplyForce(forceDirection, pushForce, 0.2f);
        }

        if (_isDashing)
        {
            if (collision.gameObject.TryGetComponent(out Enemy enemy))
            {
                enemy.RecieveHit(_config.CollisionDamage, gameObject);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        OnCollisionEnter2D(collision);
    }

    public void Initialize(CharacterConfiguration config)
    {
        _config = config;
        _currentHealsPoints = _config.MaxHealsPoints;

        _weapon = Instantiate(config.Weapon, transform);
        _weapon.Initialize(gameObject, _config.ProjectileEffects, _config.ProjectileAdditiveDamage, _config.ProjectileAdditiveSpeed);
        config.OnProjectileAdditiveDamageChanged += (newValue) => _weapon.SetAdditiveDamage(newValue);
        config.OnProjectileAdditiveSpeedChanged += (newValue) => _weapon.SetAdditiveSpeed(newValue);

        _audioSource = AudioSourceProvider.Instance.GetSoundsSource();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Dash(Vector2 direction)
    {
        _currentDashingCoroutine = StartCoroutine(DashCoroutine(direction));
        _audioSource.PlayOneShot(_dashSound);
    }

    public void Move(Vector2 direction)
    {
        if (!_isDashing)
        {
            _rigidbody2D.velocity = (Vector3)direction * _config.MovementSpeed;
        }
    }

    public void Attack(Vector3 targetPosition)
    {
        transform.LookAt2D(targetPosition);
        CameraShaker.Instance.ShakeCamera(0.05f, 0.05f);
        _weapon.Attack(targetPosition, _config.Projectile);
    }

    public void RecieveHit(float damage, GameObject sender)
    {
        if (!_isDashing && !_isInvulnerable)
        {
            StartCoroutine(BecomeInvulnerable(_config.InvulnerabilityAfterHitDuration));
            StartCoroutine(TookDamageAnimation(_config.InvulnerabilityAfterHitDuration));
            ApplyDamage(damage);
        }
    }

    public void ApplyEffect(Effect effect)
    {
        var clonedEffect = effect.Clone();
        clonedEffect.Initialize(this);
        _appliedEffects.Add(clonedEffect);
        clonedEffect.OnEnded += RemoveEffect;
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

    public void ApplyDamage(float damage)
    {
        if (damage < 0)
        {
            throw new Exception("Incorrect damage");
        }

        _currentHealsPoints = _currentHealsPoints - damage > 0 ? _currentHealsPoints - damage : 0;
        OnHealsChanged?.Invoke();
        _audioSource.PlayOneShot(_hitSound);

        if (_currentHealsPoints == 0)
        {
            Die();
        }
    }

    public void ApplyForce(Vector2 direction, float force, float duration)
    {
        StartCoroutine(PushingCoroutine(direction, force, duration));
    }

    private IEnumerator BecomeInvulnerable(float time)
    {
        _isInvulnerable = true;
        _colliderForEnemy.isTrigger = true;
        yield return new WaitForSeconds(time);
        _colliderForEnemy.isTrigger = false;
        _isInvulnerable = false;
    }

    private IEnumerator TookDamageAnimation(float time)
    {
        var startColor = _spriteRenderer.color;

        float timeFromStart = 0;
        while (timeFromStart < 1)
        {
            var currentAlfa = _colorAlfaOnInvulnerable.Evaluate(timeFromStart);
            _spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, currentAlfa);

            timeFromStart += Time.deltaTime / time;
            yield return null;
        }

        _spriteRenderer.color = startColor;
    }

    private IEnumerator DashCoroutine(Vector2 direction)
    {
        if (_isDashing || direction == Vector2.zero)
        {
            yield break;
        }

        _isDashing = true;

        _colliderForEnemy.isTrigger = true;
        _rigidbody2D.velocity = direction * _config.DashDistance / _config.DashDuration;
        yield return new WaitForSeconds(_config.DashDuration);
        _rigidbody2D.velocity = Vector2.zero;
        _colliderForEnemy.isTrigger = false;
        _isDashing = false;
        OnEndedDash?.Invoke();
    }

    private IEnumerator PushingCoroutine(Vector2 direction, float force, float duration)
    {
        if (_isPushed)
        {
            yield break;
        }

        _isPushed = true;
        var initialForce = force;
        while (force > 0)
        {
            _rigidbody2D.velocity = direction * force;
            force -= Time.deltaTime / duration * initialForce;
            yield return null;
        }

        _isPushed = false;
        _rigidbody2D.velocity = Vector2.zero;
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
  
    public virtual void RemoveEffect(Effect effect)
    {
        _appliedEffects.Remove(effect);
    }

    private void Die()
    {
        OnDied?.Invoke();
    }
}
