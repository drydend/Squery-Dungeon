﻿using System;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class ShootingAndWalkingState : ShootingState
{
    private NavMeshAgent _navMeshAgent;
    public ShootingAndWalkingState(Enemy controlableEnemy, NavMeshAgent navMeshAgent, Timer timer)
        : base(controlableEnemy, timer)
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
        base.Update();
        _navMeshAgent.SetDestination(_target.transform.position);
    }
}
