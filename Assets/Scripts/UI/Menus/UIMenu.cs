using System;
using UnityEngine;

public abstract class UIMenu : MonoBehaviour
{
    public abstract bool CanBeClosed { get; set; }

    public event Action OnOpened;
    public event Action OnClosed;

    public abstract void Initialize();
    public abstract void OnCovered();
    public abstract void Open();
    public abstract void Close();

    protected virtual void OnMenuOpened()
    {
        OnOpened?.Invoke();
    }

    protected virtual void OnMenuClosed()
    {
        OnClosed?.Invoke();
    }
}
