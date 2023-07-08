using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class MoveWithCurve : MonoBehaviour
{
    private AnimationCurve _movementCurve;
    private float _baseY;

    void Awake()
    {
        _baseY = transform.position.y;
        _movementCurve = new AnimationCurve();
        _movementCurve.AddKey( new Keyframe( 0, 0.5f, 0, 0 ) );
        _movementCurve.AddKey( new Keyframe( 0.5f, 0f, 0, 0 ) );
        _movementCurve.AddKey( new Keyframe( 1, 0.5f, 0, 0 ) );
        _movementCurve.AddKey( new Keyframe( 1.5f, 0f, 0, 0 ) );
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, _baseY + _movementCurve.Evaluate((Time.time % _movementCurve.length)), transform.position.z);
    }
}
