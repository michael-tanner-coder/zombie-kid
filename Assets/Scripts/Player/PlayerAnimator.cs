using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.InputSystem;
using ScriptableObjectArchitecture;

public class PlayerAnimator : MonoBehaviour 
{
    [SerializeField] private Animator _anim;
    [SerializeField] private AudioSource _source;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private ParticleSystem _moveParticles, _landParticles;
    [SerializeField] private AudioClip[] _footsteps;
    [SerializeField] private float _maxTilt = .1f;
    [SerializeField] private float _tiltSpeed = 1;
    [SerializeField, Range(1f, 3f)] private float _maxIdleSpeed = 2;
    [SerializeField] private float _maxParticleFallSpeed = -40;
    [SerializeField] private Vector2Variable _aimDirection;

    private IPlayerController _player;
    private bool _playerGrounded;
    private ParticleSystem.MinMaxGradient _currentGradient;
    private Vector2 _movement;

    [SerializeField] private float _timeBetweenFootsteps = 0.5f;
    private float _footStepTimer;

    void Awake() 
    {
        _player = GetComponentInParent<IPlayerController>();
        _footStepTimer = _timeBetweenFootsteps;

        // Kick dust on direction change
        InputHandler _inputHandler = ServiceLocator.Instance.Get<InputManager>().Inputs();
        _inputHandler.Shoot().performed += StartShoot;
        // _inputHandler.Move().canceled += _ => StopRun();
    }

    void Update() 
    {
        InputHandler _inputHandler = ServiceLocator.Instance.Get<InputManager>().Inputs();
        Vector2 Input = _inputHandler.Move().ReadValue<Vector2>();

        // Flip the sprite
        _anim.SetFloat("Horizontal", Input.x);
        _anim.SetFloat("Vertical", Input.y);
        _anim.SetFloat("Speed", Mathf.Abs(Input.x) + Mathf.Abs(Input.y));
        
        // Lean while running
        var targetRotVector = new Vector3(0, 0, Mathf.Lerp(-_maxTilt, _maxTilt, Mathf.InverseLerp(-1, 1, _player.Input.X)));
        // _anim.transform.rotation = Quaternion.RotateTowards(_anim.transform.rotation, Quaternion.Euler(targetRotVector), _tiltSpeed * Time.deltaTime);

        // Speed up idle while running
        // _anim.SetFloat(IdleSpeedKey, Mathf.Lerp(1, _maxIdleSpeed, Mathf.Abs(_player.Input.X)));

        // Footstep sound loop
        _footStepTimer -= Time.deltaTime;
        if ((_anim.GetFloat("Speed") >= 0.1f) && _footStepTimer < 0f) 
        {
            ServiceLocator.Instance.Get<AudioManager>().PlaySoundFromDictionary("Footstep");
        }

        if (_footStepTimer < 0f)
        {
            _footStepTimer = _timeBetweenFootsteps;
        }

        // Detect ground color
        var groundHit = Physics2D.Raycast(transform.position, Vector3.down, 2, _groundMask);
        if (groundHit && groundHit.transform.TryGetComponent(out SpriteRenderer r)) 
        {
            _currentGradient = new ParticleSystem.MinMaxGradient(r.color * 0.9f, r.color * 1.2f);
            SetColor(_moveParticles);
        }

        _movement = _player.RawMovement; // Previous frame movement is more valuable
    }

    private void OnDisable() 
    {
        _moveParticles.Stop();
    }

    private void OnEnable() 
    {
        _moveParticles.Play();
    }

    void SetColor(ParticleSystem ps) 
    {
        var main = ps.main;
        main.startColor = _currentGradient;
    }

    void StartShoot(InputAction.CallbackContext context)
    {
        float _aimDirectionX = context.ReadValue<Vector2>().x;
        float _aimDirectionY = context.ReadValue<Vector2>().y;
        _anim.SetFloat("ShootHorizontal", _aimDirectionX);
        _anim.SetFloat("ShootVertical", _aimDirectionY);
        _anim.SetTrigger("Shooting");
    }

    void StartRun()
    {
        KickDust();
        _anim.SetTrigger("Run");
    }

    void StopRun()
    {
        _anim.SetTrigger("Idle");
    } 

    void KickDust()
    {
        if(!_moveParticles.isPlaying) {
            _moveParticles.Play();
            return;
        }

        if(_moveParticles.isPlaying) {
            _moveParticles.Stop();
            _moveParticles.Play();
        }
    }

    #region Animation Keys

    // private static readonly int GroundedKey = Animator.StringToHash("Grounded");
    private static readonly int IdleSpeedKey = Animator.StringToHash("IdleSpeed");
    // private static readonly int JumpKey = Animator.StringToHash("Jump");

    #endregion
}
