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

        _pauseMenuUI.OnOpened += () => _openedMenu.Push(_pauseMenuUI);
        foreach (var menu in _allMenu)
        {
            menu.OnOpened += () => _openedMenu.Push(menu);
        }

        _playerInput.EscapeButton.performed += (context) => EscapeButtonPressed();
    }

    public void ReturnToPreviousMenu()
    {
        if (_openedMenu.Count == 1)
        {
            _openedMenu.Pop().Close();
            PauseMenager.Instance.Unpause();
        }
        else if (_openedMenu.Count > 1)
        {
            _openedMenu.Pop().Close();
            _openedMenu.Pop().Open();
        }
    }

    private void EscapeButtonPressed()
    {
        if (_openedMenu.Count == 0)
        {
            _pauseMenuUI.Open();
            PauseMenager.Instance.Pause();
        }
        else
        {
            if (_openedMenu.Peek().CanBeClosed)
            {
                ReturnToPreviousMenu();
            }
        }
    }

}
