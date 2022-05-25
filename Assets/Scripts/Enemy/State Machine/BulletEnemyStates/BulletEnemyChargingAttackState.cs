using UnityEngine.AI;
using UnityEngine;

public class BulletEnemyChargingAttackState : BaseEnemyState
{
    private Animator _animator;
    private NavMeshAgent _navMeshAgent;
    private float _chargingAttackAnimationDuration;
    private float _chargingDuration;

    private EnemyBullet _enemyBullet;

    private float _previousAnimatorSpeed;
    private Timer _animationTimer;

    public BulletEnemyChargingAttackState(Enemy controlableEnemy, NavMeshAgent navMeshAgent, 
        Animator animator, float chargingAnimationDuration, float chargingDuration) : base(controlableEnemy)
    {   
        _enemyBullet = (EnemyBullet)controlableEnemy;
        _chargingDuration = chargingDuration;
        _animator = animator;
        _navMeshAgent = navMeshAgent;
        _chargingAttackAnimationDuration = chargingAnimationDuration;
        
        _animationTimer = new Timer(_chargingDuration);
        _animationTimer.OnFinished += OnAnimationEnd;
    }

    private void OnAnimationEnd()
    {
        _enemyBullet.OnEndAttackCharging();
    }

    public override void OnEnter()
    {
        _previousAnimatorSpeed = _animator.speed;
        _animator.speed = _chargingAttackAnimationDuration / _chargingDuration;
        _animationTimer.ResetTimer();
        _navMeshAgent.isStopped = true;
        _enemyBullet.OnBeginAttackcharging();
        _animator.SetTrigger(EnemyBullet.AttackChargingAnimationTrigger);
    }

    public override void OnExit()
    {
        _animator.speed = _previousAnimatorSpeed;
        _navMeshAgent.isStopped = false;
    }

    public override void Update()
    {
        _animationTimer.UpdateTick(Time.deltaTime);
        _controlableEnemy.Transform.LookAt2D(_controlableEnemy.Target.transform.position);
    }
}

