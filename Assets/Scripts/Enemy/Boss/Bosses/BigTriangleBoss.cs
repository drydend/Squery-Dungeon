using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class BigTriangleBoss : Boss
{
    [SerializeField]
    private float _timeBetweenStages = 1f;
    [SerializeField]
    private List<BossShell> _bossShells;
    private BossShell _currentShell;

    private PolygonCollider2D _polygonCollider2D;

    private void Start()
    {
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
    }

    private void Update()
    {
        if (!_isSpawned)
        {
            return;
        }

        for (int i = 0; i < _appliedEffects.Count; i++)
        {
            _appliedEffects[i].Update();
        }

        _currentStage.Update();
    }

    public override void Initialize(Character target)
    {
        base.Initialize(target);

        _leftStages = new List<BossStage>(_allStages);

        _currentStage = _leftStages.OrderByDescending(stage => stage.HealsThresholdPercentToEnter).First();
        _leftStages.Remove(_currentStage);

        _bossShells.OrderByDescending(shell => shell.ShellOrder);
        _currentShell = _bossShells.First();
        _bossShells.Remove(_currentShell);

        _polygonCollider2D = (PolygonCollider2D)gameObject.AddComponent(typeof(PolygonCollider2D));

        UpdateCollider();

        _polygonCollider2D.enabled = true;

        OnHealsChanged += OnTakeDamage;
        OnDied += () => _currentShell.DestroyShell();
    }

    protected override void OnSpawned()
    {
        base.OnSpawned();
        _currentStage.OnEnter();
    }

    private void OnTakeDamage()
    {
        if (_heals == 0)
        {
            return;
        }

        _currentShell.OnHit();

        if(_leftStages.Count == 0)
        {
            return;
        }

        if (_heals / _maxHeals < _leftStages.First().HealsThresholdPercentToEnter)
        {
            StartCoroutine(SetNextStage(_timeBetweenStages));

            if (_bossShells.Count > 0)
            {
                var currentShell = _bossShells.First();
                _bossShells.Remove(currentShell);
                _currentShell.DestroyShell();
                _currentShell = currentShell;

                UpdateCollider();
            }
        }
    }

    private void UpdateCollider()
    {
        Vector2[] scaledPoints = new Vector2[_currentShell.Collider2D.points.Length];
        for (int i = 0; i < _currentShell.Collider2D.points.Length; i++)
        {
            scaledPoints[i] = _currentShell.Collider2D.points[i] * _currentShell.transform.localScale;
        }

        _polygonCollider2D.points = scaledPoints;
    }

    private IEnumerator SetNextStage(float delay)
    {
        _currentStage.OnExit();

        StopFollowingPlayer();
        IsInvulnerable = true;

        var nextStage = _leftStages.First();
        _leftStages.Remove(nextStage);
        _currentStage = nextStage;

        yield return new WaitForSeconds(delay);

        _currentStage.OnEnter();

        IsInvulnerable = false;
        FollowPlayer();
    }
}

