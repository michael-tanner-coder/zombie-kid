using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowerMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _followSpeed;


    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _followSpeed * Time.deltaTime);
    }
}
