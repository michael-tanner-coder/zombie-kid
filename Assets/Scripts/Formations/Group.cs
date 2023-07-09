using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ScriptableObjectArchitecture;

public class Group : MonoBehaviour {
    #region Inspector Properties
    private RadialFormation _formation;

    public RadialFormation Formation 
    {
        get 
        {
            if (_formation == null) _formation = GetComponent<RadialFormation>();
            return _formation;
        }
        set => _formation = value;
    }

    [Header("Units")]
    [SerializeField] private GameObject _unitPrefab;
    [SerializeField] private float _unitSpeed = 2;

    private readonly List<GameObject> _spawnedUnits = new List<GameObject>();
    private List<Vector3> _points = new List<Vector3>();

    [Header("Targets")]
    [SerializeField] private Transform _leader;
    [SerializeField] private GameObjectCollection _opposingGroups;

    [Header("Consumption")]
    [SerializeField] private bool _canConsumeUnits = false;
    [SerializeField] private float _timeUntilConsumption = 1f;
    private float _consumptionTimer = 0f;

    [Header("Events")]
    [SerializeField] private GameEvent _onDestroy;
    
    #endregion

    #region Formation Update

    private void Awake()
    {
        SetFormationRings();
    }

    private void Update() 
    {
        SetFormation();

        if (Formation.Amount <= 0)
        {
            _onDestroy?.Raise();
            Destroy(gameObject);
        }
    }

    private void SetFormation() 
    {
        _points = Formation.EvaluatePoints().ToList();

        if (_points.Count > _spawnedUnits.Count) 
        {
            var remainingPoints = _points.Skip(_spawnedUnits.Count);
            Spawn(remainingPoints);
        }
        else if (_points.Count < _spawnedUnits.Count) 
        {
            Kill(_spawnedUnits.Count - _points.Count);
        }

        for (var i = 0; i < _spawnedUnits.Count; i++) 
        {
            _spawnedUnits[i].transform.position = Vector3.MoveTowards(_spawnedUnits[i].transform.position, _leader.position + _points[i], _unitSpeed * Time.deltaTime);
        }
    }
    #endregion

    #region Unit Generation/Destruction
    private void Spawn(IEnumerable<Vector3> points) 
    {
        foreach (var pos in points) 
        {
            var unit = Instantiate(_unitPrefab, transform.position + pos, Quaternion.identity, transform);
            _spawnedUnits.Add(unit);
        }
    }

    private void Kill(int num) 
    {
        for (var i = 0; i < num; i++) 
        {
            var unit = _spawnedUnits.Last();
            _spawnedUnits.Remove(unit);
            Destroy(unit.gameObject);
        }
    }
    #endregion

    #region Group Consumption
    public void Consume(Group otherGroup)
    {
        int theirAmount = otherGroup.Formation.Amount;
        int ourAmount = Formation.Amount;

        if (ourAmount > theirAmount && _canConsumeUnits) 
        {
            ourAmount += 1;
        }

        if (ourAmount < theirAmount)
        {
            ourAmount -= 1;
        }

        Formation.SetAmount(ourAmount);

        SetFormationRings();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (_opposingGroups.Contains(collision.gameObject))
        {
            _consumptionTimer = 0f;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (_opposingGroups.Contains(collision.gameObject))
        {
            _consumptionTimer = 0f;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        _consumptionTimer += Time.deltaTime;
        if (_opposingGroups.Contains(collision.gameObject) && _consumptionTimer >= _timeUntilConsumption)
        {
            _consumptionTimer = 0f;
            Group otherGroup = collision.gameObject.GetComponent<Group>();
            Consume(otherGroup);
        }
    }

    public void SetFormationRings()
    {
        int newRingAmount = (int) Mathf.Floor((float) Formation.Amount / 10f);
        newRingAmount = Mathf.Clamp(newRingAmount, 1, 10);
        Formation.SetRings(newRingAmount);
    }
    #endregion
}