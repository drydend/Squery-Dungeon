using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
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
        _currentLevelCreator.CreateLevel();
        _player.SetCharacterPosition(_currentLevelCreator.StartRoom.transform.position);
    }
}
