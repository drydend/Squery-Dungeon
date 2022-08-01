﻿using UnityEngine.UI;
using UnityEngine;

public class LevelRestartButton :MonoBehaviour
{
    [SerializeField]
    private Button _button;
    [SerializeField]
    private SceneTransition _sceneTransition;
    [SerializeField]
    private ConfirmPanel _confirmPanel;
    [SerializeField]
    private SceneInput _sceneInput;

    private void Awake()
    {
        _confirmPanel.SetAction(RestartLevel);
        _button.onClick.AddListener(TryRestartLevel);        
    }

    private void TryRestartLevel()
    {
        _confirmPanel.Open();
    }

    private void RestartLevel()
    {
        _sceneInput.OffAllInput();
        _sceneTransition.RestartScene();
    }
}

