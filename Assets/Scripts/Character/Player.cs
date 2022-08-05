using System;
using UnityEngine;
using UnityEngine.InputSystem;

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
    private AudioClip _levelUpSound;

    private float _currentEnergy = 0;
    private int _currentLevel = 0;
    private int _currentExp = 0;
    private int _expToNextLevel;

    private Timer _attackTimer;
    private Timer _dashTimer;

    private AudioSource _audioSource;

    public event Action OnLevelUp;
    public event Action OnLevelChanged;
    public event Action OnCurrentExpChanged;

    public event Action OnDontHaveEnoungtEnergy;

    public event Action OnCharacterDied;
    public event Action OnCharacterEndedDashing;
    public event Action OnCharacterHealsChanged;
    public event Action OnCharacterMaxHealsChanged;

    public int CurrentExp => _currentExp;
    public int ExpToNextLevel => _expToNextLevel;

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
        _audioSource = AudioSourceProvider.Instance.GetSoundsSource();
        _characterConfig = _startConfig.Clone();
        _characterConfig.Initialize();
        _currentCharacter.Initialize(_characterConfig);

        _attackTimer = new Timer(_characterConfig.AttackCooldown);
        _dashTimer = new Timer(_characterConfig.DashCooldown);

        _attackTimer.FinishTimer();
        _dashTimer.FinishTimer();

        _characterConfig.OnAttackSpeedChanged += () => _attackTimer.SetSecondsToFinish(_characterConfig.AttackCooldown);
        _characterConfig.OnDashCooldownChanged += () => _dashTimer.SetSecondsToFinish(_characterConfig.DashCooldown);

        _currentEnergy = MaxEnergy;
        _expToNextLevel =  GenerateExpToNextLevel();
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
        _currentCharacter.OnHealsChanged += () => OnCharacterHealsChanged?.Invoke();
        _characterConfig.OnMaxHealsChanged += () => OnCharacterMaxHealsChanged?.Invoke();
        _input.DashButton.started += Dash;
        _input.AttackButton.started += Attack;
    }

    private void OnDisable()
    {
        _input.DashButton.performed -= Dash;
        _input.AttackButton.performed -= Attack;
    }

    public void ApplyPowerUP(PlayerUpgrade upgrade)
    {
        upgrade.ApplyUpgrade(this);
    }

    public void RevertPowerUP(PlayerUpgrade upgrade) 
    {

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
        if (_attackTimer.IsFinished)
        {   
            if(_currentEnergy < _characterConfig.AttackEnergyCost)
            {
                OnDontHaveEnoungtEnergy?.Invoke();
                return;
            }

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

    public void ReceiveExp(int value)
    {
        if (value < 0)
        {
            throw new Exception("Value must be more than zero");
        }

        if (_currentLevel == _characterConfig.MaxLevel)
        {
            return;
        }

        _currentExp += Mathf.RoundToInt(value * _characterConfig.GainedExpMultiplier);
        bool levelChanged = false;

        while (_currentExp >= _expToNextLevel)
        {
            _currentExp -= _expToNextLevel;
            _currentLevel++;
            
            _expToNextLevel = GenerateExpToNextLevel();
            
            OnLevelChanged?.Invoke();
            levelChanged = true;
        }

        if (levelChanged)
        {
            OnLevelUp?.Invoke();
            _audioSource.PlayOneShot(_levelUpSound);
        }

        OnCurrentExpChanged?.Invoke();
    }

    public void RecieveEnergy(float value)
    {
        if (_currentEnergy >= MaxEnergy)
        {
            return;
        }

        if (value < 0)
        {
            throw new Exception("Value must be more than zero");
        }

        _currentEnergy = _currentEnergy + value > MaxEnergy ? MaxEnergy : _currentEnergy + value;
    }

    private void SpendEnergy(float value)
    {
        if (value < 0)
        {
            throw new Exception("Value must be more than zero");
        }

        _currentEnergy = _currentEnergy - value > 0 ? _currentEnergy - value : 0;
    }

    private int GenerateExpToNextLevel()
    {
        return 2 * _currentLevel + 5;
    }
}
