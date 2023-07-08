using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModifiableAttribute
{
    [Header("VALUES")]
    [Tooltip("The default float value for this attribute")]
    [SerializeReference, ReadOnly] private float _baseValue;
    public float BaseValue => _baseValue;
    
    [Tooltip("The modified float value for this attribute (typically read by other components)")]
    [SerializeField, ReadOnly] private float _currentValue;
    public float CurrentValue => _currentValue;

    [Header("MODIFICATIONS")]
    [Tooltip("The active modifers factored into the current value")]
    [SerializeField] private List<Modifier> _modifiers;
    public List<Modifier> Modifiers => _modifiers;

    public void Awake()
    {
        _currentValue = _baseValue;
        _modifiers.Clear();
    }

    public void CalculateValue()
    {
        float newValue = _baseValue;
        
        foreach (Modifier modifier in _modifiers)
        {
            newValue = modifier.GetModifiedValue(newValue);
        }
        
        _currentValue = newValue;
    }

    public void AddModifier(Modifier modifier)
    {
        _modifiers.Add(modifier);
        CalculateValue();
    }

    public void RemoveModifier(Modifier modifier)
    {
        _modifiers.Remove(modifier);
        CalculateValue();
    }

    public void SetBaseValue(float value)
    {
        _baseValue = value;
    }
}
