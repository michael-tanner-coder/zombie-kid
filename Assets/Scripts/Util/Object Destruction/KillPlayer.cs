using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using ScriptableObjectArchitecture;

public class KillPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObjectCollection _players;
    
    [SerializeField]
    private List<GameObjectGameEvent> _events = new List<GameObjectGameEvent>();

    void OnCollisionEnter2D(Collision2D other) 
    {
        foreach(GameObjectGameEvent e in _events)
        {
            if (_players.Contains(other.gameObject))
            {
                e.Raise(other.gameObject);
                
            }
        }
    }
}
