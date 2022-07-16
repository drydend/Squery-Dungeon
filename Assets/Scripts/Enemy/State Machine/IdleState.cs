using UnityEngine.AI;

public class IdleState : BaseEnemyState
{
    private NavMeshAgent _navMeshAgent;
    public IdleState(Enemy controlableEnemy, NavMeshAgent navMeshAgent) : base(controlableEnemy)
    {
        _navMeshAgent = navMeshAgent;
    }

    public override void OnEnter()
    {
        _navMeshAgent.isStopped = true;
    }

    public override void OnExit()
    {
        _navMeshAgent.isStopped = false;
    }

    public override void Update()
    {
        _controlableEnemy.transform.LookAt2D(_controlableEnemy.Target.transform.position);
    }
}

