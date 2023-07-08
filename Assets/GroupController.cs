using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public enum GroupState 
{
    CHASE,
    FLEE, 
    FOLLOW,
    IDLE,
}  

public class GroupController : MonoBehaviour
{
    [SerializeField] private Group _group;
    [SerializeField] private GroupState _state = GroupState.IDLE;
    [SerializeField] private GameObjectCollection _opposingGroups;
    [SerializeField] private CircleCollider2D _collider;
    [SerializeField] private bool _canGainUnits = false;
    [SerializeField] private float _timeUntilConsumption = 1f;
    private float _consumptionTimer = 0f;

    void Awake()
    {
        _collider.radius = _group.Formation.Radius;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (_opposingGroups.Contains(collision.gameObject))
        {
            Debug.Log("Other group entered range");
            _consumptionTimer = 0f;

            Group otherGroup = collision.gameObject.GetComponent<Group>();
            
            if (otherGroup.Formation.Amount < _group.Formation.Amount)
            {
                Debug.Log("Chasing group");
                _state = GroupState.CHASE;
            }
            
            if (otherGroup.Formation.Amount > _group.Formation.Amount)
            {
                Debug.Log("Fleeing group");
                _state = GroupState.FLEE;

            }


        }
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
            _group.Consume(otherGroup);
        }
    }

    void Chase()
    {

    }

    void Flee()
    {

    }

    void Update()
    {
        switch (_state)
        {
            case GroupState.FLEE:
                Flee();
                break;
            case GroupState.CHASE:
                Chase();
                break;
            case GroupState.IDLE:
            default:
                break;
        }
    }
}
