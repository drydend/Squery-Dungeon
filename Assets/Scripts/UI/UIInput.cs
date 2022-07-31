using UnityEngine;

public class UIInput : MonoBehaviour
{
    [SerializeField]
    private PlayerInput _playerInput;

    [SerializeField]
    private UIMenusHandler _uiMenusHandler;
    [SerializeField]
    private PlayerLevelController _playerLevelController;

    private void Start()
    {
        _playerInput.EscapeButton.performed += (context) => _uiMenusHandler.EscapeButtonPressed();
        _playerInput.EButton.performed += (context) => _playerLevelController.ShowRewards();
    }
}

