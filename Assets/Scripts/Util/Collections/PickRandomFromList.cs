using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickRandomFromList<T> : MonoBehaviour
{
    [SerializeField]
    private List<T> _list = new List<T>();

    public T GetRandomElement()
    {
        int index = Random.Range(0, _list.Count);
        return _list[index];
    }
}
