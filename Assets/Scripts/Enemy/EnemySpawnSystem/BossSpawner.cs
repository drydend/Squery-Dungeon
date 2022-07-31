using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    public Boss SpawnBoss(BossRoom bossRoom)
    {
        var boss = Instantiate(bossRoom.BossOfRoom, bossRoom.BossSpawnPosition.position, Quaternion.identity);
        boss.Initialize(_player.CurrentCharacter);
        boss.PlaySpawningAnimation();
        return boss;
    }
}

