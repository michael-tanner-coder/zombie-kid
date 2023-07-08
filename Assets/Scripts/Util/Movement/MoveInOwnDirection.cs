using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class MoveInOwnDirection : MonoBehaviour
{
    [SerializeField]
    private float _xDirection = 0f;
    public float XDirection => _xDirection;
    
    [SerializeField]
    private float _yDirection = 0f;
    public float YDirection => _yDirection;

    [SerializeField]
    private FloatReference _speed;
    public FloatReference Speed => _speed;

    private float _instanceSpeed;

    void Awake()
    {
        _instanceSpeed = _speed.Value;
    }

    void Update()
    {
        float currentX = transform.position.x;
        float currentY = transform.position.y;

        float nextX = currentX + _xDirection * _instanceSpeed * Time.deltaTime;
        float nextY = currentY + _yDirection * _instanceSpeed * Time.deltaTime;

        transform.position = new Vector3(nextX, nextY, transform.position.z);
    }

    public void SetDirection(Vector2 direction)
    {
        _xDirection = direction.x;
        _yDirection = direction.y;
    }

    public void SetSpeed(float speed)
    {
        _instanceSpeed = speed;
    }

    public void FlipDirection()
    {
        _xDirection *= -1;
        _yDirection *= -1;

        UpdateRotation();
    }

    public void FlipYDirection()
    {
        _yDirection *= -1;

         UpdateRotation();
    }

    public void FlipXDirection()
    {
        _xDirection *= -1;

        UpdateRotation();
    }

    public void UpdateRotation()
    {
        float angle = Mathf.Atan2(_yDirection, _xDirection) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void SetMovementDirectionFromAngle(float angle)
    {
        float x = Mathf.Round(Mathf.Cos(angle * Mathf.PI / 180f));
        float y = Mathf.Round(Mathf.Sin(angle * Mathf.PI / 180f));
        SetDirection(new Vector2(x, y));
    }
}
