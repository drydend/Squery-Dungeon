using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenuUI : UIMenu
{
    [SerializeField]
    private GameObject _settingMenu;

    public override bool CanBeClosed { get { return true; } set { } }

    public override event Action OnOpened;

    public override void Close()
    {
        _settingMenu.SetActive(false);
    }

    public override void Open()
    {
        OnOpened?.Invoke();
        _settingMenu.SetActive(true);
    }
}
