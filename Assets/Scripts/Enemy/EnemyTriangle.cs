using UnityEngine;
using UnityEngine.AI;

public class EnemyTriangle : Enemy
{
    [SerializeField]
    private RangeWeapon _weapon;
    [SerializeField]
    private float _attackSpeed;
    private Timer _attackTimer;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        _attackTimer = new Timer(_attackSpeed);
        _weapon.OnBeginAttack += () => _attackTimer.ResetTimer();
    }

    private void Update()
    {
        if (_isSpawned)
        {
            _attackTimer.UpdateTick(Time.deltaTime);
            _navMeshAgent.SetDestination(_target.transform.position);
            transform.LookAt2D(_target.transform.position);

            var distanceToTarget = Vector2.Distance(_target.transform.position, transform.position);

            var raycastDirection = -(transform.position - _target.transform.position).normalized;
            var ray = Physics2D.RaycastAll(transform.position, raycastDirection, Mathf.Infinity, _raycastLayers);
            var canSeeTarget = ray[1].collider.TryGetComponent(out Character player);
            if (canSeeTarget)
            {
                if (distanceToTarget < _maxAttackDistance && distanceToTarget > _minAttackDistance)
                {
                    if (_attackTimer.IsFinished)
                        _weapon.Attack(_target.transform.position);
                }
                else if (distanceToTarget > _maxAttackDistance)
                {
                    _navMeshAgent.isStopped = false;
                }
                else if (distanceToTarget <= _minAttackDistance)
                {
                    if (_attackTimer.IsFinished)
                        _weapon.Attack(_target.transform.position);
                    _navMeshAgent.isStopped = true;
                }
            }
            else
            {
                _navMeshAgent.isStopped = false;
            }
        }
    }
}
