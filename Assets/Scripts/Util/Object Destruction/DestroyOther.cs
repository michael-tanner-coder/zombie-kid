using UnityEngine;
using ScriptableObjectArchitecture;

public class DestroyOther : MonoBehaviour
{
    [SerializeField]
    private GameObjectCollection _collidableObjects;

    void OnCollisionEnter2D(Collision2D other) 
    {
        if (_collidableObjects.Contains(other.gameObject))
        {
            Destroy(other.gameObject);
        }    
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (_collidableObjects.Contains(other.gameObject))
        {
            Destroy(other.gameObject);
        }    
    }
}
