using System;
using System.Collections;
using UnityEngine;

public class RangeWeapon : MonoBehaviour
{
    [SerializeField]
    private Character _owner;
    [SerializeField]
    private Transform _attackDirection;
    [SerializeField]
    private ParticleSystem _shootingParticle;
    [SerializeField]
    private GameObject _projectilePrefab;
    [SerializeField]
    private float _projectileDamage = 1f;
    [SerializeField]
    private float _projedtileSpeed = 20f;

    public event Action OnBeginAttack;
    public event Action OnEndAttack;

    public void Attack()
    {
        OnBeginAttack.Invoke();
        var projectileDirection = - _attackDirection.right;
        var projectile = Instantiate(_projectilePrefab, _attackDirection.position, Quaternion.identity)
            .GetComponent<Projectile>();
        Instantiate(_shootingParticle, _attackDirection.position, Quaternion.identity);
        projectile.Initialize(projectileDirection, _projedtileSpeed, _projectileDamage, _owner);
    }

}
