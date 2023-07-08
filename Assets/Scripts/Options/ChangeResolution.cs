using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Resolutions
{
    public string key; 
    public int width; 
    public int height; 
}

public class ChangeResolution : MonoBehaviour
{
    [SerializeField] private SelectionField _resolutionField;
    [SerializeField] private List<Resolutions> _resolutions;


    public void SetResolution()
    {
        string keyValue = _resolutionField.GetCurrentValue();

        Resolutions _targetResolution = new Resolutions();

        foreach (Resolutions resolution in _resolutions)
        {
            if (resolution.key == keyValue)
            {
                _targetResolution = resolution;
            }
        }

        Screen.SetResolution(_targetResolution.width, _targetResolution.height, false);

        Debug.Log(_targetResolution.key);
    }
}
