using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SettingsMenuUI : UIMenu
{
    [SerializeField]
    private GameObject _settingMenu;
    [SerializeField]
    private Button _openButton;
    [SerializeField]
    private Button _closeButton;

    public override bool CanBeClosed { get { return true; } set { } }

    public override void Initialize()
    {
        _openButton.onClick.AddListener(Open);
        _closeButton.onClick.AddListener(Close);
    }

    public override void Cover()
    {
        _settingMenu.SetActive(false);
    }

    public override void Uncover()
    {
        _settingMenu.SetActive(true);
    }

    public override void Close()
    {
        OnMenuClosed();
        _settingMenu.SetActive(false);
    }

    public override void Open()
    {
        OnMenuOpened();
        _settingMenu.SetActive(true);
    }

}
