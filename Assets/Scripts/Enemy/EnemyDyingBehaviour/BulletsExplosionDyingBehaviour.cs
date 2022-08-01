using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyDuingBehaviour/Bullets Explosion", fileName = "Bullets Explosion")]
public class BulletsExplosionDyingBehaviour : EnemyDyingBehaviour
{
    [SerializeField]
    private List<Effect> _projectileEffects;
    [SerializeField]
    private Projectile _projectile;
    [SerializeField]
    private int _numberOfProjectiles = 1;
    [SerializeField]
    private float _angleBetweenProjectiles = 0;
    [SerializeField]
    private RangeWeapon _weaponPrefab;

    private RangeWeapon _weapon;

    public override void Initialize(Enemy enemy)
    {
        base.Initialize(enemy);
        _weapon = Instantiate(_weaponPrefab, enemy.Transform);
        _weapon.Initialize(_enemy.gameObject, _projectileEffects, 0, 0);
    }

    protected override void OnEnemyDied()
    {
        base.OnEnemyDied();
        var shootingAngle = Quaternion.AngleAxis(_enemy.Transform.rotation.eulerAngles.z, Vector3.forward) * Vector3.left;
        _weapon.Attack(_enemy.transform.position + shootingAngle, _projectile, _numberOfProjectiles, _angleBetweenProjectiles);
    }
}

