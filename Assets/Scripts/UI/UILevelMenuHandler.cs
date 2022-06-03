using System.Collections.Generic;
using UnityEngine;

public class UILevelMenuHandler : MonoBehaviour
{
    [SerializeField]
    private PlayerInput _playerInput;

    [SerializeField]
    private PauseMenuUI _pauseMenuUI;

    [SerializeField]
    private List<UIMenu> _allMenu;
    private Stack<UIMenu> _openedMenu;

    private void Start()
    {
        _openedMenu = new Stack<UIMenu>();

        _pauseMenuUI.Initialize();
        _pauseMenuUI.OnOpened += CoverPreviousMenu;
        _pauseMenuUI.OnOpened += () => _openedMenu.Push(_pauseMenuUI);
        _pauseMenuUI.OnClosed += ReturnToPreviousMenu;

        foreach (var menu in _allMenu)
        {
            menu.Initialize();
            menu.OnOpened += CoverPreviousMenu;
            menu.OnOpened += () => _openedMenu.Push(menu);
            menu.OnClosed += ReturnToPreviousMenu;
        }

        _playerInput.EscapeButton.performed += (context) => EscapeButtonPressed();
    }

    public void ReturnToPreviousMenu()
    {
        if (_openedMenu.Count == 1)
        {
            _openedMenu.Pop();
        }
        else if (_openedMenu.Count > 1)
        {
            _openedMenu.Pop();
            _openedMenu.Pop().Open();
        }
    }

    private void EscapeButtonPressed()
    {
        if (_openedMenu.Count == 0)
        {
            _pauseMenuUI.Open();
        }
        else
        {
            if (_openedMenu.Peek().CanBeClosed)
            {
                _openedMenu.Peek().Close();
            }
        }
    }

    private void CoverPreviousMenu()
    {
        if(_openedMenu.Count > 0)
        {
            _openedMenu.Peek()?.OnCovered();
        }
    }

}
