using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    [SerializeField] private float _damage = 0f;
    public float Damage => _damage;

    public void SetDamage(float damage)
    {
        _damage = damage;
    }
}
