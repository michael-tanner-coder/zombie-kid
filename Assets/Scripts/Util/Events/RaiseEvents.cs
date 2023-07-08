using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;

public class RaiseEvents : MonoBehaviour
{
    [SerializeField] private List<GameEventBase> _events = new List<GameEventBase>();

    public void RaiseAll() 
    {
        foreach(GameEventBase e in _events)
        {
            e.Raise();
        }
    }

    public void RaiseOne(GameEventBase e) 
    {
        e?.Raise();
    }

}
