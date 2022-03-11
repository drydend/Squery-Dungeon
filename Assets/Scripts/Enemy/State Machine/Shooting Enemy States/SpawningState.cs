using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.AI;


public class SpawningState : BaseEnemyState
{
    public SpawningState(Enemy controlableEnemy) : base(controlableEnemy) { }

    public override void OnEnter()
    {
        _controlableEnemy.OnSpawn();
    }

    public override void OnExit()
    {
      
    }

    public override void Update()
    {

    }
}

