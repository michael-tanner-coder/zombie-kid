using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class CombatTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _spawner;
    [SerializeField] private GameObjectCollection _collidableObjects;
    [SerializeField] private List<GameObject> _exits = new List<GameObject>();
    private bool _combatStarted = false;

    public void Update()
    {
        if (ServiceLocator.Instance.Get<GameStateManager>().State == GameState.COMBAT)
        {
            CloseExits();
        }
        else 
        {
            OpenExits();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (_collidableObjects.Contains(collision.gameObject) && !_combatStarted)
        {
            _spawner.SetActive(true);
            _spawner.GetComponent<EnemySpawnController>().ResetState();
            _combatStarted = true;
            ServiceLocator.Instance.Get<GameStateManager>().SetState(GameState.COMBAT);
            ServiceLocator.Instance.Get<AudioManager>().PlaySoundFromDictionary("GameOver");
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
    }
}
