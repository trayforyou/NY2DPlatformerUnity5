using System;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Rigidbody2D _rigidbodyCharacter;

    private bool _isGrounded;
    private bool _isJump;
    private bool _isFall;
    private Vector2 _groundCheckerSize;

    public event Action Fell;
    public event Action Jumped;
    
    public bool IsGrounded => _isGrounded;

    private void Awake()
    {
        float widthCapsule = 0.8f;
        float heightCapsule = 0.1f;
        _isJump = false;
        _isFall = false;
        _groundCheckerSize = new Vector2(widthCapsule, heightCapsule);
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCapsule(transform.position, _groundCheckerSize, CapsuleDirection2D.Horizontal, 0, _groundLayer);

        CheckFall();
    }

    private void CheckFall()
    {
        if(!_isGrounded)
        {
            if (!_isJump && _rigidbodyCharacter.velocity.y > 0)
            {
                Jumped?.Invoke();

                _isJump = true;
                _isFall = false;
            }
            else if(!_isFall && _rigidbodyCharacter.velocity.y < 0)
            {
                Fell?.Invoke();

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
}