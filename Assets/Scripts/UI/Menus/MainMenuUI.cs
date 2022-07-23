using UnityEngine;

public class MainMenuUI : UIMenu
{
    [SerializeField]
    private GameObject _mainMenu;

    public override bool CanBeClosed { get { return false; } set { } }

    public override void Close()
    {
        OnMenuClosed();
        _mainMenu.SetActive(false);
    }

    public override void Initialize()
    {
        
    }

    public override void Cover()
    {
        _mainMenu.SetActive(false);
    }

    public override void Uncover()
    {
        _mainMenu.SetActive(true);       
    }

    public override void Open()
    {
        OnMenuOpened();
        _mainMenu.SetActive(true);
    }
}

