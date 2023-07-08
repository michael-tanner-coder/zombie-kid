using System.Collections.Generic;
using UnityEngine;

public class ComponentDisabler : MonoBehaviour
{
    [SerializeField]
    private List<MonoBehaviour> components = new List<MonoBehaviour>();

    public void DisableComponents() 
    {
        components.ForEach((component) =>
        {
            component.enabled = false;
        });
    }

    public void EnableComponents() 
    {
        components.ForEach((component) =>
        {
            component.enabled = true;
        });
    }
}
