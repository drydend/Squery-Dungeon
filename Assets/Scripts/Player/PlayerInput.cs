using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance;

    private InputBinds _input;
    private Camera _camera;
    private Vector3 _currentMousePosition;
    private Vector3 _playerMoveDirection;
    private bool _isAttackButtonHolded;

    public bool IsAttackButtonBeingHolded => _isAttackButtonHolded;
    public Vector3 PlayerMoveDirection => _playerMoveDirection;
    public Vector3 CurrentMousePoisition => _currentMousePosition;
    public InputAction AttackButton => _input.Character.ChangeCharacterToTriangle;
    public InputAction ChangeCharacterToTriangle => _input.Character.ChangeCharacterToTriangle;
    public InputAction ChangeCharacterToSquare => _input.Character.ChangeCharacterToSquare;
    public InputAction ChangeCharacterToPentagon => _input.Character.ChangeCharacterToPentagon;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            throw new Exception("PlayerInput can be only one");
        }

        _camera = Camera.main;
        _input = new InputBinds();
        _input.Character.Attack.performed += (context) =>_isAttackButtonHolded = true;
        _input.Character.Attack.canceled += (context) => _isAttackButtonHolded = false;
    }

    private void Update()
    {
        _playerMoveDirection = _input.Character.Move.ReadValue<Vector2>();
        var mousePositionInPixels = _input.Character.Mouse.ReadValue<Vector2>();
        _currentMousePosition = _camera.ScreenToWorldPoint(mousePositionInPixels);
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }
}
