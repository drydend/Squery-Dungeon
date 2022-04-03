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
    [SerializeField]
    private CameraShaker _cameraShaker;
    private CharacterConfiguration _characterConfig;

    public event Action OnCurrentCharacterHealsChanged;
    public event Action OnCurrentCharacterMaxHealsChanged;

    public float MaxHealsPoints => _characterConfig.MaxHealsPoints;
    public float CurrentHealsPoints => _currentCharacter.CurrentHealsPoints;

    public Character CurrentCharacter => _currentCharacter;
    public Transform CharacterTransform => _currentCharacter.transform;

    private void Awake()
    {
        _characterConfig = (CharacterConfiguration)_startConfig.Clone();
        _currentCharacter.Initialize(_characterConfig);
    }

    private void FixedUpdate()
    {
        if (PauseMenager.Instance.IsPaused)
            return;

        _currentCharacter.Move(_input.PlayerMoveDirection);
        _currentCharacter.transform.LookAt2D(_input.CurrentMousePoisition);
        if (_input.IsAttackButtonBeingHolded)
        {
            _currentCharacter.Attack(_input.CurrentMousePoisition);
        }
    }

    private void OnEnable()
    {
        OnCurrentCharacterHealsChanged += () => _cameraShaker.ShakeCamera(0.2f, 0.3f);
        _currentCharacter.OnHealsChanged +=() => OnCurrentCharacterHealsChanged?.Invoke();
        _characterConfig.OnMaxHealsChanged += () => OnCurrentCharacterMaxHealsChanged?.Invoke();
        _input.DashButton.performed += (context) => _currentCharacter.Dash(_input.PlayerMoveDirection);
        _input.AttackButton.performed += (context) => _currentCharacter.Attack(_input.CurrentMousePoisition);
    }

    private void OnDisable()
    {
        _currentCharacter.OnHealsChanged -= OnCurrentCharacterHealsChanged.Invoke;
        _input.AttackButton.performed -= (context) => _currentCharacter.Attack(_input.CurrentMousePoisition);
        _input.DashButton.performed -= (context) => _currentCharacter.Dash(_input.PlayerMoveDirection);
    }

    public void ApplyPowerUp(PowerUp powerUp)
    {
        if(powerUp.GetType() == typeof(StatUpgrade))
        {
            var statUpgrade = powerUp as StatUpgrade;
            _characterConfig.IncreaseStatValue(statUpgrade.StatType, statUpgrade.Value);
        }
    }

    public void SetCharacterPosition(Vector2 position)
    {
        _currentCharacter.transform.position = position;
    }
}
