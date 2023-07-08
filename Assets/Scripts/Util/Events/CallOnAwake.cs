using UnityEngine;
using UnityEngine.Events;

public class CallOnAwake : MonoBehaviour
{
    [SerializeField] private UnityEvent _response;
    void Awake()
    {
        _response?.Invoke();
    }
}
