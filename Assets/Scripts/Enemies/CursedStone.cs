using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;
using ScriptableObjectArchitecture.Examples;

public class CursedStone : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField] private int _baseHealth;
    private int _currentHealth;

    [SerializeField] private SpriteFlash _hitFlash;

    [SerializeField] private Sprite _damagedFrame;

    [SerializeField] private GameObjectCollection _damagingObjects;

    [SerializeField] private GameObject _shatterEffect;

    [SerializeField] private GameObject _damageSplashEffect;

    [SerializeField] private ImageFillSetter _healthbar;

    [SerializeField] private GameEvent _onDestroyEvent;
    
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _currentHealth = _baseHealth;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        // _healthbar.SetValues(_currentHealth, _baseHealth);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // get damage of colliding object
        if (_damagingObjects.Contains(other.gameObject))
        {
            _currentHealth -= 1;
            ServiceLocator.Instance.Get<AudioManager>().PlaySoundFromDictionary("Hit");
            
            Destroy(other.gameObject);
            
            _hitFlash.Flash();

            Instantiate(_damageSplashEffect, transform.position, Quaternion.identity);

            // _healthbar.SetValues(_currentHealth, _baseHealth);

            if (_currentHealth <= _baseHealth / 2)
            {
                _spriteRenderer.sprite = _damagedFrame;
            }

            if (_currentHealth <= 0)
            {
                Instantiate(_shatterEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
                _onDestroyEvent?.Raise();
            }
        }
    }
}
