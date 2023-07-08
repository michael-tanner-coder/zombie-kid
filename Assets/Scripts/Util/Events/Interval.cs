using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Interval : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _response;

    [SerializeField]
    private float _waitTime;

    IEnumerator CallAfterWaitForSeconds()
    {
        yield return new WaitForSeconds(_waitTime);

        _response?.Invoke();

        StartCoroutine(CallAfterWaitForSeconds());
    }

    public void StartInterval()
    {
        StartCoroutine(CallAfterWaitForSeconds());
    }

    public void Awake()
    {
        StartInterval();
    }
}
