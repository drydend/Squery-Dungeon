using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using TMPro;
using System.Collections;

[RequireComponent(typeof(RectTransform))]
public class PowerUPBlank : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    private AnimationCurve _creationAnimation;
    [SerializeField]
    private float _creationAnimationDuration;
    [SerializeField]
    private float _selectionAnimationDuration;
    [SerializeField]
    private float _scaleFactorOnSelected;
    private float _currentScaleFactor = 1;
    private Vector2 _startScale;

    [SerializeField]
    private Image _backLight;
    [SerializeField]
    private Image _selectedBackLight;
    [SerializeField]
    private Image _icon;
    [SerializeField]
    private TMP_Text _discription;

    private Modificator _modificator;
    private RectTransform _rectTransform;

    private bool _isSelected;
    private bool _isAnimated;
    private Coroutine _selectionAnimation;
    private Coroutine _unselectionAnimation;

    public event Action<PowerUPBlank> OnSelected;
    public event Action<PowerUPBlank> OnDestroyed;

    public Modificator CurrentModificator => _modificator;

    public void Initialize(Color backLightColor, Modificator modificator)
    {
        _modificator = modificator;
        _backLight.color = backLightColor;
        _selectedBackLight.color = backLightColor;
        _icon.sprite = _modificator.Icon;

        _backLight.enabled = true;
        _selectedBackLight.enabled = false;
        _discription.text = modificator.GetDiscription();

        _rectTransform = GetComponent<RectTransform>();
        _startScale = _rectTransform.localScale;
    }

    public void PlayCreationAnimation()
    {
        StartCoroutine(CreationAnimation());
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Select();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_isSelected || _isAnimated)
        {
            return;
        }

        if (_unselectionAnimation != null)
        {
            StopCoroutine(_unselectionAnimation);
            _unselectionAnimation = null;
        }

        if (_selectionAnimation == null)
        {
            _selectionAnimation = StartCoroutine(SelectionAnimation());
        }

        _backLight.enabled = false;
        _selectedBackLight.enabled = true; 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_isSelected || _isAnimated)
        {
            return;
        }

        if (_selectionAnimation != null)
        {
            StopCoroutine(_selectionAnimation);
            _selectionAnimation = null;
        }

        if (_unselectionAnimation == null)
        {
            _unselectionAnimation = StartCoroutine(UnselectionAnimation());
        }

        _backLight.enabled = true;
        _selectedBackLight.enabled = false;
    }
    
    public void Select()
    {
        if (_isSelected || _isAnimated)
        {
            return;
        }

        _rectTransform.localScale = _startScale * _scaleFactorOnSelected;
        
        _backLight.enabled = false;
        _selectedBackLight.enabled = true;
        
        _isSelected = true;
        OnSelected?.Invoke(this);
    }

    public void Unselect()
    {
        _unselectionAnimation = StartCoroutine(UnselectionAnimation());
        _backLight.enabled = true;
        _selectedBackLight.enabled = false;
        _isSelected = false;
    }

    private IEnumerator SelectionAnimation()
    {
        var timeElapsed = 0f;

        while (timeElapsed < _selectionAnimationDuration)
        {
            _currentScaleFactor = Mathf.Lerp(_currentScaleFactor, _scaleFactorOnSelected, timeElapsed / _selectionAnimationDuration);
            _rectTransform.localScale = _startScale * _currentScaleFactor;

            timeElapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        _selectionAnimation = null;
    }

    private IEnumerator UnselectionAnimation()
    {
        var timeElapsed = 0f;

        while (timeElapsed < _selectionAnimationDuration)
        {
            _currentScaleFactor = Mathf.Lerp(_currentScaleFactor, 1, timeElapsed / _selectionAnimationDuration);
            _rectTransform.localScale = _startScale * _currentScaleFactor;

            timeElapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        _unselectionAnimation = null;
    }

    private IEnumerator CreationAnimation()
    {
        float timeElapsed = 0;
        _isAnimated = true;

        while (timeElapsed < _creationAnimationDuration)
        {
            var currentScale = _creationAnimation.Evaluate(timeElapsed / _creationAnimationDuration);

            _rectTransform.localScale = _startScale * currentScale;

            timeElapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        _isAnimated = false;
    }
}
