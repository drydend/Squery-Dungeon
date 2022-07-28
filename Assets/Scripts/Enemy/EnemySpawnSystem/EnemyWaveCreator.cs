using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyWaveCreator : MonoBehaviour
{
    [SerializeField]
    private List<Enemy> _enemiesPrefabs;
    [SerializeField]
    private float _enemyNumberMultiplier;
    [SerializeField]
    private int _enemyMinDifficultyRange;
    [SerializeField]
    private int _enemyMaxDifficultyRange;
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
        var scaledMaxEnemiesInWave = Mathf.RoundToInt(maxEnemiesInWave * _enemyNumberMultiplier);
        var scaledMinEnemiesInWave = Mathf.RoundToInt(minEnemiesInWave * _enemyNumberMultiplier);

        var waves = new List<EnemyWave>();
        var availableEnemies = _enemiesPrefabs.FindAll(enemyController =>
            enemyController.Difficulty >= roomDifficulty - _enemyMinDifficultyRange
            && enemyController.Difficulty <= roomDifficulty + _enemyMaxDifficultyRange)
            .ToList();
        
        var lerpFactor = roomDifficulty / 10 >= _maxLerpFactor ? _maxLerpFactor : roomDifficulty / 10;

        var minWaves = (int)Mathf.Lerp(1, _maxWaves, lerpFactor);
        var numberOfWaves = Random.Range(minWaves, _maxWaves + 1);

        for (int waveIndex = 0; waveIndex < numberOfWaves; waveIndex++)
        {
            var enemySequence = new List<Enemy>();

            int averageEnemiesInWave = (scaledMinEnemiesInWave + scaledMaxEnemiesInWave) / 2;
            
            var minEnemiesInWaveByDifficulty = (int)Mathf.Lerp(scaledMinEnemiesInWave, averageEnemiesInWave, lerpFactor);
            var maxEnemiesInWaveByDifficulty = (int)Mathf.Lerp(averageEnemiesInWave, scaledMaxEnemiesInWave, lerpFactor);

            int numberOfEnemiesInCurrentWave = Random.Range(minEnemiesInWaveByDifficulty, scaledMaxEnemiesInWave);

            for (int i = 0; i < numberOfEnemiesInCurrentWave; i++)
            {
                enemySequence.Add(availableEnemies[Random.Range(0, availableEnemies.Count)]);
            }
            waves.Add(new EnemyWave(enemySequence));
        }
        return waves;
    }
}
