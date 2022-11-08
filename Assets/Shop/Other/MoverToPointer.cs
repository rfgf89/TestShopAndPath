using UnityEngine;
using UnityEngine.InputSystem;

public class MoverToPointer : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    
    private const string INPUT_ACTION = "Point";
    private InputAction _inputAction;
    private void Start()
    {
        _inputAction = _playerInput.currentActionMap[INPUT_ACTION];
        _inputAction.Enable();

        _inputAction.performed += Move;
        
    }

    private void Move(InputAction.CallbackContext obj)
    {
        transform.position = obj.ReadValue<Vector2>();
    }

    private void OnDestroy()
    {
        _inputAction.performed -= Move;
    }
}
