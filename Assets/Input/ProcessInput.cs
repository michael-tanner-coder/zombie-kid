using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ProcessInput : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _response;

    public void Press(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _response?.Invoke();
        }
    }

    public void Hold(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _response?.Invoke();
        }
    }

    public void Release(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            _response?.Invoke();
        }
    }
}
