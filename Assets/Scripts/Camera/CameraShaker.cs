using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    private Coroutine _currentCameraShake;
    
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

            timeFromStart += Time.deltaTime / duration;
            yield return null;
        }
    }
}
