using System;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Rigidbody2D _rigidbodyCharacter;

    private bool _isGrounded;
    private Vector2 _groundCheckerSize;

    public event Action<bool> Fell;
    
    public bool IsGrounded => _isGrounded;

    private void Awake()
    {
        float widthCapsule = 0.8f;
        float heightCapsule = 0.1f;

        _groundCheckerSize = new Vector2(widthCapsule, heightCapsule);
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCapsule(transform.position, _groundCheckerSize, CapsuleDirection2D.Horizontal, 0, _groundLayer);

        CheckFall();
    }

    private void CheckFall()
    {
        if (!_isGrounded && _rigidbodyCharacter.velocity.y < 0)
            Fell?.Invoke(true);
        else
            Fell?.Invoke(false);
    }
}
