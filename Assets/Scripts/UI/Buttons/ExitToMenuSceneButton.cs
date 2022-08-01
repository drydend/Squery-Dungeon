﻿using UnityEngine.UI;
using UnityEngine;

public class ExitToMenuSceneButton : MonoBehaviour
{
    [SerializeField]
    private Button _button;
    [SerializeField]
    private SceneTransition _sceneTransition;
    [SerializeField]
    private ConfirmPanel _confirmPanel;
    [SerializeField]
    private LevelController _levelController;
    [SerializeField]
    private SceneInput _sceneInput;

    private void Awake()
    {
        _confirmPanel.SetAction(ExitToMenu);
        _button.onClick.AddListener(TryExitToMenu);
    }

    private void TryExitToMenu()
    {
        _confirmPanel.Open();
    }
    
    private void ExitToMenu()
    {
        _sceneInput.OffAllInput();
        _levelController.SaveLevel();
        _sceneTransition.SwitchToMainMenu();
    }

}

