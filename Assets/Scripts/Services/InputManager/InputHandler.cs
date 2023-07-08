using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private PlayerControls _inputs;

    private void Awake()
    {
        _inputs = new PlayerControls();
    }

    private void OnEnable()
    {
        _inputs.Enable();
    }

    private void OnDisable()
    {
        _inputs.Disable();
    }

    // input actions
    public InputAction Shoot()
    {
        return _inputs.Player.Shoot;
    }
    
    public InputAction Move()
    {
        return _inputs.Player.Move;
    }

    public InputAction SwitchWeapon()
    {
        return _inputs.Player.SwitchWeapon;
    }

    public InputAction Interact()
    {
        return _inputs.Player.Interact;
    }

    public InputAction Pause()
    {
        return _inputs.Player.Pause;
    }

    public InputAction Navigate()
    {
        return _inputs.Player.Navigate;
    }

    public InputAction Exit()
    {
        return _inputs.Player.Exit;
    }

    public InputAction FindAction(string actionName)
    {
        return _inputs.FindAction(actionName, false);
    }
}
