using System;
using UnityEngine;

public class Player : MonoBehaviour , IDamageable
{ 
    [SerializeField]
    private float _maxHealsPoints;
    private float _currentHealsPoints;

    private CharacterTypeController _characterTypeController;
    private Character _currentCharacter;

    public Character CurrentCharacter => _currentCharacter;
    public Transform CharacterTransform => _currentCharacter.transform;
    
    public void Initialize(CharacterTypeController characterTypeController)
    {
        _characterTypeController = characterTypeController;
        _currentCharacter = _characterTypeController.CurrentCharacter;
    }

    public void IncreaseCharactersStat(CharacterType characterType, StatType statType, float value)
    {

    }

    public void DecreaseCharactersStat(CharacterType characterType, StatType statType, float value)
    {

    }

    public void SetCharacterPosition(Vector2 position)
    {
        _currentCharacter.transform.position = position;
    }

    public virtual void RecieveDamage(float damage, GameObject sender)
    {
        if (damage >= 0)
        {
            _currentHealsPoints -= damage;
        }
        else
        {
            throw new Exception("Incorrect damage");
        }
    }

    private void Awake()
    {
        _currentHealsPoints = _maxHealsPoints;
    }

    private void FixedUpdate()
    {
        _currentCharacter.Move(PlayerInput.Instance.PlayerMoveDirection);
        _currentCharacter.LookAt(PlayerInput.Instance.CurrentMousePoisition);
        if (PlayerInput.Instance.IsAttackButtonBeingHolded)
        {
            _currentCharacter.Attack();
        }
    }

    private void OnEnable()
    {
        PlayerInput.Instance.AttackButton.performed += (context) => _currentCharacter.Attack();
        PlayerInput.Instance.ChangeCharacterToTriangle.performed += (context) =>
            _characterTypeController.TrySetCharacter(CharacterType.Triangle, out _currentCharacter);
        PlayerInput.Instance.ChangeCharacterToSquare.performed += (context) =>
            _characterTypeController.TrySetCharacter(CharacterType.Square, out _currentCharacter);
        PlayerInput.Instance.ChangeCharacterToPentagon.performed += (context) =>
            _characterTypeController.TrySetCharacter(CharacterType.Pentagon, out _currentCharacter);
    }

    private void OnDisable()
    {
        PlayerInput.Instance.ChangeCharacterToTriangle.performed -= (context) =>
            _characterTypeController.TrySetCharacter(CharacterType.Triangle, out _currentCharacter);
        PlayerInput.Instance.ChangeCharacterToSquare.performed -= (context) =>
            _characterTypeController.TrySetCharacter(CharacterType.Square, out _currentCharacter);
        PlayerInput.Instance.ChangeCharacterToPentagon.performed -= (context) =>
            _characterTypeController.TrySetCharacter(CharacterType.Pentagon, out _currentCharacter);
    }
}
