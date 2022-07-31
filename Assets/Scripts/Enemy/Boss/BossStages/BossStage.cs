using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Bosses/BigTriangle/Stage", fileName = "Boss Stage")]
public class BossStage : ScriptableObject
{
    [SerializeField]
    protected List<BossAttackPattern> _attackPatterns;
    [SerializeField]
    protected float _timeBetweenAttacks;

    protected Boss _boss;
    protected List<BossAttackPattern> _avaibleAttackPatterns;
    protected BossAttackPattern _currentAttackPattern;
    private Coroutine _currentAttackCoroutine;

    [SerializeField] [Range(0,1f)]
    private float _percentageHealthThresholdToEnter;

    public float HealsThresholdPercentToEnter => _percentageHealthThresholdToEnter;

    public virtual void Initialize(Boss boss)
    {
        _boss = boss;
    }

    public virtual void OnEnter()
    {   
        if(_attackPatterns.Count ==0)
        {
            return;
        }

        _avaibleAttackPatterns = new List<BossAttackPattern>(_attackPatterns);
        foreach (var attackPattern in _attackPatterns)
        {
            attackPattern.Initialize(_boss);
            attackPattern.OnEnded += StartNextAttack;
        }

        _currentAttackPattern = GetRandomAttackPattern();
        _currentAttackCoroutine = _boss.StartCoroutine(Attack(_currentAttackPattern));
    }

    public virtual void OnExit()
    {   
        _boss.StopCoroutine(_currentAttackCoroutine);
        _currentAttackPattern.Stop();
    }

    public virtual void Update()
    {

    }

    private IEnumerator Attack(BossAttackPattern attackPattern)
    {
        _boss.FollowPlayer();
        yield return new WaitForSeconds(_timeBetweenAttacks);
        attackPattern.Start();
        _boss.StopFollowingPlayer();
    }

    private void StartNextAttack()
    {
        _currentAttackPattern.Stop();
        _currentAttackPattern = GetRandomAttackPattern();
        _boss.StartCoroutine(Attack(_currentAttackPattern));
    }

    private BossAttackPattern GetRandomAttackPattern()
    {
        BossAttackPattern result;

        if (_avaibleAttackPatterns.Count > 1)
        {
            result = _avaibleAttackPatterns.GetRandomValue();
        }
        else
        {
            result = _avaibleAttackPatterns.First();
            _avaibleAttackPatterns.Remove(result);
            _avaibleAttackPatterns.AddRange(_attackPatterns);
        }

        _avaibleAttackPatterns.Remove(result);
        return result;
    }
}

