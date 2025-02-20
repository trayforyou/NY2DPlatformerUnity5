using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private LayerMask _groundLayer;

    private SpriteRenderer _spriteRenderer;
    private bool _isGrounded;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private Vector2 _groundCheckerSize;

    private void Awake()
    {
        _groundCheckerSize = new Vector2(0.8f, 0.1f);
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        TryRun();
        TryJump();
        CheckFall();
    }

    private void CheckFall()
    {
        if (!_isGrounded && _rigidbody.velocity.y < 0)
        {
            _animator.SetBool("IsFall", true);
            _animator.SetBool("IsJump", false);
        }
        else if (!_isGrounded)
        {
            _animator.SetBool("IsJump", true);
            _animator.SetBool("IsFall", false);
        }
        else
        {
            _animator.SetBool("IsFall", false);
            _animator.SetBool("IsJump", false);
        }
    }

    private void TryRun()
    {
        float x = Input.GetAxis("Horizontal");
        _rigidbody.velocity = new Vector2(x * _speed, _rigidbody.velocity.y);

        if (x < 0)
            _spriteRenderer.flipX = true;
        if (x > 0)
            _spriteRenderer.flipX = false;

        if (_rigidbody.velocity.x != 0 && _isGrounded)
            _animator.SetBool("IsRuning", true);
        else
            _animator.SetBool("IsRuning", false);
    }

    private void TryJump()
    {
        _isGrounded = Physics2D.OverlapCapsule(_groundChecker.position, _groundCheckerSize, CapsuleDirection2D.Horizontal, 0, _groundLayer);

        if (_isGrounded && Input.GetButtonDown("Jump"))
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }


    }
}