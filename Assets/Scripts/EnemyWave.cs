using System.Collections.Generic;

class EnemyWave
{
    private readonly List<EnemyController> _enemiesSequence;

    public EnemyWave(List<EnemyController> enemiesSequence)
    {
        _enemiesSequence = enemiesSequence;
    }

    public IEnumerator<EnemyController> GetEnemies()
    {
        for (int i = 0; i < _enemiesSequence.Count; i++)
        {
            yield return _enemiesSequence[i];
        }
    }

}

