using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatUI : MonoBehaviour
{
    [Tooltip("Player data to display in the UI")]
    [SerializeField] private PlayerAttributes _player;

    [Tooltip("Icon for the player's active weapon")]
    [SerializeField] private Image _currentWeapon;

    [Tooltip("The open slots the player has available for curses")]
    [SerializeField] private CurseList _curseSlots;
    
    [Tooltip("Icon positions for each curse slot")]
    [SerializeField] private List<GameObject> _curseImageSlots = new List<GameObject>();

    void Update()
    {
        if (_player.CurrentWeapon != null)
        {
            _currentWeapon.sprite = _player.CurrentWeapon.EquipIcon;
        } 

        for (int i = 0; i <= _curseSlots.Elements.Count - 1; i++)
        {
            _curseImageSlots[i].GetComponent<Image>().sprite = _curseSlots.Elements[i].Image;
        }
    }
}
