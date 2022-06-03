using System;
using UnityEngine;

public abstract class UIMenu : MonoBehaviour
{
    public abstract bool CanBeClosed { get; set; }

    public abstract event Action OnOpened;
    public abstract event Action OnClosed;

    public abstract void Initialize();
    public abstract void OnCovered();
    public abstract void Open();
    public abstract void Close();
}
