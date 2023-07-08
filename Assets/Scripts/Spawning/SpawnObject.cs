using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private GameObject _object;

    public void Spawn()
    {
        Instantiate(_object, transform.position, transform.rotation);
    }

    public void SpawnPrefab(GameObject prefab)
    {
        Instantiate(prefab, transform.position, transform.rotation);
    }
}
