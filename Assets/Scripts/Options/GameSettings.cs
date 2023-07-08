using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Options/Game Settings Controller", order = 1)]
public class GameSettings : ScriptableObject
{
    [SerializeField, Range(0, 10)] private float _gameSpeed = 1f;
    [SerializeField] private BoolVariable _flashToggle;

    void OnValidate()
    {
        Debug.Log("Game settings changed");
     
        Time.timeScale = _gameSpeed;
    }
}
