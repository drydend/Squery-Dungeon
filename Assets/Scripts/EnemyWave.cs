using System.Collections;
using System.Collections.Generic;

public class EnemyWave : IEnumerable<Enemy>
{
    public readonly float _averageEnemyDifficulty;
    private readonly List<Enemy> _enemySequence;

    public EnemyWave(List<Enemy> enemySequence)
    {
        _enemySequence = enemySequence;
        int difficultySum = 0;
        foreach (var enemy in enemySequence)
        {
            difficultySum += enemy.Difficulty;
        }
        _averageEnemyDifficulty = difficultySum / enemySequence.Count;
    }
    public IEnumerator<Enemy> GetEnumerator()
    {
        for (int i = 0; i < _enemySequence.Count; i++)
        {
            yield return _enemySequence[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator(); 
    }
}

