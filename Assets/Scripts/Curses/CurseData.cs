using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

[CreateAssetMenu(fileName = "CurseData", menuName = "Curses/Curse Data", order = 1)]
public class CurseData : ScriptableObject
{
    #region Basic Curse Information
    [Header("INFO")]
    [Tooltip("The name of the item the player will see on the UI")]
    [SerializeField] private string _name;
    public string Name => _name;

    [Tooltip("Text meant to describe the curse's functionality and add humor")]
    [TextArea(15,20)]
    [SerializeField] private string _description;
    public string Description => _description;
    #endregion

    #region Curse Stats
    [Header("MODIFIERS")]
    [SerializeField] private List<Modifier> _modifiers = new List<Modifier>();
    public List<Modifier> Modifiers => _modifiers;
    #endregion
    
    #region Visuals and Sound
    [Header("GRAPHICS")]
    [Tooltip("Default image for the item in the dungeons and the shop")]
    [SerializeField] private Sprite _image;
    public Sprite Image => _image;
    
    [Header("SOUNDS")]
    [Tooltip("Sound to play when the item appears in a dungeon")]
    [SerializeField] private AudioClip _spawnSound;
    public AudioClip SpawnSound => _spawnSound;

    [Tooltip("Sound to play when the player picks up the item")]
    [SerializeField] private AudioClip _collectSound;
    public AudioClip CollectSound => _collectSound;
    #endregion
}
