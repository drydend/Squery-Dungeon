﻿using UnityEngine;

public class Crate : MonoBehaviour, IHitable
{
    [SerializeField]
    protected float _heals;
    [SerializeField]
    protected ScalableParticles _destructParticlePrefab;
    [SerializeField]
    protected float _particleScaleFactor = 1;
    [SerializeField]
    protected AudioClip _breakingSound;

    protected AudioSource _audioSource;
    protected bool _isDestructed;

    public Transform Transform => transform;

    protected void Awake()
    {
        _audioSource = AudioSourceProvider.Instance.GetSoundsSource();
    }

    public void RecieveHit(float damage, GameObject sender)
    {
        if (damage < 0)
            throw new System.Exception("Damage can`t be less than zero");

        _heals = _heals - damage >= 0 ? _heals - damage : 0;
        
        if(_heals <= 0)
        {
            Destruct();
        }
    }

    protected virtual void Destruct()
    {
        _audioSource.PlayOneShot(_breakingSound);
        _isDestructed = true;
        var destructionParticles = Instantiate(_destructParticlePrefab, transform.position, Quaternion.Euler(Vector3.zero));
        destructionParticles.Scale(_particleScaleFactor);
        Destroy(gameObject);
    }
}
