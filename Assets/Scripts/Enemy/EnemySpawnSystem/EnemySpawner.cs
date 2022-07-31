using UnityEngine;
using System;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    private List<Enemy> _spawnedEnemies = new List<Enemy>();
    private Character _targetForEnemiesByDefault => _player.CurrentCharacter;

    public List<Enemy> SpawnedEnemies => _spawnedEnemies;

    public event Action<Enemy> OnEnemySpawned;

    public Enemy SpawnEnemy(TrialRoom room, Enemy enemyPrefab)
    { 
        var spawnedEnemy = Instantiate(enemyPrefab, room.GetRandomPositionInRoom(), enemyPrefab.transform.rotation);
        spawnedEnemy.Initialize(_targetForEnemiesByDefault);

        _spawnedEnemies.Add(spawnedEnemy);
        spawnedEnemy.OnDied += () => _spawnedEnemies.Remove(spawnedEnemy);
        OnEnemySpawned?.Invoke(spawnedEnemy);
        return spawnedEnemy;
    }
}
