using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemData _parameters;
    [SerializeField] private SpriteRenderer _renderer;

    void Awake()
    {
        SetAttributes(_parameters);
    }

    public void SetAttributes(ItemData parameters)
    {
        _parameters = parameters;
        _renderer.sprite = _parameters.Image;
    }

    public void ActivateEffects(PlayerAmmoStore ammoStore, PlayerMoneyStore moneyStore)
    {
        moneyStore.AddOnHandMoney(_parameters.MoneyAmount);
        ammoStore.GainAmmo(_parameters.AmmoAmount);
    }
}
