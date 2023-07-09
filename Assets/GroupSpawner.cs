using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupSpawner : MonoBehaviour
{
    [SerializeField] GameObject _groupPrefab;
    [SerializeField] List<GameObject> _spawnPoints = new List<GameObject>();
    [SerializeField] private float _timeBetweenSpawns;
    [SerializeField] private int _baseMinUnitSpawnCount;
    [SerializeField] private int _baseMaxUnitSpawnCount;
    [SerializeField] private int _maxGroupSize;
    private float _gameDuration;
    private float _spawnTimer;
    private int _spawnedUnits;
    private GameObject _previousSpawnPoint;

    void Awake()
    {

    }

    void Update()
    {
        _gameDuration += Time.deltaTime;


        _spawnTimer += Time.deltaTime;
        if (_spawnTimer >= _timeBetweenSpawns)
        {
            _spawnTimer = 0f;
            SpawnGroup();
        }
    }

    void SpawnGroup()
    {
        // get spawn point; don't use the same one twice in a row
        GameObject spawnPoint;
        
        do
        {
            spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
        } 
        while (GameObject.ReferenceEquals(spawnPoint, _previousSpawnPoint));

        _previousSpawnPoint = spawnPoint;

        // determine how large the new group should be based on the time passed and the player's score
        int unitSpawnCountModifier = (int) Mathf.Floor(_gameDuration / 10f);
        int minUnitSpawnCount = _baseMinUnitSpawnCount + unitSpawnCountModifier;
        int maxUnitSpawnCount = _baseMaxUnitSpawnCount + unitSpawnCountModifier;
        maxUnitSpawnCount = Mathf.Clamp(maxUnitSpawnCount, 0, _maxGroupSize);

        // spawn the new object and set its group parameters
        GameObject newGroupObject = Instantiate(_groupPrefab, spawnPoint.transform.position, Quaternion.identity);
        Group newGroup = newGroupObject.GetComponent<Group>();
        int unitAmount = (int) Mathf.Floor(Random.Range(minUnitSpawnCount, maxUnitSpawnCount));
        newGroup.Formation.SetAmount(unitAmount);
        newGroup.SetFormationRings();
    }

}
