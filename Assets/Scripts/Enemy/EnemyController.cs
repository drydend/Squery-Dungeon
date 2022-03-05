using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour, IDamageable
{   
    [SerializeField]
    protected Character _controllableCharacter;
    [SerializeField]
    protected Player _target;
    [SerializeField]
    protected float _maxHealsPoints;
    protected float _currentHealsPoints;
    [SerializeField]
    protected float _maxAttackDistance;
    [SerializeField]
    protected float _minAttackDistance;
    [SerializeField] [Range(1 , 10)]
    protected int _enemyDifficulty;
    [SerializeField]
    protected LayerMask _raycastLayers;
    protected NavMeshAgent _navMeshAgent;

    public int Difficulty => _enemyDifficulty;
    public Action<EnemyController> OnDie;

    public virtual void RecieveDamage(float damage, GameObject sender)
    {
        if (damage >= 0)
        {
            _currentHealsPoints -= damage;
            if (_currentHealsPoints <= 0)
                Die();
        }
        else
        {
            throw new Exception("Incorrect damage");
        }
    }
    
    private void Awake()
    {
        _currentHealsPoints = _maxHealsPoints;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateRotation = false;
    }

    private protected virtual void Die() 
    {
        OnDie?.Invoke(this);
        Destroy(gameObject);
    }

    private void Start()
    {
        _controllableCharacter.Initialize();
    }
}
