using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using ScriptableObjectArchitecture;

public class Dialog : MonoBehaviour
{
    #region Inspector Settings
    [Header("CONTENT")]
    [Tooltip("The current dialog sequence (will be split into lines)")]
    [SerializeField] private DialogSequence _dialogSequence;

    [Tooltip("The text object to hold the current line of dialog")]
    [SerializeField] private TextMeshProUGUI textObject;
    public TextMeshProUGUI TextObject => textObject;

    [Tooltip("The lines of dialog to display (displayed one at a time)")]
    [SerializeField] private string[] lines;
    public string[] Lines => lines;
    private int index;

    [Header("UI")]
    [SerializeField] private GameObject _UITextbox;

    [Header("SETTINGS")]
    [Tooltip("Select a speed setting for the text typewriter (higher number = faster text)")]
    [Range(1, 10)]
    [SerializeField] private int typingSpeed;
    public int TypingSpeed => typingSpeed;
    IEnumerator typingCoroutine;
    public bool startOnAwake = false;
    #endregion
    
    #region Methods
    void Awake()
    {
        InputHandler _inputHandler = ServiceLocator.Instance.Get<InputManager>().Inputs();
        _inputHandler.Interact().performed += UpdateTextProgress;

        if (startOnAwake)
        {
            UpdateDialogSequence();
        }
    }

    void StartDialog()
    {
        typingCoroutine = TypeWriter();
        StartCoroutine(typingCoroutine);
    }

    void UpdateTextProgress(InputAction.CallbackContext context)
    {
        string currentLine = lines[index];

        // if the player has pressed the interact button before a line completes, immediately complete the whole line
        if (textObject.text != currentLine)
        {
            StopCoroutine(typingCoroutine);
            AutoCompleteSentence();
        }
        // if the line has completed and the user has pressed the interact button, get the next line
        else if (textObject.text == currentLine)
        {
            MoveToNextSentence();
            typingCoroutine = TypeWriter();
            StartCoroutine(typingCoroutine);
            CheckIfDialogIsComplete();
        }
    }

    IEnumerator TypeWriter()
    {
        string currentLine = lines[index];
        char[] charactersOfCurrentLine = currentLine.ToCharArray();
        int characterIndex = 0;

        if (textObject.text != currentLine)
        {
            while(characterIndex <= charactersOfCurrentLine.Length - 1)
            {
                // get the next character to display
                char currentCharacter = charactersOfCurrentLine[characterIndex];
                
                // increment the character index so that we can display another character on the next iteration
                characterIndex++;

                // get a portion of the full sentence to display (going all from the first character to the current character)
                string textToDisplay = currentLine.Substring(0, characterIndex);

                // if our current character is a space, don't wait to display next the character after it; add it to the display text immediately
                // (this is done to prevent the text writing from feeling slow and choppy when there are spaces between words)
                if (currentCharacter.ToString() == " " && characterIndex + 1 <= charactersOfCurrentLine.Length - 1)
                {
                    textToDisplay += charactersOfCurrentLine[characterIndex];
                    characterIndex++;
                }

                // for all characters that are not yet ready to be displayed, add them as a transparent substring (color=#00000000 is transparent)
                textToDisplay += "<color=#00000000>" + currentLine.Substring(characterIndex) + "</color>";

                // set our text object to equal the new string we created on this iteration
                textObject.text = textToDisplay;

                // if we have reached the end of the the line, set the text object to exactly match the current line
                // this is done so that it is clear we have reached the end of the line in other methods of the component
                if (characterIndex >= charactersOfCurrentLine.Length - 1)
                {
                    textObject.text = currentLine;
                }

                ServiceLocator.Instance.Get<AudioManager>().PlaySoundFromDictionary("Text");

                // wait for a set time before revealing the next character
                yield return new WaitForSeconds(1f/(float)typingSpeed);
            }
        }
    }

    void AutoCompleteSentence()
    {
        // fully complete a line of text if it has not finished
        if (textObject.text != lines[index])
        {
            textObject.text = lines[index];
        }
    }

    void CheckIfDialogIsComplete()
    {
        if (index >= lines.Length - 1 && textObject.text == lines[index])
        {
            CloseDialog();
        }
    }

    void CloseDialog()
    {
        _UITextbox.SetActive(false);
        StopCoroutine(typingCoroutine);
        
        if (_dialogSequence.OnDialogEnd != null)
        {
            _dialogSequence.OnDialogEnd.Raise();
        }
    }

    void MoveToNextSentence()
    {
        // increment the index so that we can grab the next line in TypeWriter()
        index += 1;

        // clamp the index to the maximum line count, so that we don't move past the final line 
        if (index > lines.Length - 1)
        {
            index = lines.Length - 1;
            return; // return so that we don't reset the final line to an empty string (see below)
        }
    
        // reset the text so that we have a clear textbox for the next line
        textObject.text = "";
    }

    public void UpdateDialogSequence()
    {
        string[] newLines = new string[_dialogSequence.Lines.Count];

        for (int i = 0; i < _dialogSequence.Lines.Count; i++)
        {
            newLines[i] = _dialogSequence.Lines[i].Content;
        }

        lines = newLines;
        index = 0;

        textObject.text = "";

        StartDialog();
    }

    public DialogLine GetCurrentDialogLine()
    {
        return _dialogSequence.Lines[index];
    }
    #endregion
}
