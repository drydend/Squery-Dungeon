using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PauseMenuUI : UIMenu
{
    [SerializeField]
    private GameObject _pauseMenu;
    [SerializeField]
    private Button _closeButton;
    [SerializeField]
    private List<ParticleSystem> _particles;

    public override bool CanBeClosed { get { return true; } set { } }

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
        _pauseMenu.SetActive(true);

        foreach (var particle in _particles)
        {
            particle.Play();
        }

        OnMenuOpened();
        PauseMenager.Instance.Pause();
    }

    public override void Close()
    {
        foreach (var particle in _particles)
        {
            particle.Stop();
        }

        OnMenuClosed();
        _pauseMenu.SetActive(false);
        PauseMenager.Instance.Unpause();
    }
}
