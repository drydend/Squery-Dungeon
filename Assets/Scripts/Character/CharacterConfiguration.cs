using System;
using UnityEngine;

[CreateAssetMenu(menuName ="Charcter config")]
public class CharacterConfiguration : ScriptableObject, ICloneable
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
    private float _attackDamage = 1;

    [SerializeField]
    private float _maxHealsPoints;
    [SerializeField]
    private float _invulnerabilityAfterHitDuration = 1f;

    [SerializeField]
    private float _movementSpeed;
    
    [SerializeField]
    private float _attackSpeed = 1f;

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

    public event Action OnMaxHealsChanged;
    public event Action OnAttackSpeedChanged;

    public object Clone()
    {
        var configClone = new CharacterConfiguration();
        configClone._dashDistance = _dashDistance;
        configClone._dashDuration = _dashDuration;
        configClone._dashingPushForce = _dashingPushForce;
        configClone._defaultPushForce = _defaultPushForce;
        configClone._pushingDuration = _pushingDuration;
        configClone._collisionDamage = _collisionDamage;
        configClone._attackDamage = _attackDamage;
        configClone._maxHealsPoints = _maxHealsPoints;
        configClone._invulnerabilityAfterHitDuration = _invulnerabilityAfterHitDuration;
        configClone._movementSpeed = _movementSpeed;
        configClone._attackSpeed = _attackSpeed;

        return configClone;
    }

    public void IncreaseStatValue(StatType statType, float value)
    {
        switch (statType)
        {
            case StatType.AttackDamage:
                _attackDamage += value;
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
}
