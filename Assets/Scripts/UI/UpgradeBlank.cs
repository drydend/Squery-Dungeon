using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
using TMPro;

public class UpgradeBlank : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    private Image _backLight;
    [SerializeField]
    private Image _gem;
    [SerializeField]
    private TMP_Text _discription;

    private Upgrade _upgrade;

    private bool _isSelected;

    public event Action<UpgradeBlank> OnSelected;
    public event Action<UpgradeBlank> OnDestroyed;
    
    public Upgrade CurrentUpgrade => _upgrade;

    public void Initialize(Sprite backLight,Sprite gem,Upgrade powerUp)
    {
        _upgrade = powerUp;
        _backLight.sprite = backLight;
        _gem.sprite = gem;
        _discription.text = new UpgradeDiscriptionParser(powerUp).GetDiscription();
    }

    public void Unselect()
    {
        _backLight.gameObject.SetActive(false);
        _isSelected = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isSelected)
        {
            return;
        }

        _backLight.gameObject.SetActive(true);
        _isSelected = true;
        OnSelected?.Invoke(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_isSelected)
            return;
        _backLight.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_isSelected)
            return;
        _backLight.gameObject.SetActive(false);
    }
}
