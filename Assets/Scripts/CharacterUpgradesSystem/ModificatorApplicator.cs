using UnityEngine;
using System.Collections.Generic;

public class ModificatorApplicator : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    private EnemySpawner _enemySpawner;

    private List<EnemyUpgrade> _enemiesUpgrades = new List<EnemyUpgrade>(0);
    private List<PlayerEnemyUpgrade> _playerEnemyUpgrades = new List<PlayerEnemyUpgrade>(0);

    private void Awake()
    {
        _enemySpawner.OnEnemySpawned += (enemy) =>
        {
            foreach (var upgrade in _enemiesUpgrades)
            {
                upgrade.ApplyUpgrade(enemy);
            }

            foreach (var upgrade in _playerEnemyUpgrades)
            {
                upgrade.ApplyUpgrade(_player, enemy);
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
            else if (upgrade.GetType().IsSubclassOf(typeof(PlayerEnemyUpgrade)))
            {
                ApplyPlayerEnemyUpgrade((PlayerEnemyUpgrade)upgrade);
            }
        }
    }

    private void ApplyPlayerUpgrade(PlayerUpgrade upgrade)
    {
        upgrade.ApplyUpgrade(_player);
    }

    private void ApplyEnemyUpgrade(EnemyUpgrade upgrade)
    {
        _enemiesUpgrades.Add(upgrade);

        foreach (var enemy in _enemySpawner.SpawnedEnemies)
        {
            upgrade.ApplyUpgrade(enemy);
        }
    }

    private void ApplyPlayerEnemyUpgrade(PlayerEnemyUpgrade upgrade)
    {
        var currentUpgrade = _playerEnemyUpgrades.Find(upg => upg.GetType() == upgrade.GetType());

        if (currentUpgrade != null)
        {
            foreach (var enemy in _enemySpawner.SpawnedEnemies)
            {
                currentUpgrade.RevertUpgrade(_player, enemy);
            }
        }

        _playerEnemyUpgrades.Add(upgrade);

        foreach (var enemy in _enemySpawner.SpawnedEnemies)
        {
            upgrade.ApplyUpgrade(_player, enemy);
        }
    }
}

