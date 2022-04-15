using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour, IHitable, IPushable
{
    [SerializeField]
    private RangeWeapon _weapon;

    private CharacterConfiguration _config;

    private float _currentHealsPoints;

    private Coroutine _currentDashingCoroutine;
    [SerializeField]
    private AnimationCurve _colorAlfaOnInvulnerable;
    private Timer _attackTimer;
    private bool _isDashing;
    private bool _isInvulnerable;
    private bool _isPushed;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;

    public float CurrentHealsPoints => _currentHealsPoints;

    public event Action OnHealsChanged;
    public event Action OnDied;

    private void Update()
    {
        _attackTimer.UpdateTick(Time.deltaTime);
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
            StopCoroutine(_currentDashingCoroutine);
            _isDashing = false;
            if (collision.gameObject.TryGetComponent(out IHitable hitable))
            {
                hitable.RecieveHit(_config.CollisionDamage, gameObject);
            }
        }
    }

    public void Initialize(CharacterConfiguration config)
    {
        _config = config;
        _currentHealsPoints = _config.MaxHealsPoints;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _config.OnAttackSpeedChanged += () => _attackTimer.SetSecondsToFinish(_config.AttackSpeed);

        _attackTimer = new Timer(_config.AttackSpeed);
        _weapon.OnShooted += () => _attackTimer.ResetTimer();
    }

    public void Dash(Vector2 direction)
    {
        _currentDashingCoroutine = StartCoroutine(DashCoroutine(direction));
    }

    public void Move(Vector2 direction)
    {
        if (!_isDashing)
        {
            _rigidbody2D.MovePosition(transform.position + (Vector3)direction * _config.MovementSpeed * Time.fixedDeltaTime);
        }
    }

    public void Attack(Vector3 targetPosition)
    {
        if (_attackTimer.IsFinished)
        {
            transform.LookAt2D(targetPosition);
            _weapon.Attack(targetPosition, _config.Projectile);
        }
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

    public void ApplyDamage(float damage)
    {
        if (damage >= 0)
        {
            _currentHealsPoints = _currentHealsPoints - damage > 0 ? _currentHealsPoints - damage : 0;
            OnHealsChanged?.Invoke();
            if (_currentHealsPoints == 0)
                Die();
        }
        else
        {
            throw new Exception("Incorrect damage");
        }
    }

    public void ApplyForce(Vector2 direction, float force, float duration)
    {
        StartCoroutine(PushingCoroutine(direction, force, duration));
    }

    private IEnumerator BecomeInvulnerable(float time)
    {
        _isInvulnerable = true;
        yield return new WaitForSeconds(time);
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

        _rigidbody2D.velocity = direction * _config.DashDistance / _config.DashDuration;
        yield return new WaitForSeconds(_config.DashDuration);
        _rigidbody2D.velocity = Vector2.zero;
        _isDashing = false;

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

    private void Die()
    {

    }

}
