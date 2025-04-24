using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Fliper))]
[RequireComponent(typeof(PlayerAnimationChanger))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private GroundDetector _groundDetector;

    private Rigidbody2D _rigidbody;
    private PlayerInput _inputPlayer;
    private PlayerAnimationChanger _playerAnimationChanger;
    private Fliper _fliper;
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
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputPlayer = GetComponent<PlayerInput>();
        _fliper = GetComponent<Fliper>();
        _playerAnimationChanger = GetComponent<PlayerAnimationChanger>();
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
                _playerAnimationChanger.ChangeRunAnimation();         
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
        if (_groundDetector)
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