using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwerve : MonoBehaviour
{
    [SerializeField] public EnemyAttributes enemy;
    
    MoveInOwnDirection _movement;

    bool _swerveXAxis = false;
    bool _swerveYAxis = false;

    void Start()
    {
        _movement = gameObject.GetComponent<MoveInOwnDirection>();

        if (_movement != null && _movement.XDirection == 0f)
        {
            _swerveXAxis = true;
        }

        if (_movement != null && _movement.YDirection == 0f)
        {
            _swerveYAxis = true;
        }
    }

    void Update()
    {
        float swerveDirection = Mathf.Sin(Time.time * enemy.SwerveFrequency.CurrentValue) * Time.deltaTime * enemy.SwerveAmplitude.CurrentValue;
        float newXDirection = _swerveXAxis ? swerveDirection : _movement.XDirection;
        float newYDirection = _swerveYAxis ? swerveDirection : _movement.YDirection;

        Vector2 newDirection = new Vector2(newXDirection, newYDirection);

        _movement.SetDirection(newDirection);
    }
}
