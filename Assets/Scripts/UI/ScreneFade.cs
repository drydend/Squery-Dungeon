using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ScreneFade : MonoBehaviour
{
    [SerializeField]
    private bool _shouldPlayUnfadeAtStart = false;

    [SerializeField]
    private float _animationDuration = 1;
    private Image _screne;

    private Coroutine _fadeCoroutine;

    public bool IsAnimated { get; private set; }

    private void Awake()
    {
        _screne = GetComponent<Image>();
    }

    public void Start()
    {
        if (_shouldPlayUnfadeAtStart)
        {
            var startColor = _screne.color;
            startColor.a = 1;
            _screne.color = startColor;
            Unfade();
        }
    }

    public void Fade(float fadeIntencity = 1f)
    {
        if (_fadeCoroutine != null)
        {
            StopCoroutine(_fadeCoroutine);
        }

        _fadeCoroutine =  StartCoroutine(FadeCoroutine(fadeIntencity));
    }

    public void Unfade()
    { 
        if(_fadeCoroutine != null)
        {
            StopCoroutine(_fadeCoroutine);
        }

        _fadeCoroutine = StartCoroutine(FadeCoroutine(0));
    }

    private IEnumerator FadeCoroutine(float fadeIntencity = 1f)
    {
        var timeElapsed = 0f;
        var startAlpha = _screne.color.a;
        IsAnimated = true;

        while(timeElapsed < _animationDuration)
        {
            Color currentColor = _screne.color;
            currentColor.a = Mathf.Lerp(startAlpha, fadeIntencity, timeElapsed / _animationDuration);
            _screne.color = currentColor;
            timeElapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        Color color = _screne.color;
        color.a = Mathf.Lerp(startAlpha, fadeIntencity, 1);
        _screne.color = color;

        IsAnimated = false;
    }
}
