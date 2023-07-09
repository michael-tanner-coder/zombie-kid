using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GroupCommander : MonoBehaviour
{
    [SerializeField] Camera _mainCamera;
    [SerializeField] GameObject _cursorPrefab;
    [SerializeField] GameObject _groupPrefab;
    GameObject _cursor;
    Group _group;

    void Awake()
    {
        // cache our group component
        _group = GetComponent<Group>();

        // spawn cursor on mouse position
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        _cursor = Instantiate(_cursorPrefab, mousePosition, Quaternion.identity);

        // set up inputs
        InputHandler _inputHandler = ServiceLocator.Instance.Get<InputManager>().Inputs();
        _inputHandler.Send().performed += SendUnit;
        _inputHandler.Recall().performed += RecallUnit;
    }

    void Update()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector3 worldMousePosition = _mainCamera.ScreenToWorldPoint(mousePosition);
        Vector3 newCursorPosition = new Vector3(worldMousePosition.x, worldMousePosition.y, 0f);
        _cursor.transform.position = newCursorPosition;
    }

    void SendUnit(InputAction.CallbackContext context)
    {
        int newUnitAmount = _group.Formation.Amount - 1;
        _group.Formation.SetAmount(newUnitAmount);
    }

    void RecallUnit(InputAction.CallbackContext context)
    {
        int newUnitAmount = _group.Formation.Amount + 1;
        _group.Formation.SetAmount(newUnitAmount);
    }
}
