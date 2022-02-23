using UnityEngine;
using UnityEngine.AI;

public class EnemyTriangle : EnemyController
{ 
    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
    }

    private void Update()
    {
        _navMeshAgent.SetDestination(_target.CharacterTransform.position);
        _controllableCharacter.LookAt(_target.CharacterTransform.position);
        
        var distanceToTarget = Vector2.Distance(_target.CharacterTransform.position, _controllableCharacter.transform.position);

        var raycastDirection = -(_controllableCharacter.transform.position - _target.CharacterTransform.position).normalized;
        var ray = Physics2D.Raycast(_controllableCharacter.transform.position, raycastDirection,Mathf.Infinity, _raycastLayers);
        var canSeeTarget = ray.collider.TryGetComponent(out Character player);

        if (canSeeTarget)
        {
            if (distanceToTarget < _maxAttackDistance && distanceToTarget > _minAttackDistance)
            {
                _controllableCharacter.Attack(_target.CharacterTransform);
            }
            else if (distanceToTarget > _maxAttackDistance)
            {
                _navMeshAgent.isStopped = false;
            }
            else if(distanceToTarget <= _minAttackDistance)
            {
                _controllableCharacter.Attack(_target.CharacterTransform);
                _navMeshAgent.isStopped = true;
            }
        }
        else
        {
            _navMeshAgent.isStopped = false;
        }
    }
}
