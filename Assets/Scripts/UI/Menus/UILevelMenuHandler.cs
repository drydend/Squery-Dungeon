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

        InitializeMenu(_pauseMenuUI);
        InitializeMenu(_playerStatsUI);

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
