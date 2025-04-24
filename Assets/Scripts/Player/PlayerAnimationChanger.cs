using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationChanger : MonoBehaviour
{
    [SerializeField] private GroundDetector _groundDetector;

    private Animator _animator;
    private int _currentState;
    private bool _canChangeState;

    public void ChangeFallAnimation()
    {
        if (_currentState != AnimatorData.Params.Fall && _canChangeState)
        {
            _animator.Play(AnimatorData.Params.Fall);
            
            _currentState = AnimatorData.Params.Fall;
        }
    }

    public void ChangeJumpAnimation()
    {
        if (_currentState != AnimatorData.Params.Jump && _canChangeState)
        {
            _animator.Play(AnimatorData.Params.Jump);
            
            _currentState = AnimatorData.Params.Jump;
        }
    }

    public void ChangeRunAnimation()
    {
        if (_currentState != AnimatorData.Params.Run && _canChangeState)
        {
            _animator.Play(AnimatorData.Params.Run);
            
            _currentState = AnimatorData.Params.Run;
        }
    }

    public void ChangeIdleAnimation()
    {
        if (_currentState != AnimatorData.Params.Idle && _canChangeState)
        {
            _animator.Play(AnimatorData.Params.Idle);
        
            _currentState = AnimatorData.Params.Idle;
        }
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _canChangeState = true;
    }
}