using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnVFX : MonoBehaviour
{
   [SerializeField] private GameObject _effectPrefab;
   [SerializeField] private bool _destroySelf = false;

   public void Spawn()
   {
        Instantiate(_effectPrefab, transform.position, Quaternion.identity);

        if (_destroySelf)
        {
            Destroy(gameObject);
        }
   }
}
