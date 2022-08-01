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
    private List<ParticleSystem> _particles = new List<ParticleSystem>(0);

    public override bool CanBeClosed { get { return true; } set { } }

    public override void Initialize()
    {
        foreach (var particle in _particles)
        {
            particle.Clear();
            particle.Stop();
        }

        _closeButton.onClick.AddListener(Close);
    }

    public override void Cover()
    {
        _pauseMenu.SetActive(false);
    }

    public override void Uncover()
    {
        _pauseMenu.SetActive(true);
    }

    public override void Open()
    {
        foreach (var particle in _particles)
        {
            particle.Play();
        }

        _pauseMenu.SetActive(true);
        OnMenuOpened();
        PauseMenager.Instance.Pause();
    }

    public override void Close()
    {
        foreach (var particle in _particles)
        {
            particle.Clear();
            particle.Stop();
        }

        OnMenuClosed();
        _pauseMenu.SetActive(false);
        PauseMenager.Instance.Unpause();
    }
}
