using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Charcter config")]
public class CharacterConfiguration : ScriptableObject
{
    [Header("Level")]
    [SerializeField]
    private uint _maxLevel;
    [SerializeField]
    private float _gainedExpMultiplier = 1;

    [Header("Energy")]
    [SerializeField]
    private float _maxEnergy = 100f;
    [SerializeField]
    private float _passiveEnergyRecovery = 30f;

    [Header("Dash")]
    [SerializeField]
    private float _dashCooldown = 1.5f;
    [SerializeField]
    private float _dashDistance = 5f;
    [SerializeField]
    private float _dashDuration = 0.2f;
    [SerializeField]
    private float _dashEnergyCost = 40f;

    [Header("Push forces")]
    [SerializeField]
    private float _dashingPushForce = 8;
    [SerializeField]
    private float _defaultPushForce = 5;
    [SerializeField]
    private float _pushingDuration = 0.2f;
    [Header("Heals")]
    [SerializeField]
    private float _maxHealsPoints;
    [SerializeField]
    private float _invulnerabilityAfterHitDuration = 1f;
    
    [Header("Movement")]
    [SerializeField]
    private float _movementSpeed;

    [Header("Attack")]
    [SerializeField]
    private float _attackEnergyCost = 20f;
    [SerializeField]
    private float _attackCooldown;
    [SerializeField]
    private float _projectileAdditiveDamage;
    [SerializeField]
    private float _projectileAdditiveSpeed;
    [SerializeField]
    private float _projectileDamageMultiplier;
    [SerializeField]
    private float _projectileSpeedMultiplier;
    [SerializeField]
    private float _collisionDamage = 1;

    [SerializeField]
    private RangeWeapon _weaponPrefab;
    [SerializeField]
    private Projectile _projectilePrefab;
    [SerializeField]
    private BulletHitBehaviour _bulletStartHitBehaviour;
    [SerializeField]
    private BulletCollisionBehaviour _bulletStartCollisionBehaviour;
    [SerializeField]
    private List<Effect> _projectileEffects;

    public float MaxLevel => _maxLevel;

    public float MaxEnergy => _maxEnergy;
    public float EnergyRecovery => _passiveEnergyRecovery;

    public float DashDistance => _dashDistance;
    public float DashDuration => _dashDuration;
    public float DashCooldown => _dashCooldown;
    public float DashEnergyCost => _dashEnergyCost;

    public float DashingPushForce => _dashingPushForce;
    public float DefaultPushForce => _dashingPushForce;
    public float PushingDuration => _pushingDuration;

    public float CollisionDamage => _collisionDamage;

    public float MovementSpeed => _movementSpeed;

    public float MaxHealsPoints => _maxHealsPoints;
    public float InvulnerabilityAfterHitDuration => _invulnerabilityAfterHitDuration;

    public float AttackEnergyCost => _attackEnergyCost;
    public float AttackCooldown => _attackCooldown;
    public float ProjectileDamageMultiplier => _projectileDamageMultiplier;
    public float ProjectileAdditiveDamage => _projectileAdditiveDamage;
    public float ProjectileAdditiveSpeed => _projectileAdditiveSpeed;
    public RangeWeapon Weapon => _weaponPrefab;
    public Projectile Projectile => _projectilePrefab;
    public List<Effect> ProjectileEffects => _projectileEffects;

    public float GainedExpMultiplier => _gainedExpMultiplier;

    public event Action<float> OnProjectileAdditiveSpeedChanged;
    public event Action<float> OnProjectileAdditiveDamageChanged;
    public event Action OnMaxHealsChanged;
    public event Action OnAttackSpeedChanged;

    public void Initialize()
    {
        _projectilePrefab.SetHitBehaviour(_bulletStartHitBehaviour);
        _projectilePrefab.SetCollisionBehaviour(_bulletStartCollisionBehaviour);
    }

    public CharacterConfiguration Clone()
    {
        var clonedConfig = (CharacterConfiguration)MemberwiseClone();
        clonedConfig._projectileEffects = new List<Effect>(_projectileEffects);
        return clonedConfig;
    }

    public void AddEffectToProjectile(Effect effect) 
    {
        _projectileEffects.Add(effect);
    }

    public void IncreaseMaxHeals(float value)
    {
        IncreaseStatValue(ref _maxHealsPoints, value);
        OnMaxHealsChanged?.Invoke();
    }

