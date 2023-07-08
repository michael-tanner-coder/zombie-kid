using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemField : InputField
{
    [Tooltip("The item represented in this field")]
    [SerializeField] private ItemData _itemData;
    public ItemData ItemData => _itemData;

    void Start()
    {
        Image image = GetComponent<Image>();
        image.sprite = _itemData.Image;
        if (_itemData.WeaponData != null)
        {
            image.sprite = _itemData.WeaponData.StoreIcon;
            _itemData.SetName(_itemData.WeaponData.Name);
            _itemData.SetDescription(_itemData.WeaponData.Description);
        }
    }

    public void SetItemData(ItemData data)
    {
        _itemData = data;
    }
}
