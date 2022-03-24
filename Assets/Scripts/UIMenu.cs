using System;
using UnityEngine;

public abstract class UIMenu : MonoBehaviour
{   
    public abstract event Action OnOpened;
    public abstract void Open();
    public abstract void Close();
}
