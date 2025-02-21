using UnityEngine;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMover))]
public class PlayerAnimationChanger : MonoBehaviour
{
    private PlayerMover _playerMover;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerMover = GetComponent<PlayerMover>();

        _playerMover.Ran += ChangeRunAnimation;
        _playerMover.Jumped += ChangeJumpAnimation;
        _playerMover.Fell += ChangeFallAnimation;
    }

    private void OnDisable()
    {
        _playerMover.Ran -= ChangeRunAnimation;
        _playerMover.Jumped -= ChangeJumpAnimation;
        _playerMover.Fell -= ChangeFallAnimation;
    }

    private void ChangeFallAnimation(bool isFall) =>
        _animator.SetBool(AnimatorData.Params.IsFall, isFall);

    private void ChangeJumpAnimation(bool isJump) =>
        _animator.SetBool(AnimatorData.Params.IsJump, isJump);  

    private void ChangeRunAnimation(bool isRun) =>
        _animator.SetBool(AnimatorData.Params.IsRunning, isRun);
}
