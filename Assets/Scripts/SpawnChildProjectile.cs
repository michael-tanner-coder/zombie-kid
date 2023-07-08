using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChildProjectile : MonoBehaviour
{
    [SerializeField] private float _spawnOffset;
    [SerializeField] private GameObject _projectilePrefab;
    private Enemy _enemy;

    void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    public void Spawn()
    {
        List<EnemyAttributes> spawnableEnemies = _enemy.EnemyAttributes.SpawnableEnemies; 

        float angle = Mathf.Round(transform.rotation.z);
        foreach (EnemyAttributes spawnableEnemy in spawnableEnemies)
        {
            Debug.Log("angle");
            Debug.Log(angle);

            GameObject projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity) as GameObject;
            projectile.GetComponent<SpriteRenderer>().sprite = spawnableEnemy.AttackAnimation;

            MoveInOwnDirection moveComponent = projectile.GetComponent<MoveInOwnDirection>();
            if (moveComponent != null)
            {
                moveComponent.SetMovementDirectionFromAngle(angle);
            }

            angle += 180;
        }
    }
}
