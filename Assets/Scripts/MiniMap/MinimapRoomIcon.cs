using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(Image))]
public class MinimapRoomIcon : MonoBehaviour
{
    [SerializeField]
    private Color _selectedColor;
    [SerializeField]
    private Color _compleatedColor;
    [SerializeField]
    private Color _uncompleatedColor;

    private List<MinimapRoomIcon> _connectedIcons = new List<MinimapRoomIcon>();
    private List<MinimapPassageIcon> _connectedPassages = new List<MinimapPassageIcon>();

    public List<MinimapPassageIcon> ConnectedPassages => _connectedPassages;
    public List<MinimapRoomIcon> ConnectedRoomIcons => _connectedIcons;
    public bool IsVisible { get; private set; }
    public Image Image { get; private set; }
    public RectTransform RectTransform { get; private set; }

    public event Action OnBecameVisible;
    public event Action OnBecameInvisible;

    public void Initialize(Sprite sprite, Vector2 size, Vector2 position)
    {
        Image = GetComponent<Image>();
        RectTransform = GetComponent<RectTransform>();

        IsVisible = true;
        Image.sprite = sprite;
        RectTransform.anchoredPosition = position;
        RectTransform.sizeDelta = size;
    }

    public void Unselect()
    {
        Image.color = _compleatedColor;
    }

    public void Select()
    {
        Image.color = _selectedColor;
    }

    public void BecameInvisible()
    {
        if (!IsVisible)
        {
            return;
        }

        IsVisible = false;
        Image.enabled = false;

        foreach (var passage in _connectedPassages)
        {
            if (!passage.IsConnectedRoomsVisible())
            {
                passage.BecameInvisible();
            }
        }

        OnBecameInvisible?.Invoke();
    }

    public void BecameVisible()
    {
        if (IsVisible)
        {
            return;
        }

        IsVisible = true;
        Image.enabled = true;
        
        foreach (var passage in _connectedPassages)
        {
            if (passage.IsConnectedRoomsVisible())
            {
                passage.BecameVisible();
            }
        }

        OnBecameVisible?.Invoke();
    }

    public void ConnectIcons(MinimapRoomIcon roomIcon, MinimapPassageIcon passage)
    {
        _connectedIcons.Add(roomIcon);
        roomIcon._connectedIcons.Add(this);

        _connectedPassages.Add(passage);
        roomIcon._connectedPassages.Add(passage);
    }
}

