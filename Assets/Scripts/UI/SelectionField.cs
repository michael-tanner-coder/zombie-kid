using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class SelectionField : InputField
{
    [Tooltip("The UI object that will display the currently selected value")]
    [SerializeField] private TMP_Text _valueTextObject;

    [Tooltip("List of acceptable values for this field")]
    [SerializeField] private List<string> _values;
    
    [Tooltip("The index used to retrive the active value in our list")]
    [SerializeField] private int _valueIndex = 0;

    protected void Awake()
    {
        if (_labelTextObject)
        {
            _labelTextObject.text = _label;
        }
   
        UpdateValue();

        InputHandler _inputHandler = ServiceLocator.Instance.Get<InputManager>().Inputs();
        _inputHandler.Navigate().performed += HandleInput;
    }

    protected override void HandleInput(InputAction.CallbackContext context)
    {
        if (context.ReadValue<Vector2>().x != 0 && _inputEnabled)
        {
            UpdateValueIndex(context);
            UpdateValue();
            _onInput?.Invoke();
        }
    }

    void UpdateValueIndex(InputAction.CallbackContext context)
    {
        // get next value index
        _valueIndex += (int) context.ReadValue<Vector2>().x;

        // loop to start of the list
        if (_valueIndex > _values.Count - 1)
        {
            _valueIndex = 0;
        }
        
        // loop to end of the list
        if (_valueIndex < 0)
        {
            _valueIndex = _values.Count - 1;
        }
    }

    public string GetCurrentValue()
    {
        return _values[_valueIndex];
    } 

    void UpdateValue()
    {
        _valueTextObject.text = GetCurrentValue();
    }
}
