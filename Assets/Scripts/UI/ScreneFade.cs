using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ScreneFade : MonoBehaviour
{
    private const string FadeAnimationTrigger = "Fade";
    private const string UnfadeAnimationTrigger = "Unfade";

    private static bool _shouldPlayOpeningAnimation = false;

    public bool IsAnimated { get; private set; }
    private Animator _animator;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Start()
    {
        if (_shouldPlayOpeningAnimation)
        {
            Unfade();
        }
    }

    public void Fade()
    {
        _shouldPlayOpeningAnimation = true;
        IsAnimated = true;
        _animator.SetTrigger(FadeAnimationTrigger);
    }

    public void Unfade()
    {
        _shouldPlayOpeningAnimation = false;
        IsAnimated = true;
        _animator.SetTrigger(UnfadeAnimationTrigger);
    }

    public void OnAnimationEnded()
    {
        IsAnimated = false;
    }
}
