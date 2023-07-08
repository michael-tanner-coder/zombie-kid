using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ScriptableObjectArchitecture;

public class CombatRoom : Room
{
    [SerializeField] private bool _combatFinished;
    [SerializeField] private GameObject _spawnTrigger;
    [SerializeField] private EnemySpawnController _spawnController;
    [SerializeField] private GameObjectCollection _collidableObjects;
    [SerializeField] private List<GameObject> _exits = new List<GameObject>();

    void Awake()
    {
        base.Awake();
        _combatFinished = false;
    }

    void Update()
    {
        if (_spawnController.State == SpawnState.STOP)
        {
            _combatFinished = true;
            OpenExits();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_combatFinished && _collidableObjects.Contains(collision.gameObject))
        {
            CloseExits();
        }
    }

    public void CloseExits()
    {
        foreach(GameObject exit in _exits)
        {
            exit.SetActive(true);
        }
    }

    public void OpenExits()
    {
        foreach(GameObject exit in _exits)
        {
            exit.SetActive(false);
        }

        _spawnTrigger.SetActive(false);
    }
}
