using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ButtonField : InputField
{
    [SerializeField] private Button _button;

    void Awake()
    {
        InputHandler _inputHandler = ServiceLocator.Instance.Get<InputManager>().Inputs();
        _inputHandler.Interact().performed += HandleInput;
    }

    protected override void HandleInput(InputAction.CallbackContext context)
    {
        if (_inputEnabled)
        {
            _onInput?.Invoke();
            _button.onClick?.Invoke();
        }
    }
}
