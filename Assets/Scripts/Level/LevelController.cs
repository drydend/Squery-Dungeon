using UnityEngine;
using UnityEngine.EventSystems;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private LevelCreator _currentLevelCreator;
    [SerializeField]
    private MinimapController _minimapController;

    [SerializeField]
    private CameraFollower _cameraFollower;
    [SerializeField]
    private Player _player;

    private Room[,] _levelMap;

    private void Start()
    {
        SaveController.Instance.LoadGame();
        BeginLevel();
    }

    public void SaveLevel()
    {
        SaveController.Instance.SaveGame();
    }

    private void BeginLevel()
    {
        _levelMap = _currentLevelCreator.CreateLevel();
        _minimapController.Initialize(_levelMap);
        _player.SetCharacterPosition(_currentLevelCreator.StartRoom.transform.position);
        _cameraFollower.SetCameraPosition(_player.CharacterTransform.position);
    }
}
