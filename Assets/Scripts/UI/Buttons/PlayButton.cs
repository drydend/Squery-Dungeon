using UnityEngine.UI;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    private const string LevelName = "Level 1";

    [SerializeField]
    private Button _playButton;
    [SerializeField]
    private SceneTransition _sceneTransition;

    private void Awake()
    {
        _playButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        _sceneTransition.SwitchToScene(LevelName);
    }
}
