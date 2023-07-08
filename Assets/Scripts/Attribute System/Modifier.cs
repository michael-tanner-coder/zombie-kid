using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Modifier
{
    #region Inspector Settings
    [Tooltip("Determines the operation performed on the target attribute (add, subtract, etc)")]
    [SerializeField] private Operator _operator;
    public Operator Operator => _operator;

    [Tooltip("The value to be applied in the operation")]
    [SerializeReference] private float _value;
    public float Value => _value;

    [Tooltip("The attribute affected by this modifier")]
    [SerializeField] private AttributeType _targetAttribute;
    public AttributeType TargetAttribute => _targetAttribute;
    #endregion

    #region Methods

    #region Constructor
    public Modifier(float value, AttributeType targetAttribute, Operator op) 
    {
        SetValue(value);
        SetAttributeType(targetAttribute);
        SetOperator(op);
    }
    #endregion

    #region Getters
    public float GetModifiedValue(float value)
    {
        float newValue = value;

        switch (Operator)
        {
            case Operator.ADD:
                newValue += _value;
                break;

            case Operator.SUBTRACT:
                newValue -= _value;
                break;

            case Operator.MULTIPLY:
                newValue *= _value;
                break;

            case Operator.DIVIDE:
                newValue /= _value;
                break;

            case Operator.SET:
                newValue = _value;
                break;
        }

        return newValue;
    }
    #endregion

    #region Setters
    public void SetValue(float value)
    {
        _value = value;
    }

    public void SetOperator(Operator op)
    {
        _operator = op;
    }

    public void SetAttributeType(AttributeType attribute)
    {
        _targetAttribute = attribute;
    }
    #endregion

    #endregion
}


