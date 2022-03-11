using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    }

    public override void Update()
    {
        
    }
}

