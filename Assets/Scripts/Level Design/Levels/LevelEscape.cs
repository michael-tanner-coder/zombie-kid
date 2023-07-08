using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEscape : MonoBehaviour, IInteractable
{
    [Header("INTERACTIONS")]
    [SerializeField] private string _interactionPrompt;

    public string Prompt {get; set; }
    public bool InteractionEnabled {get; set; }

    void Start()
    {
        Prompt = _interactionPrompt;
        InteractionEnabled = true;
    }

    public void ReceiveInteraction(GameObject interactor)
    {
       ServiceLocator.Instance.Get<LevelManager>().EscapeLevel();
    }
}
