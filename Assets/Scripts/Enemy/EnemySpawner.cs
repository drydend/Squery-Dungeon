using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    private Character _targetForEnemiesByDefault => _player.CurrentCharacter;

    public Enemy SpawnEnemy(TrialRoom room, Enemy enemyPrefab)
    { 
        var spawnedEnemy = Instantiate(enemyPrefab, room.GetRandomPositionInRoom(), enemyPrefab.transform.rotation);
        spawnedEnemy.Initialize(_targetForEnemiesByDefault);
        return spawnedEnemy;
    }
}
