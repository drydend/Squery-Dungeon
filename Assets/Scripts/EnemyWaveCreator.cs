﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyWaveCreator : MonoBehaviour
{
    [SerializeField]
    private List<Enemy> _enemiesPrefabs;
    [SerializeField]
    private int _enemyNumberMultiplier;
    [SerializeField]
    private int _enemyDifficultyRange;
    [SerializeField]
    private int _maxWaves = 4;
    [SerializeField]
    private float _maxLerpFactor = 0.7f;

    private EnemyWave GenerateEnemyWave(int minEnemyDifficulty, int maxEnemyDifficulty, int maxNumberOfEnemies, int minNumberOfEnemies)
    {
        var availableEnemies = _enemiesPrefabs.FindAll(enemyController => enemyController.Difficulty >= minEnemyDifficulty
            && enemyController.Difficulty <= maxEnemyDifficulty)
            .ToList();
        var enemySequence = new List<Enemy>();
        int numberOfEnemies = Random.Range(minNumberOfEnemies, maxNumberOfEnemies + 1);
        for (int i = 0; i < numberOfEnemies; i++)
        {
            enemySequence.Add(availableEnemies[Random.Range(0, availableEnemies.Count)]);
        }
        return new EnemyWave(enemySequence);
    }

    public List<EnemyWave> GenerateEnemyWaves(int roomDifficulty, int maxEnemiesInWave, int minEnemiesInWave)
    {
        var waves = new List<EnemyWave>();
        var availableEnemies = _enemiesPrefabs.FindAll(enemyController =>
            enemyController.Difficulty >= roomDifficulty - _enemyDifficultyRange
            && enemyController.Difficulty <= roomDifficulty + _enemyDifficultyRange)
            .ToList();
        
        var lerpFactor = roomDifficulty / 10 >= _maxLerpFactor ? _maxLerpFactor : roomDifficulty / 10;

        var minWaves = (int)Mathf.Lerp(1, _maxWaves, lerpFactor);
        var numberOfWaves = Random.Range(minWaves, _maxWaves + 1);

        for (int waveIndex = 0; waveIndex < numberOfWaves; waveIndex++)
        {
            var enemySequence = new List<Enemy>();

            var minEnemiesInWaveByDifficulty = (int)Mathf.Lerp(minEnemiesInWave, maxEnemiesInWave,lerpFactor );

            int numberOfEnemiesInCurrentWave = Random.Range(minEnemiesInWaveByDifficulty, maxEnemiesInWave);

            for (int i = 0; i < numberOfEnemiesInCurrentWave; i++)
            {
                enemySequence.Add(availableEnemies[Random.Range(0, availableEnemies.Count)]);
            }
            waves.Add(new EnemyWave(enemySequence));
        }
        return waves;
    }
}
