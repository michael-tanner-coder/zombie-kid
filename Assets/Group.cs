using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour
{
    [SerializeField] private GameObject _leader;
    [SerializeField] private List<GameObject> _followers = new List<GameObject>();

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _leader.transform.position, Time.deltaTime);
    }
}
