using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuUI : UIMenu
{
    [SerializeField]
    private GameObject _settingMenu;
    [SerializeField]
    private Button _openButton;

    public override bool CanBeClosed { get { return true; } set { } }

    public override event Action OnOpened;
    public override event Action OnClosed;

    public override void Initialize()
    {
        _openButton.onClick.AddListener(Open);
        _openButton.onClick.AddListener(() => Debug.Log("uuuu"));
    }

    public override void OnCovered()
    {
        _settingMenu.SetActive(false);
    }

    public override void Close()
    {
        OnClosed?.Invoke();
        _settingMenu.SetActive(false);
    }

    public override void Open()
    {
        Debug.Log("Opened");
        OnOpened?.Invoke();
        _settingMenu.SetActive(true);
    }
}