    public void DecreaseMaxHeals(float value)
    {
        DecreaseStatValue(ref _maxHealsPoints, value);
        OnMaxHealsChanged?.Invoke();
    }

    public void IncreaseMovementSpeed(float value)
    {
        IncreaseStatValue(ref _movementSpeed, value);
    }

    public void DecreaseMovementSpeed(float value)
    {
        DecreaseStatValue(ref _movementSpeed, value);
    }

    public void IncreaseDashEnergyCost(float value)
    {
        IncreaseStatValue(ref _dashEnergyCost, value);
    }

    public void DecreaseDashEnergyCost(float value)
    {
        DecreaseStatValue(ref _dashEnergyCost, value);
    }

    public void IncreaseDashCooldown(float value)
    {
        IncreaseStatValue(ref _dashCooldown, value);
    }

    public void DecreaseDashCooldown(float value)
    {
        DecreaseStatValue(ref _dashCooldown, value);
    }

    public void IncreasePassiveEnergyRecovery(float value)
    {
        IncreaseStatValue(ref _passiveEnergyRecovery, value);
    }

    public void DecreasePassiveEnergyRecovery(float value)
    {
        DecreaseStatValue(ref _passiveEnergyRecovery, value);
    }

    public void IncreaseMaxEnergy(float value)
    {
        IncreaseStatValue(ref _maxEnergy, value);
    }

    public void DecreaseMaxEnergy(float value)
    {
        DecreaseStatValue(ref _maxEnergy, value);
    }

    public void IncreaseAttackEnergyCost(float value)
    {
        IncreaseStatValue(ref _attackEnergyCost, value);
    }

    public void DecreaseAttackEnergyCost(float value)
    {
        DecreaseStatValue(ref _attackEnergyCost, value);
    }

    public void IncreaseAttackSpeed(float value)
    {
        DecreaseStatValue(ref _attackCooldown, value);
        OnAttackSpeedChanged?.Invoke();
    }

    public void DecreaseAttackSpeed(float value)
    {
        IncreaseStatValue(ref _attackCooldown, value);
        OnAttackSpeedChanged?.Invoke();
    }
    
    public void IncreaseProjectileDamage(float value)
    {
        IncreaseStatValue(ref _projectileAdditiveDamage, value);
        OnProjectileAdditiveDamageChanged?.Invoke(_projectileAdditiveDamage);
    }
    
    public void DecreaseProjectileDamage(float value)
    {
        DecreaseStatValue(ref _projectileAdditiveDamage, value);
        OnProjectileAdditiveDamageChanged?.Invoke(_projectileAdditiveDamage);
    }

    public void IncreaseProjectileSpeed(float value)
    {
        IncreaseStatValue(ref _projectileAdditiveSpeed, value);
        OnProjectileAdditiveSpeedChanged?.Invoke(_projectileAdditiveSpeed);
    }

    public void DecreaseProjectileSpeed(float value)
    {
        DecreaseStatValue(ref _projectileAdditiveSpeed, value);
        OnProjectileAdditiveSpeedChanged?.Invoke(_projectileAdditiveSpeed);
    }

    public void IncreaseProjectileSpeedMultiplier(float value)
    {
        IncreaseStatValue(ref _projectileSpeedMultiplier, value);
        _weaponPrefab.SetSpeedMulptiplier(_projectileSpeedMultiplier);
    }
    
    public void DecreaseProjectileSpeedMultiplier(float value)
    {
        DecreaseStatValue(ref _projectileSpeedMultiplier, value);
        _weaponPrefab.SetSpeedMulptiplier(_projectileSpeedMultiplier);
    }
    
    public void IncreaseAttackDamageMultiplier(float value)
    {
        DecreaseStatValue(ref _projectileDamageMultiplier, value);
        _weaponPrefab.SetDamageMultiplier(_projectileDamageMultiplier);
    }

    public void DecreaseAttackDamageMultiplier(float value)
    {
        IncreaseStatValue(ref _projectileDamageMultiplier, value);
        _weaponPrefab.SetDamageMultiplier(_projectileDamageMultiplier);
    }

    private void IncreaseStatValue(ref float stat ,float value)
    {
        if(value < 0 )
        {
            throw new Exception("Value must be more than zero");
        }

        stat += value;
    }

    private void DecreaseStatValue(ref float stat, float value)
    {
        if (value < 0)
        {
            throw new Exception("Value must be more than zero");
        }

        stat -= value;
    }

}
