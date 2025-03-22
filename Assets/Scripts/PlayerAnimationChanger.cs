using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMover))]
public class PlayerAnimationChanger : MonoBehaviour
{
    [SerializeField] private GroundDetector _groundDetector;

    private PlayerMover _playerMover;
    private Animator _animator;
    private Coroutine _coroutine;
    private WaitForSeconds _waitForAttack;
    private int _currentState;
    private bool _canChangeState;
    private float _attackDelay;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerMover = GetComponent<PlayerMover>();

        _playerMover.Ran += ChangeRunAnimation;
        _playerMover.Stopped += ChangeIdleAnimation;
        _groundDetector.Fell += ChangeFallAnimation;
        _groundDetector.Jumped += ChangeJumpAnimation;

        _canChangeState = true;
    }

    private void OnDisable()
    {

    }

    private void ChangeFallAnimation()
    {
        if (_currentState != AnimatorData.Params.Fall && _canChangeState)
        {
            _animator.Play(AnimatorData.Params.Fall);
            
            _currentState = AnimatorData.Params.Fall;
        }
    }

    private void ChangeJumpAnimation()
    {
        if (_currentState != AnimatorData.Params.Jump && _canChangeState)
        {
            _animator.Play(AnimatorData.Params.Jump);
            
            _currentState = AnimatorData.Params.Jump;
        }
    }

    private void ChangeRunAnimation()
    {
        if (_currentState != AnimatorData.Params.Run && _canChangeState)
        {
            _animator.Play(AnimatorData.Params.Run);
            
            _currentState = AnimatorData.Params.Run;
        }
    }

    private void ChangeIdleAnimation()
    {
        if (_currentState != AnimatorData.Params.Idle && _canChangeState)
        {
            _animator.Play(AnimatorData.Params.Idle);
        
            _currentState = AnimatorData.Params.Idle;
        }
    }
}