using System.Collections.Generic;
using UnityEngine;

public class UILevelMenuHandler : UIMenusHandler
{
    [SerializeField]
    private PauseMenuUI _pauseMenuUI;

    private void Start()
    {
        base.Initialize();

        _pauseMenuUI.Initialize();
        _pauseMenuUI.OnOpened += CoverPreviousMenu;
        _pauseMenuUI.OnOpened += () => _openedMenu.Push(_pauseMenuUI);
        _pauseMenuUI.OnClosed += ReturnToPreviousMenu;
    }

    public override void EscapeButtonPressed()
    {
        if (_openedMenu.Count == 0)
        {
            _pauseMenuUI.Open();
        }
        else
        {
            base.EscapeButtonPressed();
        }
    }
}
