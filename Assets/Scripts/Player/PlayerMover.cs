using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Fliper))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private GroundDetector _groundDetector;

    private Rigidbody2D _rigidbody;
    private PlayerInput _inputPlayer;
    private Fliper _fliper;
    private float _xMove;
    private bool _isTryJump;

    public event Action Ran;
    public event Action Stopped;

    private void Awake()
    {
        _xMove = 0;
        _isTryJump = false;
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputPlayer = GetComponent<PlayerInput>();
        _fliper = GetComponent<Fliper>();

        _inputPlayer.XInputed += MoveRun;
        _inputPlayer.JumpInputed += MoveJump;
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
                Stopped?.Invoke();
    }

    private void Run()
    {
        _rigidbody.velocity = new Vector2(_xMove * _speed, _rigidbody.velocity.y);

        _fliper.Flip(_xMove);

        if (_groundDetector.IsGrounded && _xMove != 0)
                Ran?.Invoke();         
    }

    private void Jump()
    {
        if (_isTryJump)
        {
            _isTryJump = false;

            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    private void MoveRun(float xMove) =>
       _xMove = xMove;
}