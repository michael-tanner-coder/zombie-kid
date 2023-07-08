using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SlowMoEffect : MonoBehaviour
{
    [SerializeField] private float _slowDownRate = 0.01f;
    [SerializeField] private float _slowMoTimer;
    [SerializeField] private UnityEvent _onEndEvent = new UnityEvent();
    private bool _activated = false;

    public void Activate()
    {
        _activated = true;
    }
    
    void Update()
    {
        if (_activated)
        {
            // lerp slow-mo
            Time.timeScale = Mathf.Lerp(Time.timeScale, 0f, _slowDownRate);
        }

        // scene change on countdown finish
        if (Time.timeScale <= 0.02f && _activated)
        {
            _slowMoTimer -= Time.unscaledDeltaTime;
            if (_slowMoTimer <= 0f)
            {
                Time.timeScale = 1f;
                _activated = false;
                _onEndEvent?.Invoke();
            }
        }
    }
}
