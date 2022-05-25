using UnityEngine;
using UnityEngine.AI;

public class BulletEnemyStopingState : BaseEnemyState
{
    private NavMeshAgent _navMeshAgent;
    private Rigidbody2D _rigidbody2D;
    private AnimationCurve _stopCurve;
    private Animator _animator;
    private float _stopTime;
    private float _stopingAnimationDuration;

    private Vector2 _previousNormal;
    private Vector2 _startVelocity;
    private float _previousAnimatorSpeed;
    private Timer _stopTimer;

    private EnemyBullet _enemyBullet;

    public BulletEnemyStopingState(Enemy controlableEnemy, NavMeshAgent navMeshAgent, Rigidbody2D rigidbody2D,
        float stopTime, AnimationCurve stopCurve, Animator animator,
        float stopingAnimationDuration) : base(controlableEnemy)
    {
        _enemyBullet = (EnemyBullet)controlableEnemy;

        _navMeshAgent = navMeshAgent;
        _animator = animator;
        _stopingAnimationDuration = stopingAnimationDuration;
        _rigidbody2D = rigidbody2D;
        _stopTime = stopTime;
        _stopCurve = stopCurve;

        _stopTimer = new Timer(_stopTime);
        _stopTimer.OnFinished += _enemyBullet.OnStoped;
    }

    public override void OnEnter()
    {
        _startVelocity =_rigidbody2D.velocity;
        _enemyBullet.OnBeginStoping();
        _previousAnimatorSpeed = _animator.speed;
        _animator.speed = _stopingAnimationDuration / _stopTime;
        _animator.SetTrigger(EnemyBullet.StopingAnimationTrigger);
        _stopTimer.ResetTimer();
        _navMeshAgent.isStopped = true;
        _navMeshAgent.enabled = false;
    }

    public override void OnExit()
    {
        _navMeshAgent.enabled = true;
        _animator.speed = _previousAnimatorSpeed;
        _rigidbody2D.velocity = Vector2.zero;
        _navMeshAgent.isStopped = false;
    }

    public override void Update()
    {
        _controlableEnemy.Transform.LookAt2D(_controlableEnemy.Target.transform.position);
        _rigidbody2D.velocity = _startVelocity * _stopCurve.Evaluate(_stopTimer.SecondsPassed / _stopTime);
        _stopTimer.UpdateTick(Time.deltaTime);
    }
}