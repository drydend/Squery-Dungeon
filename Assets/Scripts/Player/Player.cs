using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerInput _input;
    [SerializeField]
    private CharacterConfiguration _startConfig;
    [SerializeField]
    private Character _currentCharacter;
    private CharacterConfiguration _characterConfig;

    private float _currentEnergy = 0;

    private Timer _attackTimer;
    private Timer _dashTimer;

    public event Action OnCurrentCharacterEndedDashing;
    public event Action OnCurrentCharacterHealsChanged;
    public event Action OnCurrentCharacterMaxHealsChanged;

    public float CurrentEnergy => _currentEnergy;
    public float MaxEnergy => _characterConfig.MaxEnergy;

    public float MaxHealsPoints => _characterConfig.MaxHealsPoints;
    public float CurrentHealsPoints => _currentCharacter.CurrentHealsPoints;

    public float DashCooldownTime => _characterConfig.DashCooldown;
    public float CurrentDashColldown => _dashTimer.SecondsPassed;

    public CharacterConfiguration CharacterConfig => _characterConfig;
    public Character CurrentCharacter => _currentCharacter;
    public Transform CharacterTransform => _currentCharacter.transform;

    private void Awake()
    {
        _characterConfig = _startConfig.Clone();
        _characterConfig.Initialize();
        _currentCharacter.Initialize(_characterConfig);

        _attackTimer = new Timer(_characterConfig.AttackCooldown);
        _dashTimer = new Timer(_characterConfig.DashCooldown);

        _attackTimer.FinishTimer();
        _dashTimer.FinishTimer();

        _characterConfig.OnAttackSpeedChanged += () => _attackTimer.SetSecondsToFinish(_characterConfig.AttackCooldown);

        _currentEnergy = MaxEnergy;
    }

    private void Update()
    {
        _attackTimer.UpdateTick(Time.deltaTime);
        _dashTimer.UpdateTick(Time.deltaTime);

        RecieveEnergy(_characterConfig.EnergyRecovery * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (PauseMenager.Instance.IsPaused)
            return;

        Move(_input.PlayerMoveDirection);
        _currentCharacter.transform.LookAt2D(_input.CurrentMousePoisition);
        
        if (_input.IsAttackButtonBeingHolded)
        {
            Attack(_input.CurrentMousePoisition);
        }
    }

    private void OnEnable()
    {   
        OnCurrentCharacterHealsChanged += () => CameraShaker.Instance.ShakeCamera(0.2f, 0.3f);
        _currentCharacter.OnEndedDash += () => OnCurrentCharacterEndedDashing?.Invoke();
        _currentCharacter.OnHealsChanged +=() => OnCurrentCharacterHealsChanged?.Invoke();
        _characterConfig.OnMaxHealsChanged += () => OnCurrentCharacterMaxHealsChanged?.Invoke();
        _input.DashButton.performed += (context) => Dash(_input.PlayerMoveDirection);
        _input.AttackButton.performed += (context) => Attack(_input.CurrentMousePoisition);
    }


    public void ApplyPowerUP(Upgrade powerUP)
    {
        powerUP.ApplyUpgrade(this);
    }

    public void RevertPowerUP(Upgrade powerUp) { }

    public void RecieveEnergy(float value)
    {
        if(_currentEnergy >= MaxEnergy)
        {
            return;
        }

        if (value < 0)
        {
            throw new Exception("Value must be more than zero");
        }

        _currentEnergy = _currentEnergy + value > MaxEnergy ? MaxEnergy : _currentEnergy + value;
    }

    public void AddEffectToProjectile(Effect effect)
    {
        _characterConfig.AddEffectToProjectile(effect);
    }

    public void SetCharacterPosition(Vector2 position)
    {
        _currentCharacter.transform.position = position;
    }

    private void Attack(Vector2 targetPosition)
    {
        if (_attackTimer.IsFinished && _currentEnergy > _characterConfig.AttackEnergyCost)
        {
            _currentCharacter.Attack(targetPosition);
            _attackTimer.ResetTimer();
            SpendEnergy(_characterConfig.AttackEnergyCost);
        }
    }

    private void Move(Vector2 direction)
    {
        _currentCharacter.Move(_input.PlayerMoveDirection);
    }

    private void Dash(Vector2 direction)
    {   
        if(direction == Vector2.zero)
        {
            return;
        }

        if (_dashTimer.IsFinished && _currentEnergy > _characterConfig.DashEnergyCost)
        {
            _currentCharacter.Dash(direction);
            _dashTimer.ResetTimer();
            SpendEnergy(_characterConfig.DashEnergyCost);
        }
    }

    private void SpendEnergy(float value)
    {
        if(value < 0)
        {
            throw new Exception("Value must be more than zero");
        }

        _currentEnergy = _currentEnergy - value > 0 ? _currentEnergy - value : 0;
    }
}
