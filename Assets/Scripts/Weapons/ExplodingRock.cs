using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingRock : MonoBehaviour
{
    [SerializeField] private float _timeToExplode;
    private float _intialTimeToExplode;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Sprite _projectileRockSprite;
    [SerializeField] private float _maxShakeOffset;
    Vector2 _initialPosition = new Vector2(0f, 0f);


    void Start()
    {
        _initialPosition.x = gameObject.transform.position.x;
        _initialPosition.y = gameObject.transform.position.y;
        _intialTimeToExplode = _timeToExplode;
    }

    void Update()
    {
        // shake over time
        float shake = Mathf.Pow((_intialTimeToExplode - _timeToExplode) / _intialTimeToExplode, 2);
        float offsetX = shake * _maxShakeOffset * Random.Range(-1, 1);
        float newX = _initialPosition.x + offsetX;
        float newY = transform.position.y;
        gameObject.transform.position = new Vector3(newX, newY, gameObject.transform.position.z);

        // spawn projectile rocks
        _timeToExplode -= Time.deltaTime;
        if (_timeToExplode <= 0)
        {
            float angle = 0;

            for(int i = 0; i <= 3; i++) 
            {
                // set spawn position
                Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                
                // spawn object
                GameObject _newProjectile = Instantiate(_projectilePrefab, spawnPosition, Quaternion.identity);

                // change sprite to match the rock projectile 
                SpriteRenderer _renderer = _newProjectile.GetComponent<SpriteRenderer>();
                if (_renderer != null)
                {
                    _renderer.sprite = _projectileRockSprite;
                }

                // give the new projectile a default ammo type
                Bullet bulletProperties = _newProjectile.GetComponent<Bullet>();
                bulletProperties.SetAmmoType(AmmoType.NONE);

                // set rotation
                _newProjectile.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

                // move each projectile in a cardinal direction
                MoveInOwnDirection moveComponent = _newProjectile.GetComponent<MoveInOwnDirection>();
                if (moveComponent != null)
                {
                    _newProjectile.GetComponent<MoveInOwnDirection>().SetMovementDirectionFromAngle(angle);
                }

                // increment the angle for the next iteration
                angle += 90;
            }

            Destroy(gameObject);
        }



    }
}
