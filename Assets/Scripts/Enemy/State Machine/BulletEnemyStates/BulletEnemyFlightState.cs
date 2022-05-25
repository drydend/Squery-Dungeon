using UnityEngine.AI;
using UnityEngine;

public class BulletEnemyFlightState : BaseEnemyState
{
    private Animator _animator;
    private NavMeshAgent _navMeshAgent;
    private Rigidbody2D _rigidbody2D;

    private EnemyBullet _enemyBullet;

    private Vector2 _currentMoveDirection;
    private float _flightSpeed;

    private Vector2 _previousNormal;

    public BulletEnemyFlightState(Enemy controlableEnemy, NavMeshAgent navMeshAgent,
        Rigidbody2D rigidbody2D,float flightSpeed, Animator animator) 
        : base(controlableEnemy)
    {   
        _enemyBullet = (EnemyBullet)controlableEnemy;

        _flightSpeed = flightSpeed;
        _rigidbody2D = rigidbody2D;
        _animator = animator;
        _navMeshAgent = navMeshAgent;
    }

    public override void OnEnter()
    {
        _previousNormal = Vector2.zero;
        _enemyBullet.OnBeginFlying();

        _navMeshAgent.isStopped = true;
        _animator.SetTrigger(EnemyBullet.FlyingAnimationTrigger);
        _navMeshAgent.enabled = false;
        _controlableEnemy.BecomeInvulnarable();

        Vector2 interceptionPoint;
    
        if(_enemyBullet.TryFindIntercectionPointWithTarget(out interceptionPoint)&& _controlableEnemy.CanSeePoint(interceptionPoint))
        {
            _currentMoveDirection = VectorExtensions.ClampMagnitude(interceptionPoint
            - (Vector2)_controlableEnemy.transform.position, 1, 1);
        }
        else
        {
            _currentMoveDirection = VectorExtensions.ClampMagnitude(_enemyBullet.TargetLastVisiblePosition
               - (Vector2)_controlableEnemy.transform.position, 1, 1);
        }

        _rigidbody2D.velocity = _currentMoveDirection * _flightSpeed;
    }

    public override void OnExit()
    {
        _controlableEnemy.BecomeVulnarable();
        _navMeshAgent.enabled = true;
        _navMeshAgent.isStopped = false;
    }

    public override void Update()
    {
        _rigidbody2D.velocity = _currentMoveDirection * _flightSpeed;
    }

    public void HandleCollision(Collision2D collision)
    { 
        var normal = collision.contacts[0].normal;

        if (normal == _previousNormal)
        {
            return;
        }
        _previousNormal = normal;
        
        var newDireciton = Vector2.Reflect(_currentMoveDirection, normal);
        
        if(collision.gameObject.TryGetComponent(out IPushable character))
        {
            character.ApplyForce(_currentMoveDirection, _flightSpeed, 0.2f);
        }

        _currentMoveDirection = newDireciton;
        
        _rigidbody2D.velocity = newDireciton * _flightSpeed;

        EndFlying();
    }

    private void EndFlying()
    {
        _enemyBullet.OnEndFlying();
    }
}