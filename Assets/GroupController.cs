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
    public GroupState State => _state;
    [SerializeField] private CircleCollider2D _collider;
    [SerializeField] private float _moveSpeed = 0.5f;
    public GameObject target;

    void Awake()
    {
        _collider.radius = _group.Formation.Radius;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (_state == GroupState.FOLLOW) 
        {
            return;
        }
        
        if (collision.gameObject.tag == _group.foeTag)
        {
            Group otherGroup = collision.gameObject.GetComponent<Group>();

            
            if (otherGroup.Formation.Amount < _group.Formation.Amount)
            {
                _state = GroupState.CHASE;
                target = collision.gameObject;
            }
            
            if (otherGroup.Formation.Amount > _group.Formation.Amount)
            {
                _state = GroupState.FLEE;
                target = collision.gameObject;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == _group.foeTag && _state == GroupState.FLEE)
        {
            _state = GroupState.IDLE;
        }
    }

    public void SetState(GroupState state)
    {
        _state = state;
    }

    void Chase()
    {
        if (target != null)
        {
            Vector2 dir = target.transform.position - transform.position;
            transform.Translate(dir * _moveSpeed * Time.deltaTime);
        }
    }

    void Flee()
    {
        if (target != null)
        {
            Vector2 dir = transform.position - target.transform.position;
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
            case GroupState.FOLLOW:
                Chase();
                break;
            case GroupState.IDLE:
            default:
                break;
        }
    }
}
