using UnityEngine;
using System.Collections.Generic;

public class CrateWithSpikes : Crate
{
    [SerializeField]
    private RangeWeapon _weapon;
    [SerializeField]
    private Projectile _projectile;
    [SerializeField]
    private List<Effect> _projectileEffects;
    private Vector3 _attackDirection = new Vector3(0.707f, 0.707f, 0);

    private new void Awake()
    {
        base.Awake();
        _weapon.Initialize(gameObject, _projectileEffects, 0, 0);
    }

    protected override void Destruct()
    {
        _weapon.Attack(transform.position + _attackDirection, _projectile);
        base.Destruct();
    }

}
