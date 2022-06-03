using UnityEngine;

public class MultipleRangeWeapon : RangeWeapon
{
    [SerializeField]
    private int _numberOfProjectiles;
    [SerializeField]
    private float _angleBetweenProjectiles;

    public override void Attack(Vector3 targetPosition, Projectile projectilePrefab)
    {
        InvokeOnShooted();
        if (_numberOfProjectiles % 2 == 0)
        {
            var directionToTarget = VectorExtensions.ClampMagnitude(targetPosition - transform.position, 1, 1);
            for (int i = 0; i < _numberOfProjectiles / 2; i++)
            {
                var projectileDirection = Quaternion.AngleAxis(_angleBetweenProjectiles * i + _angleBetweenProjectiles / 2,
                    Vector3.forward) * directionToTarget;
                ShootProjectile(projectileDirection, projectilePrefab);

                projectileDirection = Quaternion.AngleAxis((_angleBetweenProjectiles * i + _angleBetweenProjectiles / 2) * -1,
                    Vector3.forward) * directionToTarget;
                ShootProjectile(projectileDirection, projectilePrefab);
            }
        }
        else
        {
            var directionToTarget = VectorExtensions.ClampMagnitude(targetPosition - transform.position, 1, 1);
            ShootProjectile(directionToTarget, projectilePrefab);
            for (int j = 0; j < (_numberOfProjectiles - 1) / 2; j++)
            {
                var projectileDirection = Quaternion.AngleAxis(_angleBetweenProjectiles * (j + 1),
                    Vector3.forward) * directionToTarget;
                ShootProjectile(projectileDirection, projectilePrefab);

                projectileDirection = Quaternion.AngleAxis(_angleBetweenProjectiles * (j + 1) * -1,
                    Vector3.forward) * directionToTarget;
                ShootProjectile(projectileDirection, projectilePrefab);
            }
        }
    }

    private void ShootProjectile(Vector2 projectileDirection, Projectile projectilePrefab)
    {
        var projectile = Instantiate(projectilePrefab, _projectileSpawnPosition.position, Quaternion.identity);
        projectile.Initialize(projectileDirection, _owner, _projectileEffects,
            _projectileSpeedMulptiplier, _projectileDamageMulptiplier,
            _projectileAdditiveDamage, _projectileAdditiveSpeed);
    }
}
