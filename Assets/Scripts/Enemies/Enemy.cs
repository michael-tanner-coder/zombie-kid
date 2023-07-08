using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyAttributes _attributes;
    public EnemyAttributes EnemyAttributes => _attributes;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpawnVFX _vfxSpawner;   
    [SerializeField] private GameObjectCollection _collidableObjects;
    [SerializeField] private UnityEvent _enterResponse;

    [SerializeField] private float _timeUntilLethal = 0.5f;
    private bool _lethal = false;
    public bool Lethal => _lethal;

    void Awake()
    {
        // get necesssary components
        _renderer = gameObject.GetComponent<SpriteRenderer>();
        _animator = gameObject.GetComponent<Animator>();

        // apply attributes to individual components
        SetAttributes(_attributes);
    }


    public void SetAttributes(EnemyAttributes attributes)
    {
        _attributes = attributes;
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (_collidableObjects.Contains(other.gameObject) && _lethal)
        {
            _enterResponse?.Invoke();
        }    
    }
}
