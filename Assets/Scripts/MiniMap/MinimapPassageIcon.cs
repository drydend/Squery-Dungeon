using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[RequireComponent(typeof(Image))]
public class MinimapPassageIcon : MonoBehaviour
{
    private MinimapRoomIcon _firstRoom;
    private MinimapRoomIcon _secondRoom;
    
    private Image _image;
    private RectTransform _rectTransform;

    public bool IsVisible { get; private set; }

    public void Initialize(Vector2 iconSize, Vector2 iconPosition,Vector3 iconRotation ,MinimapRoomIcon firstRoom, MinimapRoomIcon secondRoom )
    {
        _image = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();
        _firstRoom = firstRoom;
        _secondRoom = secondRoom;
        _rectTransform.rotation = Quaternion.Euler(iconRotation);

        IsVisible = true;
        _rectTransform.anchoredPosition = iconPosition;
        _rectTransform.sizeDelta = iconSize;
    }
  
    public void BecameVisible()
    {
        if (IsVisible)
        {
            return;
        }

        IsVisible = true;
        _image.enabled = true;
    }

    public void BecameInvisible()
    {
        if (!IsVisible)
        {
            return;
        }

        IsVisible = false;
        _image.enabled = false;
    }

    public bool IsConnectedRoomsVisible()
    {
        return _firstRoom.IsVisible && _secondRoom.IsVisible;
    }

}

