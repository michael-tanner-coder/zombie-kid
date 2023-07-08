using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ScriptableObjectArchitecture;

public class NPC : MonoBehaviour, IInteractable
{
    [Header("DIALOG")]
    [SerializeField, HideCustomDrawer] private DialogSequence _activeDialogueSequence;
    [SerializeField] private DialogSequence _npcDialogueSequence;
    [SerializeField] private List<DialogSequence> _availableDialog;
    [SerializeField] private DialogFlagDictionary _flaggedDialog;
    public bool useFlags = false;
    public bool randomDialog = false;
    public bool randomLine = false;
    
    [Header("EVENTS")]
    [SerializeField] private GameEvent _onDialogStart;
    [SerializeField] private GameEvent _onDialogEnd;
    
    [Header("INTERACTIONS")]
    [SerializeField] private string _interactionPrompt;

    public string Prompt {get; set; }
    public bool InteractionEnabled {get; set; }

    void Start()
    {
        Prompt = _interactionPrompt;
        InteractionEnabled = true;

        if (useFlags)
        {
            foreach(string flag in _flaggedDialog.Keys)
            {
                bool flagStatus = ServiceLocator.Instance.Get<SaveDataManager>().GetFlag(flag);
                if (flagStatus)
                {
                    SetCurrentDialogSequence(_flaggedDialog[flag]);
                }
            }
        }
    }

    public void SetCurrentDialogSequence(DialogSequence sequence)
    {
        _npcDialogueSequence = sequence;
    }

    public DialogSequence GetRandomDialogSequence()
    {
        return _availableDialog[Random.Range(0, _availableDialog.Count)];
    }

    public List<DialogLine> GetRandomDialogLine(DialogSequence dialog)
    {
        List<DialogLine> randomLines = new List<DialogLine>();
        randomLines.Add(dialog.Lines[Random.Range(0, dialog.Lines.Count)]);
        return randomLines;
    }

    public void ReceiveInteraction(GameObject interactor)
    {
       if (_npcDialogueSequence != null && _activeDialogueSequence != null)
       {
            DialogSequence selectedDialogSequence = randomDialog ? GetRandomDialogSequence() : _npcDialogueSequence;
            List<DialogLine> selectedDialogLines = randomLine ? GetRandomDialogLine(selectedDialogSequence) : selectedDialogSequence.Lines;
            _activeDialogueSequence.SetLines(selectedDialogLines);
            _activeDialogueSequence.SetEndEvent(_onDialogEnd ? _onDialogEnd : selectedDialogSequence.OnDialogEnd);
            _onDialogStart?.Raise();
            InteractionEnabled = false;
       }
    }
}
