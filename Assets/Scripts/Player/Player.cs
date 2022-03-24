using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerInput _input;
    [SerializeField]
    private Character _currentCharacter;

    public Character CurrentCharacter => _currentCharacter;
    public Transform CharacterTransform => _currentCharacter.transform;

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
        _input.DashButton.performed += (context) => _currentCharacter.Dash(_input.PlayerMoveDirection);
        _input.AttackButton.performed += (context) => _currentCharacter.Attack(_input.CurrentMousePoisition);
    }

    private void OnDisable()
    {
        _input.AttackButton.performed -= (context) => _currentCharacter.Attack(_input.CurrentMousePoisition);
        _input.DashButton.performed -= (context) => _currentCharacter.Dash(_input.PlayerMoveDirection);
    }

    public void IncreaseCharacterStat(StatType statType, float value)
    {

    }

    public void DecreaseCharacterStat(StatType statType, float value)
    {

    }

    public void SetCharacterPosition(Vector2 position)
    {
        _currentCharacter.transform.position = position;
    }
}
