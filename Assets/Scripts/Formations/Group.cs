using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Group : MonoBehaviour {
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

    [SerializeField] private GameObject _unitPrefab;
    [SerializeField] private float _unitSpeed = 2;

    private readonly List<GameObject> _spawnedUnits = new List<GameObject>();
    private List<Vector3> _points = new List<Vector3>();
    [SerializeField] private Transform _leader;

    [SerializeField] private bool _canConsumeUnits = false;

    private void Update() 
    {
        SetFormation();
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

        otherGroup.Formation.SetAmount(theirAmount);
        Formation.SetAmount(ourAmount);

        int newRingAmount = (int) Mathf.Floor((float) Formation.Amount / 10f);
        newRingAmount = Mathf.Clamp(newRingAmount, 1, 10);
        Formation.SetRings(newRingAmount);

        if (ourAmount <= 0)
        {
            Destroy(gameObject);
        }
    }
}