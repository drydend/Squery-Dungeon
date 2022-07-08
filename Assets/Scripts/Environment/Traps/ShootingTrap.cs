using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTrap : Trap
{
    [SerializeField]
    private RangeWeapon _weapon;
    [SerializeField]
    private Projectile _projectile;
    [SerializeField]
    private float _shootingSpeedPerSecond;
    [SerializeField]
    private List<Effect> _projectileEffects = new List<Effect>(0);
    [SerializeField]
    private Transform _target;
    private float _timeSinceLastShoot;
    private bool _isActive;

    private void Awake()
    { 
        _weapon.Initialize(gameObject, _projectileEffects, 0, 0);
    }

    private void Start()
    {
        Activate();
    }


    private void Update()
    {
        if (!_isActive)
        {
            return;
        }

        _timeSinceLastShoot += Time.deltaTime;

        if(_timeSinceLastShoot >= 1 / _shootingSpeedPerSecond)
        {
            _weapon.Attack(_target.position, _projectile);
            _timeSinceLastShoot -= 1 / _shootingSpeedPerSecond;
        }
    }

    public override void Activate()
    {
        _isActive = true;
        _timeSinceLastShoot = 0;
    }

    public override void Deactivate()
    {
        _isActive = false;
    }
}
