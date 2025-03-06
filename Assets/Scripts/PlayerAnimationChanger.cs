using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerAttack))]
public class PlayerAnimationChanger : MonoBehaviour
{
    [SerializeField] private GroundDetector _groundDetector;

    private PlayerMover _playerMover;
    private PlayerAttack _playerAttack;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerMover = GetComponent<PlayerMover>();
        _playerAttack = GetComponent<PlayerAttack>();

        _playerMover.Ran += ChangeRunAnimation;
        _playerMover.Stopped += ChangeIdleAnimation;
        _playerAttack.Attacked += ChangeAttackAnimation;
        _groundDetector.Fell += ChangeFallAnimation;
        _groundDetector.Jumped += ChangeJumpAnimation;
    }

    private void OnDisable()
    {

    }

    private void ChangeFallAnimation() =>
        _animator.SetTrigger(AnimatorData.Params.Fall);

    private void ChangeJumpAnimation() =>
        _animator.SetTrigger(AnimatorData.Params.Jump);  

    private void ChangeRunAnimation() =>
        _animator.SetTrigger(AnimatorData.Params.Run);

    private void ChangeAttackAnimation() =>
        _animator.SetTrigger(AnimatorData.Params.Attack);

    private void ChangeIdleAnimation() =>
        _animator.SetTrigger(AnimatorData.Params.Idle);
}
