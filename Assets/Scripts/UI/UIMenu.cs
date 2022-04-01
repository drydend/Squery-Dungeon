using System;
using UnityEngine;

public abstract class UIMenu : MonoBehaviour
{
    public abstract bool CanBeClosed { get; set; }

    public abstract event Action OnOpened;
    public abstract void Open();
    public abstract void Close();
}
