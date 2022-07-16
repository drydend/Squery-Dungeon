using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[CreateAssetMenu(menuName = "Bosses/AttackPatterns/Shooting")]
public class ShootingAttackPattern : BossAttackPattern
{
    [SerializeField]
    protected RangeWeapon _weaponPrefab;
    [SerializeField]
    protected Projectile _projectilePrefab;
    [SerializeField]
    protected List<Effect> _projectileEffects = new List<Effect>();

    [SerializeField]
    protected float _duration;
    [SerializeField]
    protected int _numberOfProjectiles = 1;
    [SerializeField]
    protected float _angleBetweenProjectiles = 15;
    [SerializeField]
    protected float _attackSpeed;

    protected RangeWeapon _weapon;
    private Coroutine _attackCoroutine;

    public override void Initialize(Boss boss)
    {
        base.Initialize(boss);
        _weapon = Instantiate(_weaponPrefab, boss.transform);
        _weapon.Initialize(boss.gameObject, _projectileEffects, 0, 0);
    }

    public override void Start()
    {
        _attackCoroutine = _boss.StartCoroutine(AttackCoroutine());
    }

    public override void Stop()
    {   
        if(_attackCoroutine == null)
        {
            return;
        }

        _boss.StopCoroutine(_attackCoroutine);
    }

    protected virtual IEnumerator AttackCoroutine()
    {
        Timer attackTimer = new Timer(_attackSpeed);
        attackTimer.OnFinished += () =>
        {
            _weapon.Attack(_boss.Target.transform.position, _projectilePrefab,_boss.Target.transform,
                _numberOfProjectiles, _angleBetweenProjectiles);
            attackTimer.ResetTimer();
        };

        var timeElapsed = 0f;

        while (timeElapsed < _duration)
        {
            _boss.transform.LookAt2D(_boss.Target.transform.position);
            attackTimer.UpdateTick(Time.deltaTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        OnAttackEnded();
    }
}
