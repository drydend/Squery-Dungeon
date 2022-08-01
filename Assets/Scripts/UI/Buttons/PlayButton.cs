using UnityEngine.UI;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    private const string LevelName = "Level 1";

    [SerializeField]
    private Button _playButton;
    [SerializeField]
    private SceneTransition _sceneTransition;
    [SerializeField]
    private MainMenu _mainMenu;
    [SerializeField]
    private SceneInput _sceneInput;

    private void Awake()
    {
        _playButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        _mainMenu.OnMainMenuClosed();
        _sceneTransition.SwitchToScene(LevelName);
        _sceneInput.OffAllInput();
    }
}
