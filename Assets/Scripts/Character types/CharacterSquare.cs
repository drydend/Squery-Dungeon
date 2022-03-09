using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSquare : Character
{
    [SerializeField]
    private float _attackSpeedPerSecond = 1f;
    [SerializeField]
    private float _attackAnimationDuration = 0.5f;
    [SerializeField]
    private AnimationCurve _attackAnimation;
    [SerializeField]
    private Timer _attackTimer;
    [SerializeField]
    private MeleeWeapon _weapon;

    private Vector3 _scaleByDefault;

    public override void UseAbility()
    {

    }

    public override void Attack(Vector3 targetPosition)
    {
        if (_attackTimer.IsFinished)
        {
            _attackTimer.ResetTimer();
            StartCoroutine(AttackAnimation());
        }
    }

    public override void Initialize()
    {
        CharacterType = CharacterType.Square;
        base.Initialize();
    }

    private void Awake()
    {
        _scaleByDefault = transform.localScale;
        _attackTimer = new Timer(_attackSpeedPerSecond + _attackAnimationDuration);
    }

    private void Update()
    {
        _attackTimer.UpdateTick(Time.deltaTime);
    }


    private void OnDisable()
    {
        transform.localScale = _scaleByDefault;
    }

    private IEnumerator AttackAnimation()
    {
        var startScale = transform.localScale;
        float timeFromStart = 0f;
        bool attacked = false;

        while (timeFromStart <= 1)
        {
            transform.localScale = startScale * (1 + _attackAnimation.Evaluate(timeFromStart / _attackAnimationDuration));
            timeFromStart += Time.deltaTime / _attackAnimationDuration;
            if (!attacked && timeFromStart >= 0.7f)
            {
                attacked = true;
                _weapon.Attack();
            }
            yield return null;
        }
        transform.localScale = startScale;


    }

    public override void IncreaseStat(StatType statType, float value)
    {
        throw new System.NotImplementedException();
    }

    public override void DecreaseStat(StatType statType, float value)
    {
        throw new System.NotImplementedException();
    }
}

