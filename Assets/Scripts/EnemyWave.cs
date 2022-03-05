using System.Collections.Generic;

public class EnemyWave
{
    public readonly float _averageEnemyDifficulty;
    private readonly List<EnemyController> _enemySequence;

    public EnemyWave(List<EnemyController> enemySequence)
    {
        _enemySequence = enemySequence;
        int difficultySum = 0;
        foreach (var enemy in enemySequence)
        {
            difficultySum += enemy.Difficulty;
        }
        _averageEnemyDifficulty = difficultySum / enemySequence.Count;
    }
    public IEnumerator<EnemyController> GetEnemies()
    {
        for (int i = 0; i < _enemySequence.Count; i++)
        {
            yield return _enemySequence[i];
        }
    }

}

