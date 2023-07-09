using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhistleRadius : MonoBehaviour
{
    public GameObject caller;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Zombie")
        {
            Debug.Log("Found zombie to recall");
            GroupController AIController = collider.gameObject.GetComponent<GroupController>();
            if (AIController != null && caller != null)
            {
                AIController.SetState(GroupState.FOLLOW);
                AIController.target = caller;
            }
        }
    }
}
