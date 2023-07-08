using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ScriptableObjectArchitecture;

public class SliderSOVariable : MonoBehaviour
{
    [SerializeField] private FloatVariable _float;
    [SerializeField] private Slider _slider;

    public void UpdateValue()
    {
        _float.Value = _slider.value;
    }
}
