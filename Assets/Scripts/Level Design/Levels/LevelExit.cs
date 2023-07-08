using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ScriptableObjectArchitecture;

public class LevelExit : MonoBehaviour, IInteractable
{
    [Header("INTERACTIONS")]
    [SerializeField] private string _interactionPrompt;
    public string Prompt {get; set; }
    public bool InteractionEnabled {get; set; }

    [Header("LEVEL TRANSITION")]
    [SerializeField] private GameObject _endPoint;
    [SerializeField] private GameEvent _onLevelTransition;

    void Start()
    {
        Prompt = _interactionPrompt;
        InteractionEnabled = true;
    }

    public void MoveToNextLevel(GameObject player)
    {
        player.transform.position = _endPoint.transform.position;
        _onLevelTransition?.Raise();
    }

    public void ReceiveInteraction(GameObject interactor)
    {
       ServiceLocator.Instance.Get<LevelManager>().GoToNextLevel();
       
       MoveToNextLevel(interactor);
    }
}
