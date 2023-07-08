using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class DifficultySetting : MonoBehaviour
{
    [SerializeField] private InputField _field;
    public InputField Field => _field;

    [SerializeField] private int _maxCost;
    public float MaxCost => _maxCost;
    
    [SerializeField] private float _maxRewardBoost;
    public float MaxRewardBoost => _maxRewardBoost;

    private int _currentCost;
    public int CurrentCost => _currentCost;
}
