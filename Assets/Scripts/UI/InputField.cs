using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.InputSystem;

public class InputField : MonoBehaviour
{
    [Header("FIELD INFO")]
    [Tooltip("The name of the input field displayed on the UI")]
    [SerializeField] protected string _label;
    
    [Tooltip("Text to explain what the input does")]
    [SerializeField] protected string _description;
    public string Description => _description; 

    [Tooltip("The text object to hold the label")]
    [SerializeField] protected TMP_Text _labelTextObject;
    
    [Header("CHANGE HANDLING")]
    [Tooltip("The event invoked when a change is made to the input field")]
    [SerializeField] protected UnityEvent _onInput = new UnityEvent();

    protected bool _inputEnabled;
    public bool InputEnabled => _inputEnabled;

    protected void Awake()
    {
        if (_labelTextObject)
        {
            _labelTextObject.text = _label;
        }
    }

    public void SetInputEnabled(bool toggleValue)
    {
        _inputEnabled = toggleValue;
    }

    protected virtual void GetCurrentValue()
    {
    }

   protected virtual void HandleInput(InputAction.CallbackContext context)
    {
        if (_inputEnabled)
        {
            _onInput?.Invoke();
        }
    }
}
