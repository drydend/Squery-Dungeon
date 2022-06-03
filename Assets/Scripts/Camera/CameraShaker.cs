using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShaker : MonoBehaviour
{
    public static CameraShaker Instance;

    private Coroutine _currentCameraShake;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            throw new Exception("CamraShaker can be only one");
        }
    }

    public void ShakeCamera(float duration, float shakeRadious)
    {   
        if(_currentCameraShake != null)
        {
            StopCoroutine(_currentCameraShake);
        }

        _currentCameraShake = StartCoroutine(ShakeCoroutine (duration, shakeRadious));
    }

    private IEnumerator ShakeCoroutine(float duration , float shakeRadious)
    {
        float timeFromStart = 0f;
        Vector3 startPosition = transform.position;

        while(timeFromStart < 1)
        {
            float xPos = Random.Range(-1, 1) * shakeRadious;
            float yPos = Random.Range(-1, 1) * shakeRadious;

            transform.localPosition = (Vector2)startPosition + new Vector2(xPos, yPos);

            timeFromStart += Time.unscaledDeltaTime / duration;
            yield return null;
        }
    }
}
