using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour, IPausable
{
    private InputBinds _input;
    private Camera _camera;
    private Vector3 _currentMousePosition;
    private Vector3 _playerMoveDirection;
    private bool _isAttackButtonHolded;

    public bool IsAttackButtonBeingHolded => _isAttackButtonHolded;
    public Vector3 PlayerMoveDirection => _playerMoveDirection;
    public Vector3 CurrentMousePoisition => _currentMousePosition;
    public InputAction AttackButton => _input.Character.Attack;
    public InputAction DashButton => _input.Character.Dash;

    public InputAction EButton => _input.UI.EButton;
    public InputAction EscapeButton => _input.UI.PauseOrUnpause;

    private void Awake()
    {
        _camera = Camera.main;
        _input = new InputBinds();

        _input.Character.Attack.performed += (context) =>_isAttackButtonHolded = true;
        _input.Character.Attack.canceled += (context) => _isAttackButtonHolded = false;
    }

    private void Start()
    {
        PauseMenager.Instance.Register(this);
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

    public void Pause()
    {
        _input.Character.Disable();
    }

    public void UnPause()
    {
        _input.Character.Enable();
    }
}
