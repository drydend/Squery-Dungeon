using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour, IHitable, IPushable
{
    [SerializeField]
    private float _dashDistance = 5f;
    [SerializeField]
    private float _dashDuration = 0.2f;
    private bool _isDashing;
    private Coroutine _currentDashingCoroutine;
    [SerializeField]
    private float _dashingPushForce = 8;
    private float _defaultPushForce = 5;
    [SerializeField]
    private float _collisionDamage = 1;

    [SerializeField]
    private float _movementSpeed;

    [SerializeField]
    private float _maxHealsPoints;
    private float _currentHealsPoints;
    private bool _isInvulnerable = false;
    [SerializeField]
    private float _invulnerabilityAfterHitDuration = 1f;
    [SerializeField]
    private AnimationCurve _colorAlfaOnInvulnerable;

    [SerializeField]
    private RangeWeapon _weapon;
    [SerializeField]
    private float _attackSpeed = 1f;
    private Timer _attackTimer;

    [SerializeField]
    private float _pushingDuration = 0.2f;
    private bool _isPushed;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;

    public float Speed => _movementSpeed;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _attackTimer = new Timer(_attackSpeed);
        _weapon.OnShooted += () => _attackTimer.ResetTimer();
    }

    private void Update()
    {
        _attackTimer.UpdateTick(Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isDashing)
        {
            if (collision.gameObject.TryGetComponent(out IPushable pushable))
            {
                var forceDirection = Vector2.ClampMagnitude(collision.transform.position - transform.position, 1);
                pushable.ApplyForce(forceDirection, _dashingPushForce);
            }
        }

        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            var forceDirection = Vector2.ClampMagnitude(collision.transform.position - transform.position, 1);
            enemy.ApplyForce(forceDirection, _defaultPushForce);
            enemy.RecieveHit(_collisionDamage, gameObject);
        }
    }

    public void Dash(Vector2 direction)
    {
        _currentDashingCoroutine = StartCoroutine(DashCoroutine(direction));
    }

    public void Move(Vector2 direction)
    {
        if (!_isDashing && !_isPushed)
        {
            _rigidbody2D.MovePosition(transform.position + (Vector3)direction * _movementSpeed * Time.fixedDeltaTime);
        }
    }

    public void IncreaseStat(StatType statType, float value)
    {

    }

    public void DecreaseStat(StatType statType, float value)
    {

    }

    public void Attack(Vector3 targetPosition)
    {
        if (_attackTimer.IsFinished)
        {
            transform.LookAt2D(targetPosition);
            _weapon.Attack(targetPosition);
        }
    }

    public void RecieveHit(float damage, GameObject sender)
    {
        if (!_isDashing && !_isInvulnerable)
        {
            StartCoroutine(BecomeInvulnerable(_invulnerabilityAfterHitDuration));
            StartCoroutine(TookDamageAnimation(_invulnerabilityAfterHitDuration));
            ApplyDamage(damage);
        }
    }

    public void ApplyDamage(float damage)
    {
        if (damage >= 0)
        {
            _currentHealsPoints -= damage;
        }
        else
        {
            throw new Exception("Incorrect damage");
        }
    }


    public void ApplyForce(Vector2 direction, float force)
    {
        StartCoroutine(PushingCoroutine(direction, force));
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
        if (!_isDashing)
        {
            _isDashing = true;

            _rigidbody2D.velocity = direction * _dashDistance / _dashDuration;
            yield return new WaitForSeconds(_dashDuration);
            _rigidbody2D.velocity = Vector2.zero;

            _isDashing = false;
        }
    }

    private IEnumerator PushingCoroutine(Vector2 direction, float force)
    {
        _isPushed = true;
        var initialForce = force;
        while (force > 0)
        {
            _rigidbody2D.velocity = direction * force;
            force -= Time.deltaTime / _pushingDuration * initialForce;
            yield return null;
        }

        _isPushed = false;
        _rigidbody2D.velocity = Vector2.zero;
    }


    private void Die()
    {

    }
}
