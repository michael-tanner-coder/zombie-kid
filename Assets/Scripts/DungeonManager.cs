using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class DungeonManager : MonoBehaviour
{
    [SerializeField] private PlayerAttributes _player;
    [SerializeField] private IntVariable _score;
    [SerializeField] private IntVariable _ammo;

    void Awake()
    {
        // reset player values to the default
        _score.Value = 0;
        _ammo.Value = 9;
        _player.CurseSlots.Clear();
    }
}
