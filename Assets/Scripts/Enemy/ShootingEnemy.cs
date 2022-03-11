using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class ShootingEnemy : Enemy
{
    [SerializeField]
    private RangeWeapon _weapon;
    [SerializeField]
    private float _attackSpeed;
    private Timer _attackTimer;

    private void Start()
    {
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
    }

    public override void Initialize(Character target)
    {
        base.Initialize(target);

        _attackTimer = new Timer(_attackSpeed);
        _weapon.OnBeginAttack += () => _attackTimer.ResetTimer();
        _allAvaibleStates = new List<BaseEnemyState>()
        {
            new SpawningState(this),
            new IdleState(this,_navMeshAgent),
            new ChasingState(this, _navMeshAgent),
            new ShootingState(this, _weapon, _attackTimer),
            new ShootingAndWalkingState(this, _navMeshAgent, _weapon, _attackTimer)
        };

        _currentState = _allAvaibleStates.FirstOrDefault(state => state is SpawningState);
        _currentState.OnEnter();
    }


    private void Update()
    {
        _attackTimer.UpdateTick(Time.deltaTime);
        _currentState.Update();

        if (_isSpawned)
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
