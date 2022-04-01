using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuUI : UIMenu
{
    [SerializeField]
    private GameObject _pauseMenu;

    public override bool CanBeClosed { get { return true; } set { } }

    public override event Action OnOpened;

    public override void Open()
    {
        OnOpened?.Invoke();
        _pauseMenu.SetActive(true);
    }

    public override void Close()
    {
        _pauseMenu.SetActive(false);
    }
}
