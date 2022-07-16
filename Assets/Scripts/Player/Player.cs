using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerInput _input;
    [SerializeField]
    private CharacterConfiguration _startConfig;
    [SerializeField]
    private Character _currentCharacter;
    private CharacterConfiguration _characterConfig;
    
    [SerializeField]
    private AudioClip _powerUPApplyingSound;

    private float _currentEnergy = 0;

    private Timer _attackTimer;
    private Timer _dashTimer;

    private AudioSource _audioSource;

    public event Action OnCharacterDied;
    public event Action OnCharacterEndedDashing;
    public event Action OnCharacterHealsChanged;
    public event Action OnCharacterMaxHealsChanged;

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
        _audioSource = GetComponent<AudioSource>();
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
            Attack();
        }
    }

    private void OnEnable()
    {   
        OnCharacterHealsChanged += () => CameraShaker.Instance.ShakeCamera(0.2f, 0.3f);
        _currentCharacter.OnDied += () => OnCharacterDied?.Invoke();
        _currentCharacter.OnEndedDash += () => OnCharacterEndedDashing?.Invoke();
        _currentCharacter.OnHealsChanged +=() => OnCharacterHealsChanged?.Invoke();
        _characterConfig.OnMaxHealsChanged += () => OnCharacterMaxHealsChanged?.Invoke();
        _input.DashButton.performed += Dash;
        _input.AttackButton.performed += Attack;
    }

    private void OnDisable()
    {
        _input.DashButton.performed -= Dash;
        _input.AttackButton.performed -= Attack;
    }

    public void ApplyPowerUP(Upgrade powerUP)
    {
        _audioSource.PlayOneShot(_powerUPApplyingSound);
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

    private void Attack(InputAction.CallbackContext inputAction)
    {
        Attack();
    }

    private void Attack()
    {
        if (_attackTimer.IsFinished && _currentEnergy > _characterConfig.AttackEnergyCost)
        {
            _currentCharacter.Attack(_input.CurrentMousePoisition);
            _attackTimer.ResetTimer();
            SpendEnergy(_characterConfig.AttackEnergyCost);
        }
    }

    private void Move(Vector2 direction)
    {
        _currentCharacter.Move(_input.PlayerMoveDirection);
    }

    private void Dash(InputAction.CallbackContext inputAction)
    {
        Dash();
    }

    private void Dash()
    {
        if ((Vector2)_input.PlayerMoveDirection == Vector2.zero)
        {
            return;
        }

        if (_dashTimer.IsFinished && _currentEnergy > _characterConfig.DashEnergyCost)
        {
            _currentCharacter.Dash(_input.PlayerMoveDirection);
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
