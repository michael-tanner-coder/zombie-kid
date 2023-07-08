using ScriptableObjectArchitecture;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    [SerializeField]
    private GameObjectCollection _collidableObjects;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (_collidableObjects.Contains(other.gameObject))
        {
            Destroy(gameObject);
        }
    }
}
