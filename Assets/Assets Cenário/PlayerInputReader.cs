using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerInputReader : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;

    private InputActionMap _playerMap;
    private InputAction _move;
    private InputAction _interact;
    public Vector2 MoveInput { get; private set; }
    public bool InteractPressed { get; private set; }
    public bool InteractJustPressed => _interact.WasPressedThisFrame();
    private void Awake()
    {
        _playerMap = inputActions.FindActionMap("Player", throwIfNotFound: true);
        _move = _playerMap.FindAction("Move", throwIfNotFound: true);
        _interact = _playerMap.FindAction("Interact", throwIfNotFound: true);


    }

    private void Update()
    {

    }
    private void OnEnable()
    {
        _move.performed += OnMove;
        _move.canceled += OnMove;
        _interact.started += OnInteract;
        _interact.canceled += OnInteract;
        _playerMap.Enable();
    }

    private void OnDisable()
    {
        _move.performed -= OnMove;
        _move.canceled -= OnMove;
        _playerMap.Disable();

        _interact.started -= OnInteract;
        _interact.canceled -= OnInteract;

    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        MoveInput = ctx.ReadValue<Vector2>();
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {
        // started = apertou, canceled = soltou
        InteractPressed = ctx.started;
    }
}
