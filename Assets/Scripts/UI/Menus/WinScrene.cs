using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class WinScrene : UIMenu
{
    [SerializeField]
    private GameObject _screne;

    [SerializeField]
    private Button _backToMenuButton;
    [SerializeField]
    private Button _startAgain;
    [SerializeField]
    private SceneTransition _sceneTransition;

    [SerializeField]
    private List<ParticleSystem> _particles;

    [SerializeField]
    private ScreneFade _screneFade;

    public override bool CanBeClosed { get => false; set { } }

    public override void Initialize()
    {
        _backToMenuButton.onClick.AddListener(() => _sceneTransition.SwitchToMainMenu());
        _startAgain.onClick.AddListener(() => _sceneTransition.RestartScene());
    }

    public override void Cover()
    {
        
    }

    public override void Uncover()
    {

    }

    public override void Open()
    {
        OnMenuOpened();

        _screne.SetActive(true);
        
        _screneFade.Fade(0.7f);
        foreach (var particle in _particles)
        {
            particle.Play();
        }
    }
   
    public override void Close()
    {
        OnMenuClosed();
        _screne.SetActive(false);
    }

}

