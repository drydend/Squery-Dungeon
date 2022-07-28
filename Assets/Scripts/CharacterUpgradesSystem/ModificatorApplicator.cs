using UnityEngine;
using System.Collections.Generic;

public class ModificatorApplicator : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    private EnemySpawner _enemySpawner;

    private List<EnemyUpgrade> _enemiesUpgrades = new List<EnemyUpgrade>(0);
    
    private void Awake()
    {
        _enemySpawner.OnEnemySpawned += (enemy) =>
        {
            foreach (var upgrade in _enemiesUpgrades)
            {
                upgrade.ApplyUpgrade(enemy);
            }
        };
    }

    public void ApplyModificator(Modificator modificator)
    {
        foreach (var upgrade in modificator.Upgrades)
        {
            if (upgrade.GetType().IsSubclassOf(typeof(PlayerUpgrade)))
            {
                ApplyPlayerUpgrade((PlayerUpgrade)upgrade);
            }
            else if (upgrade.GetType().IsSubclassOf(typeof(EnemyUpgrade)))
            {
                ApplyEnemyUpgrade((EnemyUpgrade)upgrade);
            }
        }
    }

    private void ApplyPlayerUpgrade(PlayerUpgrade upgrade)
    {
        upgrade.ApplyUpgrade(_player);
    }
    
    public void ApplyEnemyUpgrade(EnemyUpgrade upgrade)
    {
        _enemiesUpgrades.Add(upgrade);
    }
}

