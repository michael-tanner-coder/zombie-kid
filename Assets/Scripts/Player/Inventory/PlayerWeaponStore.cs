using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponStore : MonoBehaviour
{
    #region Inspector Settings
    [Tooltip("The weapons the player currently has access to")]
    [SerializeField] private List<WeaponData> _acquiredWeapons = new List<WeaponData>();
    public List<WeaponData> AcquiredWeapons => _acquiredWeapons;
    #endregion

    #region Methods
    public void AddWeapon(WeaponData weapon)
    {
        if (!AcquiredWeapons.Contains(weapon))
        {
            _acquiredWeapons.Add(weapon);
        }
    }

    public void RemoveWeapon(WeaponData weapon)
    {
        if (AcquiredWeapons.Contains(weapon))
        {
            _acquiredWeapons.Remove(weapon);
        }
    }

    public bool HasWeapon(WeaponData weapon)
    {
        return _acquiredWeapons.Contains(weapon);
    }
    #endregion
}
