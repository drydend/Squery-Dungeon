using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerStatsUI : UIMenu
{
    private const string ClosingAnimationTrigget = "Close";
    private const string OpeningAnimationTrigget = "Open";

    private Animator _animator;

    public override bool CanBeClosed { get => false; set { } }

    public override void Initialize()
    {
        _animator = GetComponent<Animator>();
    }

    public override void Cover()
    {
        _animator.SetTrigger(ClosingAnimationTrigget);
    }

    public override void Uncover()
    {
        _animator.SetTrigger(OpeningAnimationTrigget);
    }

    public override void Open()
    {
        _animator.SetTrigger(OpeningAnimationTrigget);
        OnMenuOpened();
    }
   
    public override void Close()
    {
        OnMenuClosed();
        _animator.SetTrigger(ClosingAnimationTrigget);
    }
}

