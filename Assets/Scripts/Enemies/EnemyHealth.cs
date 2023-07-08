using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float _health;
    public float Health => _health;
    public float HealthMax { get; private set; }

    [Header("Collisions")]
    [SerializeField] private GameObjectCollection _damagingObjects;
    public GameObjectCollection DamagingObjects => _damagingObjects;
    private BoxCollider2D _collider;

    [Header("Events")]
    [SerializeField] private UnityEvent _damageEvents = new UnityEvent();
    [SerializeField] private UnityEvent _deathEvents = new UnityEvent();

    [Header("Visual Effects")]
    [SerializeField] private GameObject _explosion;
    [SerializeField] private GameObject _damageSplash;

    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        HealthMax = _health;
    }

    public void SetHealth(float health)
    {
        _health = health;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
    }

    public void OnHit(RaycastHit2D hit, float baseDamage)
    {
        // get damage data of colliding object
        DamageController damageController = hit.collider.gameObject.GetComponent<DamageController>();
        float damageAmount = 1;
        if (damageController != null)
        {
            damageAmount = damageController.Damage;
        }

        // apply damage based on x and y values
        TakeDamage(damageAmount * baseDamage);

        // invoke follow-up damage events
        _damageEvents?.Invoke();

        // spawn particle effects
        Instantiate(_damageSplash, transform.position, transform.rotation);

        // get ammo data of the projectile
        Bullet bullet = hit.collider.gameObject.GetComponent<Bullet>();

        // get type of ammo
        if (bullet != null)
        {
            // based on ammo type, affect the enemy gameobject
            bullet.ApplyAmmoEffect(gameObject);
        }

        // remove colliding object from scene
        if (bullet.CanBeDestoyedByEnemies)
        {
            Destroy(hit.collider.gameObject);
        }

        ServiceLocator.Instance.Get<AudioManager>().PlaySoundFromDictionary("Hit");
    }

    void Update()
    {
        if (_health <= 0)
        {
            _deathEvents?.Invoke();
            Instantiate(_explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
