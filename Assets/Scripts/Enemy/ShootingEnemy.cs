using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class ShootingEnemy : Enemy
{
    public const string ShootingAnimationTrigger = "Shoot trigger";

    [SerializeField]
    private RangeWeapon _weapon;
    [SerializeField]
    private float _attackSpeed;
    private Timer _attackTimer;
    private bool _isAttacking = false;
    
    private void Start()
    {
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
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
        _weapon.Attack(_target.transform.position);
    }

    public void OnEndShooting()
    {
        _animator.ResetTrigger(ShootingAnimationTrigger);
        _isAttacking = false;
    }

    public override void Initialize(Character target)
    {
        base.Initialize(target);

        _attackTimer = new Timer(_attackSpeed);
        _weapon.OnShooted += () => _attackTimer.ResetTimer();
        _allAvaibleStates = new List<BaseEnemyState>()
        {
            new SpawningState(this),
            new IdleState(this,_navMeshAgent),
            new ChasingState(this, _navMeshAgent),
            new ShootingState(this, _attackTimer),
            new ShootingAndWalkingState(this, _navMeshAgent, _attackTimer)
        };

        _currentState = _allAvaibleStates.FirstOrDefault(state => state is SpawningState);
        _currentState.OnEnter();
    }


    private void Update()
    {
        _attackTimer.UpdateTick(Time.deltaTime);
        _currentState.Update();

        if (_isSpawned && !_isAttacking)
        {
            if (CanSeeTarget())
            {
                if (DistanceToTarget > MaxAttackDistance)
                {
                    SwitchState<ChasingState>();
                }
                else if (DistanceToTarget < MaxAttackDistance && DistanceToTarget > MinAttakcDistance)
                {
                    SwitchState<ShootingAndWalkingState>();
                }
                else if (DistanceToTarget <= MinAttakcDistance)
                {
                    SwitchState<ShootingState>();
                }
            }
            else
            {
                SwitchState<ChasingState>();
            }
        }

    }
}
