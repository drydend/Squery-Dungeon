using UnityEngine;

public abstract class EnemyDyingBehaviour : ScriptableObject
{
    protected Enemy _enemy;

    public virtual void Initialize(Enemy enemy)
    {
        _enemy = enemy;
        _enemy.OnDied += OnEnemyDied;
    }

    public EnemyDyingBehaviour Clone()
    {
        return (EnemyDyingBehaviour)MemberwiseClone();
    }

    protected virtual void OnEnemyDied()
    {

    }
}

