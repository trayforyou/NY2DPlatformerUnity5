using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private Fliper _fliper;
    [SerializeField] private PlayerAnimationChanger _playerAnimationChanger;

    private Rigidbody2D _rigidbody;
    private PlayerInput _inputPlayer;
    private float _xMove;
    private bool _isTryJump;
    private bool _isJump;
    private bool _isFall;

    private void Awake()
    {
        _xMove = 0;
        _isTryJump = false;
        _isJump = false;
        _isFall = false;
        _inputPlayer = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _inputPlayer.XInputed += MoveRun;
        _inputPlayer.JumpInputed += MoveJump;
    }

    private void FixedUpdate()
    {
        Jump();
        Run();
        Stay();
        CheckFall();
    }

    private void OnDisable()
    {
        _inputPlayer.XInputed -= MoveRun;
        _inputPlayer.JumpInputed -= MoveJump;
    }

    private void MoveJump(bool isTryJump)
    {
        if (_groundDetector.IsGrounded)
            _isTryJump = isTryJump;
    }

    private void Stay()
    {
        if (_groundDetector.IsGrounded && _xMove == 0)
            _playerAnimationChanger.ChangeIdleAnimation();
    }

    private void Run()
    {
        _rigidbody.velocity = new Vector2(_xMove * _speed, _rigidbody.velocity.y);

        _fliper.Flip(_xMove);

        if (_groundDetector.IsGrounded && _xMove != 0)
        {
            _playerAnimationChanger.ChangeRunAnimation();
        }
    }

    private void Jump()
    {
        if (_isTryJump)
        {
            _isTryJump = false;

            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    private void CheckFall()
    {
        if (_groundDetector.IsGrounded == false)
        {
            if (!_isJump && _rigidbody.velocity.y > 0)
            {
                _playerAnimationChanger.ChangeJumpAnimation();

                _isJump = true;
                _isFall = false;
            }
            else if (!_isFall && _rigidbody.velocity.y < 0)
            {
                _playerAnimationChanger.ChangeFallAnimation();

                _isJump = false;
                _isFall = true;
            }
        }
        else
        {
            _isJump = false;
            _isFall = false;
        }
    }

    private void MoveRun(float xMove) =>
       _xMove = xMove;
}