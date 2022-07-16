using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[CreateAssetMenu(menuName = "Bosses/AttackPatterns/ChasingAndShooting")]
public class ChasingAndShootingAttackPattern : ShootingAttackPattern
{
    [SerializeField]
    private float _movementSpeed;

    public override void Start()
    {
        base.Start();
        _boss.ChaseTarget(_movementSpeed);
    }

    public override void Stop()
    {
        _boss.StopChasingTarget();
        base.Stop();
    }
}
