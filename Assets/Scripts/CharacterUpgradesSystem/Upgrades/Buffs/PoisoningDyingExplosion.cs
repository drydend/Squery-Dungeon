using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Special/Poisoning Explosion", fileName = "Poisoning Explosion")]
class PoisoningDyingExplosion : EnemyUpgrade
{
    [SerializeField]
    private EnemyPosoningExplosion _enemyPosoningExplosion;

    public override void ApplyUpgrade(Enemy enemy)
    {
        enemy.AddDyingBehaviour(_enemyPosoningExplosion);        
    }

    public override void RevertUpgrade(Enemy enemy)
    {
    }
}

