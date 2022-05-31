using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
using TMPro;

public class PowerUPBlank : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    private Image _backLight;
    [SerializeField]
    private Image _selectedBackLight;
    [SerializeField]
    private TMP_Text _discription;

    private CharacterModificator _modificator;

    private bool _isSelected;

    public event Action<PowerUPBlank> OnSelected;
    public event Action<PowerUPBlank> OnDestroyed;

    public CharacterModificator CurrentModificator => _modificator;

    public void Initialize(Color backLightColor, CharacterModificator modificator)
    {
        _modificator = modificator;
        _backLight.color = backLightColor;
        _selectedBackLight.color = backLightColor;


        _backLight.enabled = true;
        _selectedBackLight.enabled = false;
        _discription.text = modificator.GetDiscription();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Select();
    }

    public void Select()
    {
        if (_isSelected)
        {
            return;
        }

        _backLight.enabled = false;
        _selectedBackLight.enabled = true;
        _isSelected = true;
        OnSelected?.Invoke(this);
    }

    public void Unselect()
    {
        _backLight.enabled = true;
        _selectedBackLight.enabled = false;
        _isSelected = false;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_isSelected)
            return;
        _backLight.enabled = false;
        _selectedBackLight.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_isSelected)
            return;
        _backLight.enabled = true;
        _selectedBackLight.enabled = false;
    }
}
