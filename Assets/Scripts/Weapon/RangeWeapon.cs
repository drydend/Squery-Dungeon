using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RangeWeapon : MonoBehaviour
{   
    [SerializeField]
    protected Transform _projectileSpawnPosition;
    [SerializeField]
    protected ParticleSystem _shootingParticle;
    [SerializeField]
    protected AudioClip _shootSound;
    protected List<Effect> _projectileEffects;
    protected float _projectileDamageMulptiplier = 1f;
    protected float _projectileSpeedMulptiplier = 1f;
    protected float _projectileAdditiveDamage;
    protected float _projectileAdditiveSpeed;
    protected GameObject _owner;
    protected AudioSource _audioSource;

    public event Action OnShooted;

    public void Initialize(GameObject owner,List<Effect> projectileEffects ,
        float projectileAdditiveDamage, float projectileAdditiveSpeed)
    {
        _owner = owner;
        _projectileEffects = projectileEffects;
        _projectileAdditiveDamage = projectileAdditiveDamage;
        _projectileAdditiveSpeed = projectileAdditiveSpeed;
        _audioSource = GetComponent<AudioSource>();
    }

    public void SetAdditiveSpeed(float value)
    {
        if(value < 0)
        {
            throw new Exception("Vallue must be more than zero");
        }

        _projectileAdditiveSpeed = value;
    }

    public void SetAdditiveDamage(float value)
    {
        if (value < 0)
        {
            throw new Exception("Vallue must be more than zero");
        }

        _projectileAdditiveDamage = value;
    }

    public void SetSpeedMulptiplier(float multiplier)
    {
        if (multiplier < 0)
            throw new Exception("Multiplier can`t be less than zero");

        _projectileSpeedMulptiplier = multiplier;
    }

    public void SetDamageMultiplier(float multiplier)
    {
        if (multiplier < 0)
            throw new Exception("Multiplier can`t be less than zero");
        
        _projectileDamageMulptiplier = multiplier;
    }

    public virtual void Attack(Vector3 targetPosition, Projectile projectilePrefab)
    {
        Vector2 projectileDirection = targetPosition - _owner.transform.position;
        projectileDirection = VectorExtensions.ClampMagnitude(projectileDirection, 1, 1);
        ShootProjectile(projectileDirection , projectilePrefab);
        OnShootedProjectile();
    }

    private void ShootProjectile(Vector2 projectileDirection, Projectile projectilePrefab)
    {
        var projectile = Instantiate(projectilePrefab, _projectileSpawnPosition.position, Quaternion.identity);
        Instantiate(_shootingParticle, _projectileSpawnPosition.position, Quaternion.identity);
        projectile.Initialize(projectileDirection,  _owner,_projectileEffects ,_projectileSpeedMulptiplier,
            _projectileDamageMulptiplier, _projectileAdditiveDamage, _projectileAdditiveSpeed);
    }

    protected void OnShootedProjectile()
    {
        _audioSource.PlayOneShot(_shootSound);
        OnShooted?.Invoke();
    }
}
