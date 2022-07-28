using UnityEngine;

public class ExpDealer : MonoBehaviour, IRewardHandler
{
    [SerializeField]
    private Player _player;

    [SerializeField]
    private EnemySpawner _enemySpawner;
    [SerializeField]
    private int _expForCompleatingRoom;

    private void Awake()
    {
        _enemySpawner.OnEnemySpawned += GiveExpForKillingEnemy;
    }

    public void GiveReward()
    {
        _player.ReceiveExp(_expForCompleatingRoom);
    }

    private void GiveExpForKillingEnemy(Enemy enemy)
    {
        enemy.OnDied += () => _player.ReceiveExp(enemy.ExpForKilling);
    }
}

