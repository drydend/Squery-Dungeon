using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Charcter config")]
public class CharacterConfiguration : ScriptableObject
{
    [SerializeField]
    private float _dashDistance = 5f;
    [SerializeField]
    private float _dashDuration = 0.2f;

    [SerializeField]
    private float _dashingPushForce = 8;
    [SerializeField]
    private float _defaultPushForce = 5;
    [SerializeField]
    private float _pushingDuration = 0.2f;

    [SerializeField]
    private float _collisionDamage = 1;

    [SerializeField]
    private float _maxHealsPoints;
    [SerializeField]
    private float _invulnerabilityAfterHitDuration = 1f;

    [SerializeField]
    private float _movementSpeed;

    [SerializeField]
    private float _attackSpeed = 1f;
    [SerializeField]
    private float _projectileDamageMultiplier;

    [SerializeField]
    private RangeWeapon _weapon;
    [SerializeField]
    private Projectile _projectile;

    public float DashDistance => _dashDistance;
    public float DashDuration => _dashDuration;

    public float DashingPushForce => _dashingPushForce;
    public float DefaultPushForce => _dashingPushForce;
    public float PushingDuration => _pushingDuration;

    public float CollisionDamage => _collisionDamage;

    public float MovementSpeed => _movementSpeed;

    public float MaxHealsPoints => _maxHealsPoints;
    public float InvulnerabilityAfterHitDuration => _invulnerabilityAfterHitDuration;

    public float AttackSpeed => _attackSpeed;
    public float ProjectileDamageMultiplier => _projectileDamageMultiplier;
    public RangeWeapon Weapon => _weapon;
    public Projectile Projectile => _projectile;

    public event Action OnMaxHealsChanged;
    public event Action OnAttackSpeedChanged;

    public CharacterConfiguration Clone()
    {   
        var clonedConfig = (CharacterConfiguration)MemberwiseClone();
        clonedConfig._projectile = _projectile.Clone();
        Debug.Log(clonedConfig.Equals(this));
        return clonedConfig;
    }

    public void UpgradeBullet(BulletUpgrade bulletUpgrade)
    {
        var bulletUpgradeType = bulletUpgrade.GetType();

        if (bulletUpgradeType == typeof(BulletHitBehaviourUpgrade))
        {
            
        }
        else if (bulletUpgradeType == typeof(BulletCollisionBehaviourUpgrade))
        {
            var bulletCollisionBehaviourUpgrade = (BulletCollisionBehaviourUpgrade)bulletUpgrade;

            UpgradeBulletCollisionBehaviour(bulletCollisionBehaviourUpgrade);
        }
        else
        {
            throw new Exception($"Can`t process this bullet upgrade type: {bulletUpgrade.GetType()}");
        }
    }

    public void IncreaseStatValue(StatType statType, float value)
    {
        switch (statType)
        {
            case StatType.AttackDamage:
                _projectile.SetBaseDamage(_projectile.Damage + value);
                break;
            case StatType.AttackSpeed:
                _attackSpeed -= value;
                OnAttackSpeedChanged?.Invoke();
                break;
            case StatType.Heals:
                _maxHealsPoints += value;
                OnMaxHealsChanged?.Invoke();
                break;
            case StatType.Speed:
                _movementSpeed += value;
                break;
            default:
                throw new Exception("Can`t process this stat type: " + statType);
        }
    }

    private void UpgradeBulletCollisionBehaviour(BulletCollisionBehaviourUpgrade upgrade)
    {
        if (upgrade.NewCollisionBehaviour.GetType() == typeof(RicochetCollisionBehaviour))
        {
            if (_projectile.HitBehaviour.GetType() == typeof(BulletExplosiveHitBehaviour))
            {
                _projectile.SetCollisionBehaviour(new RicochetExplosiveCollisionBehaviour(
                    (BulletExplosiveHitBehaviour)_projectile.HitBehaviour,
                    (RicochetCollisionBehaviour)upgrade.NewCollisionBehaviour));
                _projectile.SetCollisionParticle(_projectile.CollisionParticle);
            }
            else
            {
                _projectile.SetCollisionBehaviour(upgrade.NewCollisionBehaviour);
                _projectile.SetCollisionParticle(upgrade.CollisionParticle);
            }
        }
        else
        {
            _projectile.SetCollisionBehaviour(upgrade.NewCollisionBehaviour);
            _projectile.SetCollisionParticle(upgrade.CollisionParticle);
        }
    }

    private void UpgradeBulletHitBehaviour(BulletHitBehaviourUpgrade upgrade)
    {

    }
}
