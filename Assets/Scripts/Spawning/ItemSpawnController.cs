using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class ItemSpawnController : MonoBehaviour
{
    #region Inspector Info
    [Header("Collections")]
    [Tooltip("The prefab to spawn")]
    [SerializeField] private GameObject _prefab;

    [Tooltip("The types of objects that can spawn")]
    [SerializeField] private List<ItemData> _spawnTypes;

    [Tooltip("Acceptable locations to spawn")]
    [SerializeField] private List<GameObject> _spawnPoints = new List<GameObject>();
    
    [Tooltip("A collection to track the spawned objects")]
    [SerializeField] private GameObjectCollection _spawnedObjectCollection;
    #endregion

    #region Methods

    public void SpawnAtAllPoints()
    {
        foreach(GameObject spawnPoint in _spawnPoints)
        {            
            // get spawn point information
            Transform spawnPointTransform = spawnPoint.GetComponent<Transform>();

            // type of object to spawn
            ItemData spawnType = PickSpawnType();

            // spawn the object
            GameObject spawnedObject = Instantiate(_prefab, spawnPointTransform.position, Quaternion.identity);

            // set parameters of new object
            Item itemComponent = spawnedObject.GetComponent<Item>();
            if (itemComponent != null)
            {
                itemComponent.SetAttributes(spawnType);
            }
    
            // track spawned object in a list
            if (!_spawnedObjectCollection.Contains(spawnedObject))
            {
                _spawnedObjectCollection.Add(spawnedObject);
            }
        }
    }

    public ItemData PickSpawnType()
    {
        int index = Random.Range(0, _spawnTypes.Count);
        ItemData spawnType = _spawnTypes[index];
        return spawnType;
    }

    #endregion
}
