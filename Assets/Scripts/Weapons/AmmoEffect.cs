using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum AmmoType
{
    FREEZE,
    PIERCE,
    DUAL_FIRE,
    SHOCK,
    STONE,
    NONE,
}

[CreateAssetMenu(fileName = "Ammo Effect", menuName = "Weapon/AmmoEffect", order = 1)]
public class AmmoEffect : ScriptableObject
{
    #region Basic Effect Information

    [Header("Basic Information")]
    [Tooltip("The name of the ammo effect the player will see on the UI")]
    [SerializeField] private string _name;
    public string Name => _name;

    [Tooltip("Text meant to describe the effect's functionality and add humor")]
    [TextArea(15,20)]
    [SerializeField] private string _description;
    public string Description => _description;

    [Tooltip("The ammo's type (determines the effect)")]
    [SerializeField] private AmmoType _type;
    public AmmoType Type => _type;

    #endregion

    #region Graphics and Sound

    [Header("Graphics and Sound")]
    [Tooltip("The icon displayed in the Shop UI")]
    [SerializeField] private Sprite _icon;
    public Sprite Icon => _icon;

    [Tooltip("The sound played when the effect is used")]
    [SerializeField] private AudioClip _activationSound;
    public AudioClip ActivationSound => _activationSound;

    #endregion

}
