using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
using TMPro;

public class PowerUpBlank : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    private Image _backLight;
    [SerializeField]
    private Image _gem;
    [SerializeField]
    private TMP_Text _discription;

    private PowerUp _powerUp;

    private bool _isSelected;

    public event Action OnSelected;

    public PowerUp CurrentPowerUp => _powerUp;

    public void Initialize(Sprite backLight,Sprite gem,PowerUp powerUp)
    {
        _powerUp = powerUp;
        _backLight.sprite = backLight;
        _gem.sprite = gem;
        _discription.text = new PowerUpDiscriptionParser(powerUp).GetDiscription();
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
        OnSelected?.Invoke();
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
