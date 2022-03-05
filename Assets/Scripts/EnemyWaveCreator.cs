using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyWaveCreator : MonoBehaviour
{
    [SerializeField]
    private List<EnemyController> _enemiesPrefabs;
    [SerializeField]
    private int _waveMinEnemyNumber = 1;
    [SerializeField]
    private int _waveMaxEnemyNumber = 8;

    public EnemyWave GenerateEnemyWave(int minEnemyDifficulty, int maxEnemyDifficulty, int maxNumberOfEnemies, int minNumberOfEnemies)
    {
        var availableEnemies = _enemiesPrefabs.FindAll(enemyController => enemyController.Difficulty >= minEnemyDifficulty
            && enemyController.Difficulty <= maxEnemyDifficulty)
            .ToList();
        var enemySequence = new List<EnemyController>();
        int numberOfEnemies = Random.Range(minNumberOfEnemies, maxNumberOfEnemies + 1);
        for (int i = 0; i < numberOfEnemies; i++)
        {
            enemySequence.Add(availableEnemies[Random.Range(0, availableEnemies.Count)]);
        }
        return new EnemyWave(enemySequence);
    }
}
