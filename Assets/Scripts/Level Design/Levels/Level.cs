using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

[CreateAssetMenu(fileName = "Level", menuName = "Level Design/Level", order = 1)]
public class Level : ScriptableObject
{
    [Tooltip("The name to use as a scene reference when transitioning to this level")]
    [SerializeField] private string _levelName;
    public string LevelName => _levelName;
    
    [Tooltip("The set of visuals for the different room types in this level")]
    [SerializeField] private SpriteDictionary _graphics;
    public SpriteDictionary Graphics => _graphics;

    [Tooltip("The music to play when this level begins")]
    [SerializeField] private MusicTrack _music;
    public MusicTrack Music => _music;
}
