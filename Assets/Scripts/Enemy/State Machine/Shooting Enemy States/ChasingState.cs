using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.AI;

public class ChasingState : BaseEnemyState
{   
    private NavMeshAgent _navMeshAgent;

    public ChasingState(Enemy controlableEnemy, NavMeshAgent navMeshAgent) : base(controlableEnemy)
    {
        _navMeshAgent = navMeshAgent;
    }

    public override void OnEnter()
    {
       _navMeshAgent.isStopped = false;
    }

    public override void OnExit()
    {
        _navMeshAgent.isStopped = true;
    }

    public override void Update()
    {
        _navMeshAgent.SetDestination(_controlableEnemy.Target.transform.position);
        _controlableEnemy.transform.LookAt2D(_controlableEnemy.Target.transform.position);
    }
}

