using UnityEngine;

public class UIMainMenuHandler : UIMenusHandler
{
    [SerializeField]
    private MainMenuUI _mainMenu;

    private void Start()
    {
        base.Initialize();

        InitializeMenu(_mainMenu);

        _openedMenu.Push(_mainMenu);
    }
}

