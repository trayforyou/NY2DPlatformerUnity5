using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(InputPlayer))]
[RequireComponent(typeof(Fliper))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private GroundDetector _groundDetector;

    private Rigidbody2D _rigidbody;
    private InputPlayer _inputPlayer;
    private Fliper _fliper;
    private float _xMove;
    private bool _isTryJump;

    public event Action<bool> Ran;
    public event Action<bool> Jumped;

    private void Awake()
    {
        _xMove = 0;
        _isTryJump = false;
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputPlayer = GetComponent<InputPlayer>();
        _fliper = GetComponent<Fliper>();

        _inputPlayer.XInputed += MoveRun;
        _inputPlayer.JumpInputed += MoveJump;
    }

    private void FixedUpdate()
    {
        Jump();
        Run();
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

    private void MoveRun(float xMove) =>
        _xMove = xMove;

    private void Run()
    {
        _rigidbody.velocity = new Vector2(_xMove * _speed, _rigidbody.velocity.y);

        _fliper.Flip(_xMove);

        if (_xMove != 0 && _groundDetector.IsGrounded)
            Ran?.Invoke(true);
        else
            Ran?.Invoke(false);
    }

    private void Jump()
    {
        if (_isTryJump)
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);

            Jumped?.Invoke(true);
            _isTryJump = false;
        }
        else if (_rigidbody.velocity.y < 0)
        {
            Jumped?.Invoke(false);
        }
    }
}