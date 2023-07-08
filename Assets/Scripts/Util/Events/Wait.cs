using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Wait : MonoBehaviour
{

    [SerializeField]
    private UnityEvent _response;

    [SerializeField]
    private float _waitTime;

    IEnumerator CallAfterWaitForSeconds()
    {
        yield return new WaitForSeconds(_waitTime);

        _response?.Invoke();
    }

    public void StartWait()
    {
        StartCoroutine(CallAfterWaitForSeconds());
    }

}
