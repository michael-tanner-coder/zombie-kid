using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class CollectCurse : MonoBehaviour
{
    [SerializeField] GameObjectCollection _curseCollection;
    [SerializeField] CurseList _curseSlots;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (_curseCollection.Contains(collision.gameObject))
        {
            Curse curseComponent = collision.gameObject.GetComponent<Curse>();
            if (curseComponent != null)
            {
                _curseSlots.Add(curseComponent.Parameters);
                Destroy(collision.gameObject);
            }
        }
    }
}
