using System;
using System.Collections;
using UnityEngine;

public class RangeWeapon : MonoBehaviour
{   
    [SerializeField]
    private Transform _projectileSpawnPosition;
    [SerializeField]
    private ParticleSystem _shootingParticle;
    [SerializeField]
    private GameObject _projectilePrefab;
    [SerializeField]
    private float _projectileDamage = 1f;
    [SerializeField]
    private float _projedtileSpeed = 20f;
    [SerializeField]
    private GameObject _owner;

    public event Action OnBeginAttack;
    public event Action OnEndAttack;

    public void Attack(Vector3 targetPosition)
    {
        OnBeginAttack?.Invoke();
        var projectileDirection = Vector2.ClampMagnitude(targetPosition - transform.position , 1);
        var projectile = Instantiate(_projectilePrefab, _projectileSpawnPosition.position, Quaternion.identity)
            .GetComponent<Projectile>();
        Instantiate(_shootingParticle, _projectileSpawnPosition.position, Quaternion.identity);
        projectile.Initialize(projectileDirection, _projedtileSpeed, _projectileDamage, _owner);
    }

}
