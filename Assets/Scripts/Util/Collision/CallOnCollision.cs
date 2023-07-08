using UnityEngine;
using UnityEngine.Events;
using ScriptableObjectArchitecture;

public class CallOnCollision : MonoBehaviour
{
    [SerializeField]
    private GameObjectCollection _collidableObjects;

    [SerializeField]
    private UnityEvent _enterResponse;

    [SerializeField]
    private UnityEvent _exitResponse;

    void OnCollisionEnter2D(Collision2D other) 
    {
        if (_collidableObjects.Contains(other.gameObject))
        {
            _enterResponse?.Invoke();
        }    
    }
    void OnCollisionExit2D(Collision2D other) 
    {
        if (_collidableObjects.Contains(other.gameObject))
        {
            _exitResponse?.Invoke();
        }    
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (_collidableObjects.Contains(other.gameObject))
        {
            _enterResponse?.Invoke();
        }    
    }
    void OnTriggerExit2D(Collider2D other) 
    {
        if (_collidableObjects.Contains(other.gameObject))
        {
            _exitResponse?.Invoke();
        }    
    }
}
