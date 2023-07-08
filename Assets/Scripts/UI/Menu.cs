using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Menu : MonoBehaviour
{
    [Header("NAVIGATION")]
    [Tooltip("The object used to highlight the current menu item")]
    [SerializeField] protected GameObject _cursor;

    [Tooltip("The interactive fields contained in this menu")]
    [SerializeField] protected List<InputField> _fields = new List<InputField>();
    public List<InputField> Fields => _fields;

    [SerializeField] protected int _cursorIndex;
    
    [Header("TEXT")]
    [Tooltip("The text to explain the current menu item's functionality")]
    [SerializeField] protected TMP_Text _explanationText;

    protected void Start()
    {
        _cursorIndex = 0;

        SetActiveInput();
        SetExplanationText(GetCurrentField().Description);

        InputHandler _inputHandler = ServiceLocator.Instance.Get<InputManager>().Inputs();
        _inputHandler.Navigate().performed += NavigateMenu;
    }

    void NavigateMenu(InputAction.CallbackContext context)
    {
        int newCursorPosition = _cursorIndex + (int) context.ReadValue<Vector2>().y * -1;
        SetCursorIndex(newCursorPosition);
        SetCursorPosition();
        SetActiveInput();
        SetExplanationText(GetCurrentField().Description);
    }

    protected void SetCursorIndex(int index)
    {
         // get the attempted new cursor position
        _cursorIndex = index;

        // loop back to start of menu list
        if (_cursorIndex > _fields.Count - 1)
        {
            _cursorIndex = 0;
        }

        // loop to end of menu list
        if (_cursorIndex < 0)
        {
            _cursorIndex = _fields.Count - 1;
        }
    }

    protected void SetCursorPosition()
    {
        // move the menu cursor to the current field
        if (_fields.Count > 0 && _fields[_cursorIndex] != null)
        {
            GameObject currentField = _fields[_cursorIndex].gameObject;
            Vector3 currentFieldPosition = currentField.transform.position;

            Vector3 cursorPosition = _cursor.transform.position;
            _cursor.transform.position = new Vector3(cursorPosition.x, currentFieldPosition.y, cursorPosition.z);

            ServiceLocator.Instance.Get<AudioManager>().PlaySoundFromDictionary("Interact");
        }
    }

    protected void SetActiveInput()
    {
        foreach(InputField field in _fields)
        {
            field.SetInputEnabled(false);
        }

        if (_fields.Count > 0 && _fields[_cursorIndex] != null)
        {
            _fields[_cursorIndex].SetInputEnabled(true);
        }
    }

    public InputField GetActiveInput()
    {
        InputField activeInput = null;
        
        foreach(InputField field in _fields)
        {
            if (field.InputEnabled)
            {
                activeInput = field;
            }
        }

        return activeInput;
    }

    protected InputField GetCurrentField()
    {
        if (_fields.Count > 0 && _fields[_cursorIndex] != null)
        {
            return _fields[_cursorIndex]; 
        }

        return new InputField();
    }

    protected void SetExplanationText(string newText)
    {
        if (_explanationText != null)
        {
            _explanationText.text = newText;
        }
    }
}
