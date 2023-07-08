using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShakeOverTime : MonoBehaviour
{
    [SerializeField] private float _timeToStartShaking = 1f;
    [SerializeField] private float _maxShakeDuration = 1f;
    [SerializeField] private float _maxShakeOffset;
    [SerializeField] private UnityEvent _onShakeEnd;
    private float _elapsedShakeDuration = 0f;
    Vector2 _initialPosition = new Vector2(0f, 0f);

    void Start()
    {
        _initialPosition.x = gameObject.transform.position.x;
        _initialPosition.y = gameObject.transform.position.y;
    }

    void Update()
    {
        // don't continue shaking when we reach the shake time limit
        if (_elapsedShakeDuration >= _maxShakeDuration)
        {
            return;
        }

        // countdown to when shaking starts
        _timeToStartShaking -= Time.deltaTime;
        if (_timeToStartShaking <= 0f)
        {
            // shake more over time (along x-axis)
            _elapsedShakeDuration += Time.deltaTime;
            float shake = Mathf.Pow(_elapsedShakeDuration / _maxShakeDuration, 2);
            float offsetX = shake * _maxShakeOffset * Random.Range(-1, 1);
            float newX = _initialPosition.x + offsetX;
            gameObject.transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
    }
}
