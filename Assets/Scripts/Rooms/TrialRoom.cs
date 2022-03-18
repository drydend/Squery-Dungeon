using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrialRoom : Room
{
    [SerializeField]
    private Transform _enemySpawnPoint;
    [SerializeField]
    private float _enemySpawnRange;
    [SerializeField]
    private float _minTimeBeforeSpawn = 0f;
    [SerializeField]
    private float _maxTimeBeforeSpawn = 0.2f;
    [SerializeField]
    private int _maxNumberOfEnemiesInWave = 6;
    [SerializeField]
    private int _minNumberOfEnemiesInWave = 2;
    private int _currentNumberOfEnemies;
    [SerializeField]
    private EnemySpawner _enemySpawner;

    private List<EnemyWave> _enemyWaves;
    private RoomState _roomState = RoomState.NotFinished;

    public int MinEnemiesInWave => _minNumberOfEnemiesInWave;
    public int MaxEnemiesInWave => _maxNumberOfEnemiesInWave;
    public event Action OnWaveFinished;

    public void SetEnemyWaves(List<EnemyWave> enemyWaves)
    {
        _enemyWaves = enemyWaves;
    }
    
    public void SetEnemySpawner(EnemySpawner enemySpawner)
    {
        _enemySpawner = enemySpawner;
    }

    private void StartRoomTrial()
    {
        _upperEntrance.Close();
        _lowerEntrance.Close();
        _rightEntrance.Close();
        _leftEntrance.Close();
        StartCoroutine(SpawnWaves());
    }

    private void EndRoomTrial()
    {
        _upperEntrance.Open();
        _lowerEntrance.Open();
        _rightEntrance.Open();
        _leftEntrance.Open();
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

    public Vector3 GetRandomPositionInRoom()
    {
        var randomPoint = _enemySpawnPoint.position + (Vector3)UnityEngine.Random.insideUnitCircle * _enemySpawnRange;
        NavMeshHit navMeshHit;
        if (NavMesh.SamplePosition(randomPoint, out navMeshHit, 5f, NavMesh.AllAreas))
        {
            var enemyPosition = navMeshHit.position;
            return enemyPosition;
        }
        Debug.LogException(new Exception("Can`t find random place on navMesh"));
        return Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Character character) && _roomState == RoomState.NotFinished)
        {
            _roomState = RoomState.InProcess;
            StartRoomTrial();
        }
    }
}
