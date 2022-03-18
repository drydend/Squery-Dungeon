using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleRangeWeapon : RangeWeapon
{
    [SerializeField]
    private int _numberOfProjectiles;
    [SerializeField]
    private float _angleBetweenProjectiles;

    public override void Attack(Vector3 targetPosition)
    {
        InvokeOnShooted();
        if (_numberOfProjectiles % 2 == 0)
        {
            var directionToTarget = Vector2.ClampMagnitude(targetPosition - transform.position, 1);
            for (int i = 0; i < _numberOfProjectiles / 2; i++)
            {
                var projectileDirection = Quaternion.AngleAxis(_angleBetweenProjectiles * i  + _angleBetweenProjectiles / 2,
                    Vector3.forward) * directionToTarget;
                ShootProjectile(projectileDirection);

                projectileDirection = Quaternion.AngleAxis((_angleBetweenProjectiles  * i  + _angleBetweenProjectiles / 2) *-1,
                    Vector3.forward) * directionToTarget;
                ShootProjectile(projectileDirection);
            }
        }
        else
        {
            var directionToTarget = Vector2.ClampMagnitude(targetPosition - transform.position, 1);
            ShootProjectile(directionToTarget);
            for (int j = 0; j < (_numberOfProjectiles - 1) / 2; j++)
            {
                var projectileDirection = Quaternion.AngleAxis(_angleBetweenProjectiles * (j + 1),
                    Vector3.forward) * directionToTarget;
                ShootProjectile(projectileDirection);

                projectileDirection = Quaternion.AngleAxis(_angleBetweenProjectiles * (j + 1) * -1,
                    Vector3.forward) * directionToTarget;
                ShootProjectile(projectileDirection);
            }
        }
    }

    private void ShootProjectile(Vector2 projectileDirection)
    {
        var projectile = Instantiate(_projectilePrefab, _projectileSpawnPosition.position, Quaternion.identity);
        projectile.Initialize(projectileDirection, _projedtileSpeed, _projectileDamage, _owner);
    }
}
