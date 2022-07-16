using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class TrialRoomWithEnemies : TrialRoom
{
    [SerializeField]
    private float _minTimeBeforeSpawn = 0f;
    [SerializeField]
    private float _maxTimeBeforeSpawn = 0.5f;
    [SerializeField]
    private int _maxNumberOfEnemiesInWave = 6;
    [SerializeField]
    private int _minNumberOfEnemiesInWave = 2;
    private int _currentNumberOfEnemies;
    private EnemySpawner _enemySpawner;
    private List<EnemyWave> _enemyWaves;

    public int MinEnemiesInWave => _minNumberOfEnemiesInWave;
    public int MaxEnemiesInWave => _maxNumberOfEnemiesInWave;

    public void SetEnemyWaves(List<EnemyWave> enemyWaves)
    {
        _enemyWaves = enemyWaves;
    }

    public void SetEnemySpawner(EnemySpawner enemySpawner)
    {
        _enemySpawner = enemySpawner;
    }

    protected override void StartRoomTrial()
    {
        base.StartRoomTrial();
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        foreach (var enemyWave in _enemyWaves)
        {
            foreach (var enemy in enemyWave)
            {
                SpawnEnemy(enemy);
                yield return new WaitForSeconds(UnityEngine.Random.Range(_minTimeBeforeSpawn, _maxTimeBeforeSpawn));
            }
            yield return new WaitUntil(() => _currentNumberOfEnemies == 0);
        }

        EndRoomTrial();
    }

    private void SpawnEnemy(Enemy enemyPrefab)
    {
        _currentNumberOfEnemies++;
        var spawnedEnemy = _enemySpawner.SpawnEnemy(this, enemyPrefab);
        spawnedEnemy.OnDie += () => _currentNumberOfEnemies--;
    }
}

