using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class RoomEntrance : MonoBehaviour
{
    [SerializeField]
    private GameObject _blockingBound;
    [SerializeField]
    private ParticleSystem _closingParticles;
    [SerializeField]
    private Light2D _closingLight;
    [SerializeField]
    private BoxCollider2D _closingCollider;
    [SerializeField]
    private AnimationCurve _closingLightIntension;
    [SerializeField]
    private AnimationCurve _openingLightIntension;
    [SerializeField]
    private float _closingAnimationDuration;
    [SerializeField]
    private float _openingAnimationDuration;
    private bool _isBlocked;

    public void Open()
    {
        if (!_isBlocked)
        {
            StartCoroutine(OpeningAnimation());
            _closingParticles.gameObject.SetActive(false);
            _closingLight.gameObject.SetActive(false);
            _closingCollider.gameObject.SetActive(false);
        }
    }

    public void Close()
    {
        if (!_isBlocked)
        {
            _closingParticles.gameObject.SetActive(true);
            _closingLight.gameObject.SetActive(true);
            _closingCollider.gameObject.SetActive(true);
            StartCoroutine(ClosingAnimation());
        }
    }

    public void Block()
    {
        _isBlocked = true;
        _blockingBound.SetActive(true);
    }

    public void Unblock()
    {
        _isBlocked = false;
        _blockingBound.SetActive(false);
    }

    private IEnumerator OpeningAnimation()
    {
        float timeFromStart = 0f;

        while (timeFromStart < 1 * _openingAnimationDuration)
        {
            _closingLight.intensity = _openingLightIntension.Evaluate(timeFromStart / _openingAnimationDuration);

            timeFromStart += Time.deltaTime;
            yield return null;
        }
    }
        
    private IEnumerator ClosingAnimation()
    {       
        float timeFromStart = 0f;

        while (timeFromStart < 1 * _closingAnimationDuration)
        {
            _closingLight.intensity = _closingLightIntension.Evaluate(timeFromStart / _closingAnimationDuration);

            timeFromStart += Time.deltaTime;
            yield return null;
        }
    }


}
