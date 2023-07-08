using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyAttributes _parameters;
    public EnemyAttributes EnemyAttributes => _parameters;
    [SerializeField] private EnemyHealth _health;
    [SerializeField] private MoveTowardClosest _movement;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private CustomCollider _hurtBox;
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
        _health = gameObject.GetComponent<EnemyHealth>();
        _movement = gameObject.GetComponent<MoveTowardClosest>();
        _hurtBox = gameObject.GetComponent<CustomCollider>();
        _animator = gameObject.GetComponent<Animator>();

        // apply parameters to individual components
        SetAttributes(_parameters);
    }

    void Update()
    {
        // time until the enemy can hurt the player
        _timeUntilLethal -= Time.deltaTime;
        if (_timeUntilLethal <= 0f)
        {
            _lethal = true;
        }

        // colision checks for projectile damage
        _hurtBox.RunCollisionChecks();
        if (_hurtBox.ColDown || _hurtBox.ColUp || _hurtBox.ColLeft || _hurtBox.ColRight)
        {
            _health.OnHit(_hurtBox.CurrentHit, 1);
        }
    }

    public void SetAttributes(EnemyAttributes parameters)
    {
        _parameters = parameters;
        _renderer.sprite = _parameters.AttackAnimation;
        _health.SetHealth(_parameters.Health.CurrentValue);
        _movement.SetSpeed(parameters.MoveSpeed.CurrentValue);
        _animator.runtimeAnimatorController = parameters.Animation;
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (_collidableObjects.Contains(other.gameObject) && _lethal)
        {
            _enterResponse?.Invoke();
        }    
    }
}
