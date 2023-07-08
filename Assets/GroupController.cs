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

    void Awake()
    {
        _collider.radius = _group.Formation.Radius;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (_opposingGroups.Contains(collision.gameObject))
        {
            Debug.Log("Other group entered range");

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
