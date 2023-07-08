using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

[CreateAssetMenu(fileName = "DialogSequence.asset", menuName = "Content/Dialog")]
public class DialogSequence : ScriptableObject
{
   [SerializeField] private List<DialogLine> lines = new List<DialogLine>();
   public List<DialogLine> Lines => lines;

   [SerializeField] private GameEvent _onDialogEnd;
   public GameEvent OnDialogEnd => _onDialogEnd;

   public void SetLines(List<DialogLine> newLines)
   {
      lines = newLines;
   }

   public void SetEndEvent(GameEvent endEvent)
   {
      _onDialogEnd = endEvent;
   }
}
