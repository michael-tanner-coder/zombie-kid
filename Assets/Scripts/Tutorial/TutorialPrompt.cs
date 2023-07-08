using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "TutorialPrompt", menuName = "Content/Tutorial Prompt", order = 2)]
public class TutorialPrompt : ScriptableObject
{
    [SerializeField] private string _promptText;
    public string PromptText => _promptText;
    
    [SerializeField] private string _inputKey;
    public string InputKey => _inputKey;
    
    [SerializeField] private int _maxInputCounter;
    public int MaxInputCounter => _maxInputCounter;
}
