using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollower : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private Player _player;
    [SerializeField]
    private float _lerpFactor = 0.001f;

    private void FixedUpdate()
    {
        Follow(_player.CharacterTransform);
    }

    private void Follow(Transform target)
    {
        var newX = Mathf.Lerp(transform.position.x, target.position.x, _lerpFactor);
        var newY = Mathf.Lerp(transform.position.y, target.position.y, _lerpFactor);
        transform.position = new Vector3(newX, newY, transform.position.z);
    }
}
