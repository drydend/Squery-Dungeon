using UnityEngine;

public class UIInput : MonoBehaviour
{
    [SerializeField]
    private PlayerInput _playerInput;
    [SerializeField]
    private UIMenusHandler _uiMenusHandler;

    private void Awake()
    {
        _playerInput.EscapeButton.performed += (context) => _uiMenusHandler.EscapeButtonPressed();
    }
}

