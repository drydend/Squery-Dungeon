using System.Collections.Generic;
using UnityEngine;

public abstract class UIMenusHandler : MonoBehaviour
{
    [SerializeField]
    protected List<UIMenu> _allMenu;
    protected Stack<UIMenu> _openedMenu;

    protected virtual void Initialize()
    {
        _openedMenu = new Stack<UIMenu>();

        foreach (var menu in _allMenu)
        {
            menu.Initialize();
            menu.OnOpened += CoverPreviousMenu;
            menu.OnOpened += () => _openedMenu.Push(menu);
            menu.OnClosed += ReturnToPreviousMenu;
        }
    }

    public void ReturnToPreviousMenu()
    {
        if (_openedMenu.Count == 1)
        {
            _openedMenu.Pop();
        }
        else if (_openedMenu.Count > 1)
        {
            _openedMenu.Pop();
            _openedMenu.Pop().Open();
        }
    }

    public virtual void EscapeButtonPressed()
    {
        if (_openedMenu.Peek().CanBeClosed)
        {
            _openedMenu.Peek().Close();
        }
    }

    protected void CoverPreviousMenu()
    {
        if (_openedMenu.Count > 0)
        {
            _openedMenu.Peek()?.OnCovered();
        }
    }
}

