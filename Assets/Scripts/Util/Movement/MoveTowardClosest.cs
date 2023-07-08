using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class MoveTowardClosest : MonoBehaviour
{
    [SerializeField] private GameObjectCollection _targetObjects;
    [SerializeField] private float _speed = 1f;
    private float _currentSpeed = 0f;
    public float Speed => _speed;
    private GameObject targetObject;

    void Update()
    {
        // gradually ramp up to max speed
        _currentSpeed += 0.5f * Time.deltaTime;
        if (_currentSpeed >= _speed)
        {
            _currentSpeed = _speed;
        }

        // if no target object is already available, pick the closest object in a given collection
        float minDistance = Mathf.Infinity;
        Vector2 selfPosition = transform.position;

        if (targetObject == null)
        {
            foreach (GameObject obj in _targetObjects)
            {
                Vector2 targetPosition = obj.transform.position;
                float distance = (targetPosition - selfPosition).sqrMagnitude;

                if (distance < minDistance)
                {
                    minDistance = distance;
                    targetObject = obj;
                }
            }
        }

        // move toward the target object
        if (targetObject != null)
        {
            MoveTowardObject(targetObject);
        }
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    public void SetTargetObject(GameObject target)
    {
        targetObject = target;
    }

   void MoveTowardObject(GameObject target)
   {
        transform.position = Vector2.Lerp(transform.position, target.transform.position, Time.deltaTime * _currentSpeed);
   }
}
