
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IPlayerController {
    // Attributes
    public PlayerAttributes _playerAttributes;
    
    // Public members
    public Vector3 Velocity { get; private set; }
    public FrameInput Input { get; private set; }
    public Vector3 RawMovement { get; private set; }
    public bool Grounded => _collider.ColDown;

    // Private members
    private Vector3 _lastPosition;
    private float _currentHorizontalSpeed, _currentVerticalSpeed;

    private bool _active;
    private Vector2 movement = new Vector2(0f, 0f);
    void Awake() => Invoke(nameof(Activate), 0.5f);
    void Activate() =>  _active = true;

    void Start() 
    {
        ServiceLocator.Instance.Get<GameStateManager>().SetState(GameState.EXPLORATION);
    }
    
    private void Update() 
    {
        if(!_active) return;
        // Calculate velocity
        Velocity = (transform.position - _lastPosition) / Time.deltaTime;
        _lastPosition = transform.position;

        GatherInput();

        _collider.RunCollisionChecks();

        CalculateWalk(); 

        MoveCharacter(); 
    }

    #region Input
    private void GatherInput() 
    {
        InputHandler _inputHandler = ServiceLocator.Instance.Get<InputManager>().Inputs();
        movement = _inputHandler.Move().ReadValue<Vector2>();
        Input = new FrameInput {
            X = movement.x,
            Y = movement.y,
        };
    }

    #endregion

    #region Collisions
    
    [Header("COLLISION")]
    [SerializeField] private CustomCollider _collider;
    private RayRange _raysUp, _raysRight, _raysDown, _raysLeft;
    private float _timeLeftGrounded;

    #endregion


    #region Move

    [Header("MOVEMENT")]
    [SerializeField] private float _moveClamp = 13;
    [SerializeField] private float _deAcceleration = 60f;

    private void CalculateWalk() 
    {
        // HORIZONTAL MOVEMENT
        if (Input.X != 0) {
            // Set horizontal move speed
            _currentHorizontalSpeed += Input.X * _playerAttributes.MovementSpeed.CurrentValue * Time.deltaTime;

            // clamped by max frame movement
            _currentHorizontalSpeed = Mathf.Clamp(_currentHorizontalSpeed, -_moveClamp, _moveClamp);

            // Increase horizontal movement speed over time
            _currentHorizontalSpeed += Time.deltaTime;
        }
        else {
            // No input. Let's slow the character down
            _currentHorizontalSpeed = Mathf.MoveTowards(_currentHorizontalSpeed, 0, _deAcceleration * Time.deltaTime);
        }

        if (_currentHorizontalSpeed > 0 && _collider.ColRight || _currentHorizontalSpeed < 0 && _collider.ColLeft) {
            // Don't walk through walls
            _currentHorizontalSpeed = 0;
        }

        // VERTICAL MOVEMENT
        if (Input.Y != 0) {
            // Set horizontal move speed
            _currentVerticalSpeed += Input.Y * _playerAttributes.MovementSpeed.CurrentValue * Time.deltaTime;

            // clamped by max frame movement
            _currentVerticalSpeed = Mathf.Clamp(_currentVerticalSpeed, -_moveClamp, _moveClamp);

            // Increase vertical movement speed over time
            _currentVerticalSpeed += Time.deltaTime;
        }
        else {
            // No input. Let's slow the character down
            _currentVerticalSpeed = Mathf.MoveTowards(_currentVerticalSpeed, 0, _deAcceleration * Time.deltaTime);
        }
      
        if (_currentVerticalSpeed > 0 && _collider.ColUp || _currentVerticalSpeed < 0 && _collider.ColDown) {
            // Don't walk through walls
            _currentVerticalSpeed = 0;
        }
    }

    // We cast our bounds before moving to avoid future collisions
    private void MoveCharacter() 
    {
        var pos = transform.position + _collider.GetBounds().center;
        RawMovement = new Vector3(_currentHorizontalSpeed, _currentVerticalSpeed); // Used externally
        var move = RawMovement * Time.deltaTime;
        var furthestPoint = pos + move;

        // check furthest movement. If nothing hit, move and don't do extra checks
        var hit = Physics2D.OverlapBox(furthestPoint, _collider.GetBounds().size, 0, _collider.CollisionLayer);
        if (!hit) {
            transform.position += move;
            return;
        }

        // otherwise increment away from current pos; see what closest position we can move to
        var positionToMoveTo = transform.position;
        for (int i = 1; i < _collider.FreeColliderIterations; i++) {
            // increment to check all but furthestPoint - we did that already
            var t = (float)i / _collider.FreeColliderIterations;
            var posToTry = Vector2.Lerp(pos, furthestPoint, t);

            if (Physics2D.OverlapBox(posToTry, _collider.GetBounds().size, 0, _collider.CollisionLayer)) {
                transform.position = positionToMoveTo;

                // We've landed on a corner or hit our head on a ledge. Nudge the player gently
                if (i == 1) {
                    if (_currentVerticalSpeed < 0) _currentVerticalSpeed = 0;
                    var dir = transform.position - hit.transform.position;
                    transform.position += dir.normalized * move.magnitude;
                }

                return;
            }

            positionToMoveTo = posToTry;
        }
    }

    #endregion
}
