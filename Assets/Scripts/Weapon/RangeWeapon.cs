using System;
using System.Collections;
using UnityEngine;

public class RangeWeapon : MonoBehaviour
{   
    [SerializeField]
    protected Transform _projectileSpawnPosition;
    [SerializeField]
    protected ParticleSystem _shootingParticle;
    [SerializeField]
    protected Projectile _projectilePrefab;
    [SerializeField]
    protected float _projectileDamage = 1f;
    [SerializeField]
    protected float _projedtileSpeed = 20f;
    [SerializeField]
    protected GameObject _owner;

    public event Action OnShooted;

    public virtual void Attack(Vector3 targetPosition)
    {
        OnShooted?.Invoke();
        var projectileDirection = Vector2.ClampMagnitude(targetPosition - transform.position , 1);
        ShootProjectile(projectileDirection);
    }

    private void ShootProjectile(Vector2 projectileDirection)
    {
        var projectile = Instantiate(_projectilePrefab, _projectileSpawnPosition.position, Quaternion.identity);
        Instantiate(_shootingParticle, _projectileSpawnPosition.position, Quaternion.identity);
        projectile.Initialize(projectileDirection, _projedtileSpeed, _projectileDamage, _owner);
    }

    protected void InvokeOnShooted()
    {
        OnShooted?.Invoke();
    }
}
