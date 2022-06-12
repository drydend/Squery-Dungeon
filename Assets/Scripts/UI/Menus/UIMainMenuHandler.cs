using UnityEngine;

public class UIMainMenuHandler : UIMenusHandler
{
    [SerializeField]
    private MainMenuUI _mainMenu;

    private void Start()
    {
        base.Initialize();

        _mainMenu.Initialize();
        _mainMenu.OnOpened += CoverPreviousMenu;
        _mainMenu.OnOpened += () => _openedMenu.Push(_mainMenu);
        _mainMenu.OnClosed += ReturnToPreviousMenu;

        _openedMenu.Push(_mainMenu);
    }
}

