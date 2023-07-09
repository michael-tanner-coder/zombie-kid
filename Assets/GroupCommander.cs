using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GroupCommander : MonoBehaviour
{
    [SerializeField] Camera _mainCamera;
    [SerializeField] GameObject _cursorPrefab;
    [SerializeField] GameObject _groupPrefab;
    [SerializeField] GameObject _whistleRadiusPrefab;
    [SerializeField] GameObject _waypointPrefab;
    GameObject _cursor;
    GameObject _whistleRadius;
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
        _inputHandler.Recall().canceled += StopRecall;
    }

    void Update()
    {
        // render cursor position
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector3 worldMousePosition = _mainCamera.ScreenToWorldPoint(mousePosition);
        Vector3 newCursorPosition = new Vector3(worldMousePosition.x, worldMousePosition.y, 0f);
        _cursor.transform.position = newCursorPosition;
    }

    void SendUnit(InputAction.CallbackContext context)
    {
        int newUnitAmount = _group.Formation.Amount - 1;
        _group.Formation.SetAmount(newUnitAmount);

        // create friendly group of 1 unit
        GameObject singleUnitGroup = Instantiate(_groupPrefab, transform.position, Quaternion.identity);
        Group newGroup = singleUnitGroup.GetComponent<Group>();
        newGroup.SetFriend(_group.friendTag);
        newGroup.SetFoe(_group.foeTag);
        newGroup.Formation.SetAmount(1);

        // create a waypoint for the unit to follow
        GameObject wayPoint = new GameObject("Waypoint");
        wayPoint.transform.position = _cursor.transform.position;

        // make the friendly group chase the waypoint
        GroupController AIController = singleUnitGroup.GetComponent<GroupController>();
        AIController.target = wayPoint;
        AIController.SetState(GroupState.CHASE);

    }

    void RecallUnit(InputAction.CallbackContext context)
    {
        _whistleRadius = Instantiate(_whistleRadiusPrefab, _cursor.transform.position, Quaternion.identity);
        _whistleRadius.GetComponent<WhistleRadius>().caller = gameObject;
    }

    void StopRecall(InputAction.CallbackContext context)
    {
        if (_whistleRadius != null)
        {
            Destroy(_whistleRadius);
        }
    }
}
