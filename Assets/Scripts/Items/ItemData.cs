using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

[CreateAssetMenu(fileName = "ItemData", menuName = "Items/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    #region Basic Item Information

    [Header("Info")]
    [Tooltip("The name of the item the player will see on the UI")]
    [SerializeField] private string _name;
    public string Name => _name;

    [Tooltip("Text meant to describe the item's functionality and add humor")]
    [TextArea(15,20)]
    [SerializeField] private string _description;
    public string Description => _description;

    public void SetName(string name) => _name = name;
    public void SetDescription(string desc) => _description = desc;

    #endregion

    #region Item Stats

    [Header("Stats")]
    [Tooltip("How much it will increase the player's current ammo for their secondary weapon")]
    [SerializeField] private int _ammoAmount;
    public int AmmoAmount => _ammoAmount;

    [Tooltip("How much it will increase the player's current ammo for their secondary weapon")]
    [SerializeField] private int _moneyAmount;
    public int MoneyAmount => _moneyAmount;

    [Tooltip("How much money the item can be bought for")]
    [SerializeField] private int _price;
    public int Price => _price;

    [Tooltip("Weapon data associated with this item (affects UI display in shop)")]
    [SerializeField] private WeaponData _weaponData;
    public WeaponData WeaponData => _weaponData;
    
    #endregion
    
    #region Graphics and Sound

    [Header("Graphics")]
    [Tooltip("Default image for the item in the dungeons and the shop")]
    [SerializeField] private Sprite _image;
    public Sprite Image => _image;
    
    [Header("Sounds")]
    [Tooltip("Sound to play when the item appears in a dungeon")]
    [SerializeField] private AudioClip _spawnSound;
    public AudioClip SpawnSound => _spawnSound;

    [Tooltip("Sound to play when the player picks up the item")]
    [SerializeField] private AudioClip _collectSound;
    public AudioClip CollectSound => _collectSound;

    #endregion
}
