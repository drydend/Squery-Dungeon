using UnityEngine.UI;
using UnityEngine;

public class ExitToMenuButton : MonoBehaviour
{
    [SerializeField]
    private Button _button;
    [SerializeField]
    private SceneTransition _sceneTransition;
    [SerializeField]
    private ConfirmPanel _confirmPanel;

    private void Awake()
    {
        _confirmPanel.SetAction(ExitToMenu);
        _button.onClick.AddListener(TryExitToMenu);
    }

    private void TryExitToMenu()
    {
        _confirmPanel.Open();
    }
    
    private void ExitToMenu()
    {
        _sceneTransition.SwitchToMainMenu();
    }

}

