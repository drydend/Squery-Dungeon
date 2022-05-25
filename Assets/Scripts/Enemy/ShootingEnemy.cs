using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Animator))]
public class ShootingEnemy : Enemy
{
    public const string ShootingAnimationTrigger = "Shoot trigger";
    public const string HitAnimationTrigger = "Hit trigger";

    [SerializeField]
    private Projectile _projectile;
    [SerializeField]
    private RangeWeapon _weapon;
    [SerializeField]

    private float _attackSpeed;
    private Timer _attackTimer;
    private bool _isAttacking = false;

    private Animator _animator;

    private void Start()
    {
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
    }

    private void Update()
    {
        _attackTimer.UpdateTick(Time.deltaTime);
        _currentState.Update();

        if (!_isSpawned || _isAttacking)
        {
            return;
        }

        if (CanSeeTarget())
        {
            if (DistanceToTarget > MaxAttackDistance)
            {
                SwitchState<ChasingState>();
            }
            else if (DistanceToTarget < MaxAttackDistance && DistanceToTarget > MinAttakcDistance)
            {
                SwitchState<ShootingEnemyShootingAndWalkingState>();
            }
            else if (DistanceToTarget <= MinAttakcDistance)
            {
                SwitchState<ShootingEnemyShootingState>();
            }
        }
        else
        {
            SwitchState<ChasingState>();
        }

    }

    public override void Attack()
    {
        _animator.SetTrigger(ShootingAnimationTrigger);
    }

    public void OnBeginShooting()
    {
        _isAttacking = true;
    }

    public void Shoot()
    {
        _weapon.Attack(_target.transform.position, _projectile);
    }

    public void OnEndShooting()
    {
        _animator.ResetTrigger(ShootingAnimationTrigger);
        _isAttacking = false;
    }

    public override void Initialize(Character target)
    {
        base.Initialize(target);

        _animator = GetComponent<Animator>();
        _attackTimer = new Timer(_attackSpeed);
        _weapon.OnShooted += () => _attackTimer.ResetTimer();
        _allAvaibleStates = new List<BaseEnemyState>()
        {
            new SpawningState(this),
            new IdleState(this,_navMeshAgent),
            new ChasingState(this, _navMeshAgent),
            new ShootingEnemyShootingState(this, _attackTimer),
            new ShootingEnemyShootingAndWalkingState(this, _navMeshAgent, _attackTimer)
        };

        _currentState = _allAvaibleStates.FirstOrDefault(state => state is SpawningState);
        _currentState.OnEnter();
    }

    protected override void OnRecieveHit()
    {
        _animator.SetTrigger(HitAnimationTrigger);
    }
}
