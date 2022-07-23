using System.Collections.Generic;
using UnityEngine;

public abstract class UIMenusHandler : MonoBehaviour
{
    [SerializeField]
    protected List<UIMenu> _allMenu;
    protected Stack<UIMenu> _openedMenu;

    protected UIMenu CurrentMenu => _openedMenu.Peek();

    protected virtual void Initialize()
    {
        _openedMenu = new Stack<UIMenu>();

        foreach (var menu in _allMenu)
        {
            InitializeMenu(menu);
        }
    }

    public void ReturnToPreviousMenu()
    {   
        if(!CurrentMenu.CanBeClosed)
        {
            return;
        }

        if (_openedMenu.Count == 1)
        {
            CurrentMenu.Close();
        }
        else if (_openedMenu.Count > 1)
        {
            CurrentMenu.Close();
            CurrentMenu.Uncover();
        }
    }

    public virtual void EscapeButtonPressed()
    {
        ReturnToPreviousMenu();
    }

    protected void UncoverPreviousMenu()
    {
        if (_openedMenu.Count > 0)
        {
            CurrentMenu.Uncover();
        }
    }

    protected void CoverPreviousMenu()
    {
        if (_openedMenu.Count > 0)
        {
            CurrentMenu.Cover();
        }
    }

    protected void InitializeMenu(UIMenu menu)
    {
        menu.Initialize();
        menu.OnOpened += CoverPreviousMenu;
        menu.OnOpened += () => _openedMenu.Push(menu);
        menu.OnClosed += () => _openedMenu.Pop();
        menu.OnClosed += UncoverPreviousMenu;
    }
}

