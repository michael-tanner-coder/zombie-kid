using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using ScriptableObjectArchitecture;

public class ShopMenu : MonoBehaviour
{
    [Header("PLAYER DATA")]
    [SerializeField] private GameObject _player;
    private PlayerMoneyStore _playerMoneyStore;
    private PlayerWeaponStore _playerWeaponStore;
    private PlayerAmmoStore _playerAmmoStore;
    
    [Header("ITEM DATA")]
    [SerializeField] private GridMenu _gridMenu;
    [SerializeField] private TMP_Text _itemName;
    [SerializeField] private TMP_Text _itemDescription;
    [SerializeField] private TMP_Text _itemPrice;

    [Header("WEAPON STATS")]
    [SerializeField] private TMP_Text _weaponAmmo;
    [SerializeField] private TMP_Text _weaponDamage;
    [SerializeField] private GameObject _statsContainer;

    [Header("EVENTS")]
    [SerializeField] private GameEvent _onShopClose;

    void Start()
    {
        // player data
        _playerMoneyStore = _player.GetComponent<PlayerMoneyStore>();
        _playerWeaponStore = _player.GetComponent<PlayerWeaponStore>();
        _playerAmmoStore = _player.GetComponent<PlayerAmmoStore>();

        // inputs
        InputHandler _inputHandler = ServiceLocator.Instance.Get<InputManager>().Inputs();
        _inputHandler.Exit().performed += CloseShop;
        _inputHandler.Interact().performed += SelectItem;
    }

    void Update()
    {
        InputField currentInput = _gridMenu.GetActiveInput();

        // view item details
        if (currentInput != null && currentInput is ShopItemField)
        {
            ShopItemField currentItem = (ShopItemField)currentInput;
            ItemData item = currentItem.ItemData;
            _itemName.text = item.Name;
            _itemDescription.text = item.Description;
            _itemPrice.text = "PRICE: " + item.Price;
            _statsContainer.active = item.WeaponData != null;
            UpdateStatsContainer(item.WeaponData);
        }
    }

    void SelectItem(InputAction.CallbackContext context)
    {
        InputField currentInput = _gridMenu.GetActiveInput();
        
        // attempt to purchase item
        if (currentInput != null && currentInput is ShopItemField)
        {
            AttemptPurchase((ShopItemField)currentInput);
        }
    }

    void CloseShop(InputAction.CallbackContext context)
    {
        _onShopClose?.Raise();
    }

    void AttemptPurchase(ShopItemField selectedItem)
    {
        ItemData item = selectedItem.ItemData;
        
        // do you own the item?
        bool ownItem = item.WeaponData != null && _playerWeaponStore.HasWeapon(item.WeaponData);

        // can you afford the item?
        bool canAfford = _playerMoneyStore.CanAfford(item.Price);

        // attemp purchase based on criteria
        if (canAfford && !ownItem)
        {
            PurchaseItem(item);
            return;
        }

        Debug.Log("CAN'T BUY ITEM");
    }

    void PurchaseItem(ItemData item)
    {
        _playerMoneyStore.SubtractOnHandMoney(item.Price);

        bool isWeapon = item.WeaponData != null;

        if (isWeapon)
        {
            _playerWeaponStore.AddWeapon(item.WeaponData);
            return;
        }

        _playerAmmoStore.GainAmmo(item.AmmoAmount);
    }

    void UpdateStatsContainer(WeaponData weapon)
    {
        // don't update the stats if the container is inactive
        if (!_statsContainer.active || weapon == null)
        {
            return;
        }

        // create a string of "+" signs to represent the maximum quantity of a stat 
        int maxStatNumber = 10;
        string statString = "";
        for(int i = 0; i < maxStatNumber; i++)
        {
            statString += "+";
        }

        // get relevant weapon base stats
        int weaponAmmo = weapon.BaseAmmoUsage;
        int weaponDamage = weapon.BaseDamage;

        // display the stats as a portion of the maximum stat string
        _weaponAmmo.text = "AMMO USAGE: " + weaponAmmo;
        _weaponDamage.text = "DAMAGE: " + weaponDamage;
    }

    ShopItemField GetActiveItem()
    {
        ShopItemField currentItem = null;

        foreach (ShopItemField field in _gridMenu.Fields)
        {
            if (field.InputEnabled)
            {
                currentItem = field;
            }
        }

        return currentItem;
    }
}
