using System;
using UnityEngine;

public class RangeWeapon : MonoBehaviour
{   
    [SerializeField]
    protected Transform _projectileSpawnPosition;
    [SerializeField]
    protected ParticleSystem _shootingParticle;
    [SerializeField]
    protected float _projectileDamageMulptiplier = 1f;
    [SerializeField]
    protected float _projectileSpeedMulptiplier = 1f;
    [SerializeField]
    protected GameObject _owner;

    public event Action OnShooted;

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
        OnShooted?.Invoke();
        Vector2 projectileDirection = targetPosition - _owner.transform.position;
        projectileDirection = VectorExtensions.ClampMagnitude(projectileDirection, 1, 1);
        ShootProjectile(projectileDirection , projectilePrefab);
    }

    private void ShootProjectile(Vector2 projectileDirection, Projectile projectilePrefab)
    {
        var projectile = Instantiate(projectilePrefab, _projectileSpawnPosition.position, Quaternion.identity);
        Instantiate(_shootingParticle, _projectileSpawnPosition.position, Quaternion.identity);
        projectile.Initialize(projectileDirection,  _owner, _projectileSpeedMulptiplier, _projectileDamageMulptiplier);
    }

    protected void InvokeOnShooted()
    {
        OnShooted?.Invoke();
    }
}
