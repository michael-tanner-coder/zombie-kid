using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class PlayerCurseStore : MonoBehaviour
{
    [Header("Slots")]
    [SerializeField] private CurseList _curses;
    
    [Header("Player")]
    [SerializeField] private PlayerAttributes _player;

    void Awake()
    {
        _curses.OnAddCurse += ActivateCurse;
    }

    private void ActivateCurse(CurseData curse)
    {
        AddCurse(curse);
    }

    public void RemoveCurse(CurseData curse)
    {
        if (_curses.Elements.Contains(curse))
        {
            _curses.Remove(curse);
             foreach(Modifier mod in curse.Modifiers)
            {
                _player.RemoveModifier(mod);
            }
        }
    }

    public void AddCurse(CurseData curse)
    {
        if (!_curses.Elements.Contains(curse))
        {
            _curses.Add(curse);
            foreach(Modifier mod in curse.Modifiers)
            {
                _player.ApplyModifier(mod);
            }
        }
    }
}
