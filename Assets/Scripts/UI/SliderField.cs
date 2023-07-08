using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SliderField : InputField
{
    [Tooltip("The UI slider to control this field")]
    [SerializeField] private Slider _slider;

    [Tooltip("How much the slider will move on each input")]
    [Range(0f, 10f)]
    [SerializeField] private float _incrementValue = 0.1f;

    void Awake()
    {
        base.Awake();
        InputHandler _inputHandler = ServiceLocator.Instance.Get<InputManager>().Inputs();
        _inputHandler.Navigate().performed += HandleInput;
    }

    protected override void HandleInput(InputAction.CallbackContext context)
    {
        if (context.ReadValue<Vector2>().x != 0 && _inputEnabled)
        {
            _slider.value += context.ReadValue<Vector2>().x * _incrementValue;
            _onInput?.Invoke();
        }
    }

    public float GetCurrentValue()
    {
        return _slider.value;
    }

    public float GetMaxValue()
    {
        return _slider.maxValue;
    }

    public float GetMinValue()
    {
        return _slider.minValue;
    }
}
