using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTriangle : Character
{
    [SerializeField]
    private RangeWeapon _weapon;
    [SerializeField]
    private float _attackSpeedPerSecond = 1f;
    private Timer _attackTimer;

    private void Awake()
    {
        _attackTimer = new Timer(_attackSpeedPerSecond);
        _weapon.OnShooted += () => _attackTimer.ResetTimer();
    }

    private void Update()
    {
        _attackTimer.UpdateTick(Time.deltaTime);
    }

    public override void Attack(Vector3 targetPosition)
    {
        if (_attackTimer.IsFinished)
        {
            transform.LookAt2D(targetPosition);
            _weapon.Attack(targetPosition);
        }
    }

    public override void UseAbility()
    {

    }

    public override void Initialize()
    {
        CharacterType = CharacterType.Triangle;
        base.Initialize();
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

