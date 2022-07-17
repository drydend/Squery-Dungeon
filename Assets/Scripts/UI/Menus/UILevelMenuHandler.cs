using System.Collections.Generic;
using UnityEngine;

public class UILevelMenuHandler : UIMenusHandler
{
    [SerializeField]
    private PauseMenuUI _pauseMenuUI;
    [SerializeField]
    private PlayerStatsUI _playerStatsUI;

    private void Start()
    {
        base.Initialize();

        _pauseMenuUI.Initialize();
        _pauseMenuUI.OnOpened += CoverPreviousMenu;
        _pauseMenuUI.OnOpened += () => _openedMenu.Push(_pauseMenuUI);
        _pauseMenuUI.OnClosed += ReturnToPreviousMenu;

        _playerStatsUI.Initialize();
        _playerStatsUI.OnOpened += CoverPreviousMenu;
        _playerStatsUI.OnOpened += () => _openedMenu.Push(_playerStatsUI);
        _playerStatsUI.OnClosed += ReturnToPreviousMenu;
        _openedMenu.Push(_playerStatsUI);
    }

    public override void EscapeButtonPressed()
    {
        if (_openedMenu.Count == 1)
        {
            _pauseMenuUI.Open();
        }
        else
        {
            base.EscapeButtonPressed();
        }
    }
}
