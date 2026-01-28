using UnityEngine;
using UnityEngine.InputSystem;

public class InputMananger : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;

    [Header("Performed Actions")]
    public System.Action OnJump;
    public System.Action<float> OnMove;
    public System.Action JumpRelease;
    public System.Action<float> OnRunning;

    
    void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();
    }

    void OnEnable()
    {
        _playerInputActions.Player.Jump.performed += OnJumpPressed;
        _playerInputActions.Player.Jump.canceled += OnJumpRelease;
        _playerInputActions.Player.Run.performed += OnRunPressed;
        //_playerInputActions.Player.Horizontal.performed += OnMovement;
    }

    void OnDisable()
    {
        _playerInputActions.Player.Jump.performed -= OnJumpPressed;
        _playerInputActions.Player.Jump.canceled -= OnJumpRelease;
        _playerInputActions.Player.Run.performed -= OnRunPressed;
        //_playerInputActions.Player.Horizontal.performed -= OnMovement;

    }

    void OnJumpPressed(InputAction.CallbackContext context)
    {
        OnJump?.Invoke();       // If the "OnJump" has listener, it will invoke.
        //Debug.Log("Jump pressed");
    }
    
    void OnMovement()
    {
        OnMove?.Invoke(_playerInputActions.Player.Horizontal.ReadValue<float>());       // If the "OnMove" has listener, it will invoke | Context: 
        //Debug.Log($"Movement pressed.");
    }

    void OnJumpRelease(InputAction.CallbackContext context)
    {
        JumpRelease?.Invoke();
        //Debug.Log("Jump Canceled.");
    }

    void OnRunPressed(InputAction.CallbackContext context)
    {
        OnRunning?.Invoke(_playerInputActions.Player.Horizontal.ReadValue<float>());
        Debug.Log("Running Pressed.");
    }
    
    void Update()
    {
        OnMovement();
    }
}

    