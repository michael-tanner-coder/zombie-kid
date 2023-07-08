using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class PlayerAmmoStore : MonoBehaviour
{
    [SerializeField] private IntVariable _currentAmmo;
    [SerializeField] private PlayerAttributes _playerAttributes;

    public void UseAmmo(int amount)
    {
        if (_currentAmmo.Value >= amount)
        {
            _currentAmmo.Value -= amount;
        }
    }

    public void SetAmmo(int amount)
    {
        _currentAmmo.Value = amount;
        _currentAmmo.Value = Mathf.Clamp(_currentAmmo.Value, 0, (int) _playerAttributes.AmmoCap.CurrentValue);
    }

    public void GainAmmo(int amount)
    {
        _currentAmmo.Value += amount;
        _currentAmmo.Value = Mathf.Clamp(_currentAmmo.Value, 0, (int) _playerAttributes.AmmoCap.CurrentValue);
    }
}
