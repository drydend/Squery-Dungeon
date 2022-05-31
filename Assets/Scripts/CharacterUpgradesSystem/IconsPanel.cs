using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class IconsPanel : MonoBehaviour
{
    [SerializeField]
    private int _maxIcons = 3;
    [SerializeField]
    private Image _iconImagePrefab;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Initialize(List<Sprite> icons)
    {
        
    }
}

