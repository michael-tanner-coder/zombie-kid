using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObjectCollection _damagingObjects;
    [SerializeField] private GameEvent _deathEvent;

     void OnTriggerEnter2D(Collider2D other) 
    {
        if (_damagingObjects.Contains(other.gameObject))
        {
            Bullet projectile = other.gameObject.GetComponent<Bullet>();
            if (projectile != null && projectile.Lethal)
            {
                _deathEvent?.Raise();
            }
        }    
    }
}
