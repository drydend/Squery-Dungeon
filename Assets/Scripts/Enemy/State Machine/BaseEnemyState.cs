using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.AI;

public abstract class BaseEnemyState
{
    protected Enemy _controlableEnemy;

    public BaseEnemyState(Enemy controlableEnemy)
    {
        _controlableEnemy = controlableEnemy;
    }

    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract void Update();

}

