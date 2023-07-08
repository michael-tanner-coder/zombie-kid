using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSlots : MonoBehaviour
{
    [SerializeField] private PlayerAttributes _player;
    private int _currentWeaponIndex = 0;

    void Awake()
    {
        InputHandler _inputHandler = ServiceLocator.Instance.Get<InputManager>().Inputs();
        _inputHandler.SwitchWeapon().performed += SwitchWeapon;
    }

    public void SwitchWeapon(InputAction.CallbackContext context)
    {
        _currentWeaponIndex += 1;
        if (_currentWeaponIndex == _player.WeaponList.Count)
        {
            _currentWeaponIndex = 0;
        }

        WeaponData nextWeapon = _player.WeaponList[_currentWeaponIndex];

        _player.SetCurrentWeapon(nextWeapon);

        ServiceLocator.Instance.Get<AudioManager>().PlaySoundFromDictionary("SwitchWeapon");
    }
}
