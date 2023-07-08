// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class ScoreGrave : MonoBehaviour, IInteractable
// {
//     [Header("INTERACTIONS")]
//     [SerializeField] private string _interactionPrompt;
    
//     public string Prompt {get; set; }
//     public bool InteractionEnabled {get; set; }

//     void Start()
//     {
//         Prompt = _interactionPrompt;
//         InteractionEnabled = true;
//     }

//     public void ReceiveInteraction(GameObject interactor)
//     {
//        if (_npcDialogueSequence != null && _activeDialogueSequence != null)
//        {
//             _activeDialogueSequence.SetLines(_npcDialogueSequence.Lines);
//             _activeDialogueSequence.SetEndEvent(_npcDialogueSequence.OnDialogEnd);
//             _onDialogStart?.Raise();
//             InteractionEnabled = false;
//        }
//     }
// }
