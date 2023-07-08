using UnityEngine;

public class InputManager : IService
{
    private readonly GameObject _InputManagerGameObject;
    private PlayerControls _playerControls;
    public InputManager()
    {
        _InputManagerGameObject = new GameObject("InputManagerGameObject");
        Object.DontDestroyOnLoad(_InputManagerGameObject);        
        _InputManagerGameObject.AddComponent<InputHandler>();
    }

    ~InputManager()
    {
        Object.Destroy(_InputManagerGameObject);
    }

    public InputHandler Inputs()
    {
        return _InputManagerGameObject.GetComponent<InputHandler>();
    }
}