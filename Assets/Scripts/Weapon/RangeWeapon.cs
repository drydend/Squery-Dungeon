using System;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : MonoBehaviour
{
    [SerializeField]
    private Transform _projectileSpawnPosition;
    [SerializeField]
    private ParticleSystem _shootingParticle;
    [SerializeField]
    private AudioClip _shootSound;
    private List<Effect> _projectileEffects;

    [SerializeField]
    private float _defaultAngleBettweenProjectiles = 15f;
    [SerializeField]
    private int _defaultNumberOfProjectiles = 1;

    private float _projectileDamageMulptiplier = 1f;
    private float _projectileSpeedMulptiplier = 1f;
    private float _projectileAdditiveDamage;
    private float _projectileAdditiveSpeed;
    private GameObject _owner;
    private AudioSource _audioSource;

    public event Action OnShooted;

    public void Initialize(GameObject owner, List<Effect> projectileEffects,
        float projectileAdditiveDamage, float projectileAdditiveSpeed)
    {
        _audioSource = AudioSourceProvider.Instance.GetSoundsSource();
        _owner = owner;
        _projectileEffects = projectileEffects;
        _projectileAdditiveDamage = projectileAdditiveDamage;
        _projectileAdditiveSpeed = projectileAdditiveSpeed;
    }

    public void SetAdditiveSpeed(float value)
    {
        if (value < 0)
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

    public void Attack(Vector3 targetPosition, Projectile projectile)
    {
        Attack(targetPosition, projectile, null, _defaultNumberOfProjectiles, _defaultAngleBettweenProjectiles);
    }

    public void Attack(Vector3 targetPosition, Projectile projectile, Transform target)
    {
        Attack(targetPosition, projectile, target, _defaultNumberOfProjectiles, _defaultAngleBettweenProjectiles);
    }

    public void Attack(Vector3 targetPosition, Projectile projectilePrefab, Transform target,
     int numberOfProjectiles, float angleBetweenProjectiles)
    {
        OnShootedProjectile();
        if (numberOfProjectiles % 2 == 0)
        {
            var directionToTarget = VectorExtensions.ClampMagnitude(targetPosition - transform.position, 1, 1);
            for (int i = 0; i < numberOfProjectiles / 2; i++)
            {
                var projectileDirection = Quaternion.AngleAxis(angleBetweenProjectiles * i + angleBetweenProjectiles / 2,
                    Vector3.forward) * directionToTarget;
                ShootProjectile(projectileDirection, projectilePrefab, target);

                projectileDirection = Quaternion.AngleAxis((angleBetweenProjectiles * i + angleBetweenProjectiles / 2) * -1,
                    Vector3.forward) * directionToTarget;
                ShootProjectile(projectileDirection, projectilePrefab, target);
            }
        }
        else
        {
            var directionToTarget = VectorExtensions.ClampMagnitude(targetPosition - transform.position, 1, 1);
            ShootProjectile(directionToTarget, projectilePrefab, target);
            for (int j = 0; j < (numberOfProjectiles - 1) / 2; j++)
            {
                var projectileDirection = Quaternion.AngleAxis(angleBetweenProjectiles * (j + 1),
                    Vector3.forward) * directionToTarget;
                ShootProjectile(projectileDirection, projectilePrefab, target);

                projectileDirection = Quaternion.AngleAxis(angleBetweenProjectiles * (j + 1) * -1,
                    Vector3.forward) * directionToTarget;
                ShootProjectile(projectileDirection, projectilePrefab, target);
            }
        }
    }

    private void ShootProjectile(Vector2 projectileDirection, Projectile projectilePrefab, Transform target)
    {
        var projectile = Instantiate(projectilePrefab, _projectileSpawnPosition.position, Quaternion.identity);
        Instantiate(_shootingParticle, _projectileSpawnPosition.position, Quaternion.identity);
        projectile.Initialize(projectileDirection, target, _owner, _projectileEffects, _projectileSpeedMulptiplier,
            _projectileDamageMulptiplier, _projectileAdditiveDamage, _projectileAdditiveSpeed);
    }

    protected void OnShootedProjectile()
    {
        _audioSource.PlayOneShot(_shootSound);
        OnShooted?.Invoke();
    }
}
