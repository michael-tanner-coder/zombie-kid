using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class EnemySpawnController : MonoBehaviour
{
    #region Inspector Info
    #region Controller State
    [Header("CONTROLLER STATE")]
    [SerializeField] private SpawnState _state = SpawnState.WAVE;
    public SpawnState State => _state;
    [SerializeField] private bool _loopWaves = false;
    #endregion

    #region Wave Data
    [Header("WAVE DATA")]
    [SerializeField] private List<WaveData> _waves = new List<WaveData>();
    [Tooltip("Counts up until it reaches the current wave's max wave duration")]
    [SerializeField, ReadOnly] private float _waveTimer = 0;

    [Tooltip("Counts up until it reaches the current wave's max rest duration")]
    [SerializeField, ReadOnly] private float _restTimer = 0;

    [Tooltip("Counts up until it reaches the max index of the wave list")]
    [SerializeField, ReadOnly] private int _waveIndex = 0;

    private int _currentWaveSpawnCount = 0;

    private float _combatTime = 0;
    #endregion

    #region Spawn Data
    [Header("Spawn Data")]
    [Tooltip("The base prefab to spawn")]
    [SerializeField] private GameObject _spawnPrefab;

    [Tooltip("Acceptable locations to spawn the prefab")]
    [SerializeField] private List<GameObject> _spawnPoints = new List<GameObject>();
    
    [Tooltip("Collection of game objects that have been spawned (used for deleting all objects in cleanup)")]
    [SerializeField, HideCustomDrawer] private GameObjectCollection _spawnedObjects;
    private GameObject _previousSpawnPoint;
    #endregion

    #region Events
    [Header("EVENTS")]
    [Tooltip("Signifies when a wave begins")]
    [SerializeField] private GameEvent _waveEvent;

    [Tooltip("Signifies when a rest begins")]
    [SerializeField] private GameEvent _restEvent;

    [Tooltip("Signifies when combat ends")]
    [SerializeField] private GameEvent _endEvent;
    #endregion
    #endregion

    #region Methods
    void Awake()
    {
        _waveIndex = 0;
    }

    void Update()
    {

        // if all waves are done
        if (_waveIndex > _waves.Count - 1)
        {
            // --- set spawn state to STOP,
            _state = SpawnState.STOP;

            // --- end combat sequence
            _waveIndex = 0;
            _endEvent.Raise();
            ServiceLocator.Instance.Get<GameStateManager>().SetState(GameState.EXPLORATION);
        }

        // get current wave with waveIndex
        WaveData currentWave = _waves[_waveIndex];

        switch(_state) 
        {
            case SpawnState.STOP:
                break;

            case SpawnState.WAVE:
                // increment wave timer
                _waveTimer += Time.deltaTime;

                // get how long we have made it through the series of waves
                _combatTime += Time.deltaTime;
                float totalWaveDuration = 0f;
                foreach (WaveData wave in _waves)
                {
                    totalWaveDuration += wave.Duration;
                }

                // get our current point in the speed fluctuation
                float CurrentWaveEnemySpeed = currentWave.SpeedRateChange.Value.Evaluate(_waveTimer / currentWave.Duration);
                float SpeedModifier = currentWave.SpeedFactor + currentWave.SpeedFactor * CurrentWaveEnemySpeed;
       
                // get count of enemies on screen
                GameObject[] activeEnemies = GameObject.FindGameObjectsWithTag("Enemy");
                int numberOfEnemies = activeEnemies.Length;

                // only spawn enemies when none are active
                if (numberOfEnemies <= currentWave.MinEnemyCount && _currentWaveSpawnCount <= currentWave.SpawnCount)
                {
                    // --- get current pattern
                    EnemyPattern currentPattern = currentWave.EnemyPatterns[Random.Range(0, currentWave.EnemyPatterns.Count)];
                    
                    // --- spawn enemies in a specific pattern
                    SpawnEnemyPattern(currentPattern, SpeedModifier);
                }

                
                // if wave has ended
                if (_currentWaveSpawnCount >= currentWave.SpawnCount && numberOfEnemies <= 0)
                {
                    // --- reset spawn count and timer
                    _currentWaveSpawnCount = 0;
                    _waveTimer = 0;
                    
                    // --- set spawn state to REST
                    _state = SpawnState.REST;
                    _restEvent?.Raise();
                }

                break;

            case SpawnState.REST:
                // increment rest timer
                _restTimer += Time.deltaTime;

                // if the rest period is over
                if (_restTimer >= currentWave.RestPeriod)
                {
                    // --- reset rest timer
                    _restTimer = 0;

                    // --- increment wave index
                    _waveIndex += 1;

                    // --- switch state to WAVE
                    _state = SpawnState.WAVE;
                    _waveEvent?.Raise();
                }

                break;

            default:
                break;
        }
    }

    void SpawnEnemyPattern(EnemyPattern pattern, float speedModifier)
    {
        if (_state != SpawnState.WAVE)
        {
            return;
        }

        GameObject spawnPoint;
        
        do
        {
            spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
        } 
        while (GameObject.ReferenceEquals(spawnPoint, _previousSpawnPoint));

        _previousSpawnPoint = spawnPoint;

        Transform spawnPointTransform = spawnPoint.GetComponent<Transform>();
        float offsetAmount = 1f;
        float spawnedEnemyCount = 0;

        foreach(EnemyAttributes enemyType in pattern.Enemies)
        {
            // get spawn point data
            SpawnData spawnPointData = spawnPoint.GetComponent<SpawnData>();

            // spawn enemies in the pattern separated by an offset
            float xOffset = spawnPointData.XDirection * offsetAmount * spawnedEnemyCount;
            float yOffset = spawnPointData.YDirection * offsetAmount * spawnedEnemyCount;
            Vector3 spawnPosition = spawnPointTransform.position;
            Vector3 spawnPositionWithOffSet = new Vector3(spawnPosition.x - xOffset, spawnPosition.y - yOffset, spawnPosition.z); 
            GameObject spawnedObject = Instantiate(_spawnPrefab, spawnPositionWithOffSet, Quaternion.identity);

            // set the enemy's movement direction based on spawn point data
            if (spawnPointData != null)
            {
                spawnedObject.GetComponent<Enemy>().SetAttributes(enemyType);
                spawnedObject.GetComponent<MoveInOwnDirection>()?.SetDirection(new Vector2(spawnPointData.XDirection, spawnPointData.YDirection));
                MoveTowardClosest enemyMovement = spawnedObject.GetComponent<MoveTowardClosest>();
                enemyMovement?.SetSpeed((enemyMovement.Speed * speedModifier) - spawnedEnemyCount * 0.2f);
            }

            // track spawned object in a list
            if (!_spawnedObjects.Contains(spawnedObject))
            {
                _spawnedObjects.Add(spawnedObject);
            }

            // increment multiplier for the position offset
            spawnedEnemyCount += 1;
        }

        for (int i = 0; i <= _spawnedObjects.Count - 1; i++)
        {
            if (i > 0)
            {
                _spawnedObjects[i].GetComponent<MoveTowardClosest>().SetTargetObject(_spawnedObjects[i-1]);
            }
        }

        _currentWaveSpawnCount += 1;
    }
    
    public void ResetState()
    {
        _state = SpawnState.WAVE;
    }
    #endregion
}
