using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    private CameraShaker _cameraShaker;

    private Character _targetForEnemiesByDefault => _player.CurrentCharacter;

    public Enemy SpawnEnemy(TrialRoom room, Enemy enemyPrefab)
    { 
        var spawnedEnemy = Instantiate(enemyPrefab, room.GetRandomPositionInRoom(), enemyPrefab.transform.rotation);
        spawnedEnemy.Initialize(_targetForEnemiesByDefault);
        spawnedEnemy.OnDie += () => 
            _cameraShaker.ShakeCamera(spawnedEnemy.CameraShakeDurationOnDeath, spawnedEnemy.CameraShakeStrenghtOnDeath);
        return spawnedEnemy;
    }
}
