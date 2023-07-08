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

    void Chase()
    {

    }

    void Flee()
    {

    }

    void Consume(Group otherGroup)
    {
        int theirAmount = otherGroup.Formation.Amount;
        int ourAmount = _group.Formation.Amount;

        if (ourAmount > theirAmount && _canGainUnits) 
        {
            ourAmount += 1;
        }

        if (ourAmount < theirAmount)
        {
            ourAmount -= 1;
        }

        otherGroup.Formation.SetAmount(theirAmount);
        _group.Formation.SetAmount(ourAmount);

        if (ourAmount <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // check distance of other groups
    }
}
