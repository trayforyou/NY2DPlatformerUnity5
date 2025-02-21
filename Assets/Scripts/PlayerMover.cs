using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(InputPlayer))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private LayerMask _groundLayer;

    private Rigidbody2D _rigidbody;
    private InputPlayer _inputPlayer;
    private Vector2 _groundCheckerSize;
    private Vector3 _seeOnRight;
    private Vector2 _seeOnLeft;
    private bool _isGrounded;

    public event Action<bool> Ran;
    public event Action<bool> Jumped;
    public event Action<bool> Fell;

    private void Awake()
    {
        float widthCapsule = 0.8f;
        float heightCapsule = 0.1f;

        _rigidbody = GetComponent<Rigidbody2D>();
        _inputPlayer = GetComponent<InputPlayer>();
        _groundCheckerSize = new Vector2(widthCapsule, heightCapsule);
        _seeOnLeft = new Vector3(0, 180, 0);
        _seeOnRight = Vector3.zero;

        _inputPlayer.XInputed += Run;
        _inputPlayer.JumpInputed += Jump;
    }

    private void Update()
    {
        CheckFall();
    }

    private void OnDisable()
    {
        _inputPlayer.XInputed -= Run;
        _inputPlayer.JumpInputed -= Jump;
    }

    private void CheckFall()
    {
        _isGrounded = Physics2D.OverlapCapsule(_groundChecker.position, _groundCheckerSize, CapsuleDirection2D.Horizontal, 0, _groundLayer);

        if (!_isGrounded && _rigidbody.velocity.y < 0)
        {
            Jumped?.Invoke(false);
            Fell?.Invoke(true);
        }
        else
        {
            Fell?.Invoke(false);
        }
    }

    private void Run(float xInput)
    {
        _rigidbody.velocity = new Vector2(xInput * _speed, _rigidbody.velocity.y);

        if (xInput > 0)
            transform.rotation = Quaternion.Euler(_seeOnRight);

        if (xInput < 0)
            transform.rotation = Quaternion.Euler(_seeOnLeft);

        if (xInput != 0 && _isGrounded)
            Ran?.Invoke(true);
        else
            Ran?.Invoke(false);
    }

    private void Jump()
    {
        if (_isGrounded)
        {
            Jumped?.Invoke(true);

            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }
}