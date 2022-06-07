using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private PowerUPHandler _powerUpHandler;
    [SerializeField]
    private EnemyWaveCreator _enemyWaveCreator;
    [SerializeField]
    private EnemySpawner _enemySpawner;
    [SerializeField]
    private LevelCreator _currentLevelCreator;
    [SerializeField]
    private MinimapController _minimapController;
    [SerializeField]
    private CameraFollower _cameraFollower;

    [SerializeField]
    private Player _player;

    private Room[,] _levelMap;

    private void Awake()
    {
        _currentLevelCreator.Initialize(_enemyWaveCreator, _enemySpawner, _powerUpHandler);
        BeginLevel();
        _minimapController.Initialize(_levelMap);
    }

    private void Start()
    {   
    }

    private void BeginLevel()
    {
        _levelMap = _currentLevelCreator.CreateLevel();
        _player.SetCharacterPosition(_currentLevelCreator.StartRoom.transform.position);
        _cameraFollower.SetCameraPosition(_player.CharacterTransform.position);
    }
}
