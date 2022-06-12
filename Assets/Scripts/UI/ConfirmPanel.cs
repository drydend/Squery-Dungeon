using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ConfirmPanel : UIMenu
{
    [SerializeField]
    private Button _confirmButton;
    [SerializeField]
    private Button _denyButton;

    private Action _action;

    public override bool CanBeClosed { get { return false; } set { } }

    public override void Initialize()
    {
        _confirmButton.onClick.AddListener(Confirm);
        _denyButton.onClick.AddListener(Deny);
    }
    
    public void SetAction(Action action)
    {
        _action = action;
    }

    public override void Close()
    {
        gameObject.SetActive(false);
        OnMenuClosed();
    }

    public override void Open()
    {
        OnMenuOpened();
        gameObject.SetActive(true);
    }

    public override void OnCovered()
    {
        
    }


    private void Confirm()
    {
        _action.Invoke();
        Close();
    }

    private void Deny()
    {
        Close();
    }
}

