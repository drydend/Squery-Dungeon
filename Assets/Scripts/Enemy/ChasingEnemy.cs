using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChasingEnemy : Enemy
{
    [SerializeField]
    private float _collisionDamage;

    public override void Update()
    {
        base.Update();

        if (IsPathAvaible)
        {
            SwitchState<ChasingState>();
        }
        else
        {
            SwitchState<IdleState>();
        }
    }

    private void Start()
    {
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IHitable hitable))
        {
            hitable.RecieveHit(_collisionDamage, gameObject);
        }

        if (collision.gameObject.TryGetComponent(out IEffectable effectable))
        {
            foreach (var effect in _attackEffects)
            {
                if (effectable.CanApplyEffect(effect))
                {
                    effectable.ApplyEffect(effect);
                }
            }
        }
    }

    public override void Initialize(Character target)
    {
        base.Initialize(target);

        _allAvaibleStates = new List<BaseEnemyState>
        {
            new SpawningState(this),
            new IdleState(this,_navMeshAgent),
            new ChasingState(this, _navMeshAgent)
        };

        _currentState = _allAvaibleStates.FirstOrDefault(state => state is SpawningState);
        _currentState.OnEnter();
    }

}

