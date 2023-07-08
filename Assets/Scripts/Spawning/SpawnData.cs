using UnityEngine;

public class SpawnData : MonoBehaviour
{
    [SerializeField]private float _xDirection = 0f;
    [SerializeField]private float _yDirection = 0f;

    public float XDirection => _xDirection;
    public float YDirection => _yDirection;
}
