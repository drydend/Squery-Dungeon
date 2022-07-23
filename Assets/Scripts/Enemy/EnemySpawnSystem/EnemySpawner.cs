using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    private Character _targetForEnemiesByDefault => _player.CurrentCharacter;

    public event Action<Enemy> OnEnemySpawned;

    public Enemy SpawnEnemy(TrialRoom room, Enemy enemyPrefab)
    { 
        var spawnedEnemy = Instantiate(enemyPrefab, room.GetRandomPositionInRoom(), enemyPrefab.transform.rotation);
        spawnedEnemy.Initialize(_targetForEnemiesByDefault);
        OnEnemySpawned?.Invoke(spawnedEnemy);
        return spawnedEnemy;
    }
}
