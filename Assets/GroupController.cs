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
    [SerializeField] private float _moveSpeed = 0.5f;
    private GameObject _target;

    void Awake()
    {
        _collider.radius = _group.Formation.Radius;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (_opposingGroups.Contains(collision.gameObject))
        {
            Group otherGroup = collision.gameObject.GetComponent<Group>();
            
            if (otherGroup.Formation.Amount < _group.Formation.Amount)
            {
                _state = GroupState.CHASE;
                _target = collision.gameObject;
            }
            
            if (otherGroup.Formation.Amount > _group.Formation.Amount)
            {
                _state = GroupState.FLEE;
                _target = collision.gameObject;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (_opposingGroups.Contains(collision.gameObject) && _state == GroupState.FLEE)
        {
            _state = GroupState.IDLE;
        }
    }

    void Chase()
    {
        if (_target != null)
        {
            Vector2 dir = _target.transform.position - transform.position;
            transform.Translate(dir * _moveSpeed * Time.deltaTime);
        }
    }

    void Flee()
    {
        if (_target != null)
        {
            Vector2 dir = transform.position - _target.transform.position;
            transform.Translate(dir * _moveSpeed * Time.deltaTime);
        }
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
