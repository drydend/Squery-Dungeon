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
        _weapon.OnBeginAttack += () => _attackTimer.ResetTimer();
    }

    private void Update()
    {
        _attackTimer.UpdateTick(Time.deltaTime);
    }

    public override void Attack()
    {
        if (_attackTimer.IsFinished)
        {
            _weapon.Attack();
        }
    }

    public override void Attack(Transform target)
    {
        LookAt(target.position);
        Attack();
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

