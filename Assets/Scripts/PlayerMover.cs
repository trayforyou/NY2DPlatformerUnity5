using System;
using System.Drawing;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(InputPlayer))]
[RequireComponent(typeof(Fliper))]
[RequireComponent(typeof(PlayerAttack))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private GroundDetector _groundDetector;

    private Rigidbody2D _rigidbody;
    private InputPlayer _inputPlayer;
    private Fliper _fliper;
    private PlayerAttack _playerAttack;
    private float _xMove;
    private bool _isTryJump;
    private bool _isRunning;
    private bool _isStaying;

    public event Action Ran;
    public event Action Stopped;

    private void Awake()
    {
        _xMove = 0;
        _isTryJump = false;
        _isRunning = false;
        _isStaying = true;
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputPlayer = GetComponent<InputPlayer>();
        _fliper = GetComponent<Fliper>();
        _playerAttack = GetComponent<PlayerAttack>();

        _inputPlayer.XInputed += MoveRun;
        _inputPlayer.JumpInputed += MoveJump;
        _playerAttack.Attacked += SkipState;
        _groundDetector.Jumped += SkipState;
        _groundDetector.Fell += SkipState;
    }

    private void FixedUpdate()
    {
        Jump();
        Run();
        Stay();
    }

    private void OnDisable()
    {
        _inputPlayer.XInputed -= MoveRun;
    }

    private void MoveJump(bool isTryJump)
    {
        if (_groundDetector.IsGrounded)
            _isTryJump = isTryJump;
    }

    private void MoveRun(float xMove) =>
        _xMove = xMove;

    private void Stay()
    {
        if (_groundDetector.IsGrounded)
        {
            if (_xMove == 0 && !_isStaying)
            {
                Stopped?.Invoke();

                Debug.Log("Встал");

                _isRunning = false;
                _isStaying = true;
            }
        }   
    }

    private void Run()
    {
        _rigidbody.velocity = new Vector2(_xMove * _speed, _rigidbody.velocity.y);

        _fliper.Flip(_xMove);

        if (_groundDetector.IsGrounded)
        {
            if (_xMove != 0 && !_isRunning)
            {
                Ran?.Invoke();

                _isRunning = true;
                _isStaying = false;
            }
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

    private void SkipState()
    {
        _isRunning = false;
        _isStaying = false;
    }
}