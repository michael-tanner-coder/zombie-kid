using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ScriptableObjectArchitecture;
using TMPro;

public class UpdateText : MonoBehaviour
{

    [Header("TEXT OBJECT")]
    [SerializeField] private TMP_Text _text;
    
    [Header("FLOAT VALUE")]
    [SerializeField] private bool _useFloat = false;
    [SerializeField, HideCustomDrawer] private FloatVariable _value;

    [Header("INT VALUE")]
    [SerializeField] private bool _useInt = false;
    [SerializeField, HideCustomDrawer] private IntVariable _intValue;

    [Header("STRING VALUE")]
    [SerializeField] private bool _useString = false;
    [SerializeField, HideCustomDrawer] private StringReference _stringValue;

    [Header("LABEL")]
    [SerializeField] private string _label;

    void Update()
    {
        if (_useFloat && _value != null)
        {
            _text.text = _label + _value.Value;
        }
     
        if (_useInt && _intValue != null)
        {
            _text.text = _label + _intValue.Value;
        }

        if (_useString && _stringValue != null)
        {
            _text.text = _stringValue.Value;
        }
    }
}
