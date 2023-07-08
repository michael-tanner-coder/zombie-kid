using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Cutscene : MonoBehaviour
{
    [Tooltip("The text that will be displayed for this cutscene")]
    [SerializeField] private Dialog _textSequence;

    [Tooltip("UI object to hold the current dialog object's corresponding image")]
    [SerializeField] private Image _image;
    
    [Tooltip("Default color of the image object when no cutscene image is available")]
    [SerializeField] private Color _backgroundColor;

    [Tooltip("The scene to transition into when the cutscene is skipped or finished")]
    [SerializeField] private string _nextScene;

    void Awake()
    {
        InputHandler _inputHandler = ServiceLocator.Instance.Get<InputManager>().Inputs();
        _inputHandler.Exit().performed += SkipCutScene;
    }

    public void Update()
    {
        // switch the image sprite to the current cutscene image
        if (_image != null && _textSequence != null)
        {
            _image.sprite = _textSequence.GetCurrentDialogLine().Image;
        } 
        
        // set the default image to match the background color when no cutscene image is available
        if (_image.sprite == null)
        {
            _image.color = _backgroundColor;
        }
        else 
        {
            _image.color = Color.white;
        }
    }

    public void SkipCutScene(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(_nextScene);
    }
}
