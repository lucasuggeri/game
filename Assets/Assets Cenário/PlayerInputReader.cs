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

  
    private InputActionAsset _runtimeActions;

    private void Awake()
    {
        _runtimeActions = Instantiate(inputActions);

        _playerMap = _runtimeActions.FindActionMap("Player", true);
        _move = _playerMap.FindAction("Move", true);
        _interact = _playerMap.FindAction("Interact", true);
    }

    // true apenas no frame em que apertou
    public bool InteractPressed => _interact.WasPressedThisFrame();

    // true enquanto estiver segurando
    public bool InteractHeld => _interact.IsPressed();

    // true apenas no frame em que soltou
    public bool InteractReleased => _interact.WasReleasedThisFrame();
    private void OnDestroy()
    {
        if (_runtimeActions != null)
            Destroy(_runtimeActions);
    }
    private void OnEnable()
    {
        _move.performed += OnMove;
        _move.canceled += OnMove;
        _playerMap.Enable();
    }

    private void OnDisable()
    {
        _move.performed -= OnMove;
        _move.canceled -= OnMove;
        _playerMap.Disable();
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        MoveInput = ctx.ReadValue<Vector2>();
    }

}
