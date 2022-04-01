using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private PowerUpHandler _powerUpHandler;
    [SerializeField]
    private EnemyWaveCreator _enemyWaveCreator;
    [SerializeField]
    private EnemySpawner _enemySpawner;

    [SerializeField]
    private LevelCreator _currentLevelCreator;
    [SerializeField]
    private Player _player;

    private void Awake()
    {
        _currentLevelCreator.Initialize(_enemyWaveCreator, _enemySpawner, _powerUpHandler);
    }

    private void Start()
    {   
        BeginLevel();
    }

    private void BeginLevel()
    {
        _currentLevelCreator.CreateLevel();
        _player.SetCharacterPosition(_currentLevelCreator.StartRoom.transform.position);
    }
}
