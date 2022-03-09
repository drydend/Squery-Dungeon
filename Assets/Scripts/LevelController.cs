using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private EnemyWaveCreator _evemyWaveCreator;
    [SerializeField]
    private LevelCreator _currentLevelCreator;
    [SerializeField]
    private Player _player;

    private void Start()
    {
        BeginLevel();
    }

    private void BeginLevel()
    {
        _currentLevelCreator.CreateLevel(_evemyWaveCreator, _player.CurrentCharacter);
        _player.SetCharacterPosition(_currentLevelCreator.StartRoom.transform.position);
    }
}
