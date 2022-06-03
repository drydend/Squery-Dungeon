using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuUI : UIMenu
{
    [SerializeField]
    private GameObject _pauseMenu;
    [SerializeField]
    private Button _closeButton;

    public override bool CanBeClosed { get { return true; } set { } }

    public override event Action OnOpened;
    public override event Action OnClosed;

    public override void Initialize()
    {
        _closeButton.onClick.AddListener(Close);
    }

    public override void OnCovered()
    {
        _pauseMenu.SetActive(false);
    }

    public override void Open()
    {
        OnOpened?.Invoke();
        _pauseMenu.SetActive(true);
        PauseMenager.Instance.Pause();
    }

    public override void Close()
    {
        OnClosed?.Invoke();
        _pauseMenu.SetActive(false);
        PauseMenager.Instance.Unpause();
    }
}
