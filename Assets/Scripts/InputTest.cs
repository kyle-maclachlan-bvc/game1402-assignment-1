using UnityEngine;
using UnityEngine.InputSystem;

public class InputTest : MonoBehaviour
{
    private PlayerInputActions _controllerAction;

    void Awake()
    {
        _controllerAction = new PlayerInputActions();           // we created an object
        
    }

    void OnEnable()
    {
        _controllerAction.Enable();                             // We turned on the function to listen to key inputs.
        _controllerAction.Player.Jump.performed += Jump;        // reads to Jump
        _controllerAction.Player.Horizontal.performed += Move;        // reads to Move
        _controllerAction.Player.Horizontal.canceled += MoveCanceled; // reads to cancel movement
    }

    void OnDisable()
    {
        _controllerAction.Player.Jump.performed -= Jump;        // turns off Jump
        _controllerAction.Player.Horizontal.performed -= Move;        // turns off movement
        _controllerAction.Player.Horizontal.canceled -= MoveCanceled; // turns off cancel movement
        _controllerAction.Disable();                            // We turn off the function to listen to key inputs.
    }

    void Jump(InputAction.CallbackContext ctx)
    {
        Debug.Log("jump");
    }

    void Move(InputAction.CallbackContext ctx)
    {
        Debug.Log("move");
    }

    void MoveCanceled(InputAction.CallbackContext ctx)
    {
        Debug.Log("moveCanceled");
    }
}
