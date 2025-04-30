using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationChanger : MonoBehaviour
{
    private Animator _animator;
    private int _currentState;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ChangeFallAnimation()
    {
        if (_currentState != AnimatorData.Params.Fall)
        {
            _animator.Play(AnimatorData.Params.Fall);
            
            _currentState = AnimatorData.Params.Fall;
        }
    }

    public void ChangeJumpAnimation()
    {
        if (_currentState != AnimatorData.Params.Jump)
        {
            _animator.Play(AnimatorData.Params.Jump);
            
            _currentState = AnimatorData.Params.Jump;
        }
    }

    public void ChangeRunAnimation()
    {
        if (_currentState != AnimatorData.Params.Run)
        {
            _animator.Play(AnimatorData.Params.Run);
            
            _currentState = AnimatorData.Params.Run;
        }
    }

    public void ChangeIdleAnimation()
    {
        if (_currentState != AnimatorData.Params.Idle)
        {
            _animator.Play(AnimatorData.Params.Idle);
        
            _currentState = AnimatorData.Params.Idle;
        }
    }
}