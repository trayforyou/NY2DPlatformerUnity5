using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAnimationChanger : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private EnemyMover _enemyMover;

    private int _currentState;

    private void Awake()
    {
        _enemyMover = GetComponent<EnemyMover>();
        _animator = GetComponent<Animator>();

        _enemyMover.Ran += ChangeRunAnimation;
        _enemyMover.Stopped += ChangeIdleAnimation;
    }

    private void OnDisable()
    {
        _enemyMover.Ran -= ChangeRunAnimation;
        _enemyMover.Stopped -= ChangeIdleAnimation;
    }

    private void ChangeRunAnimation()
    {
        if (_currentState != AnimatorData.Params.Run)
        {
            _animator.Play(AnimatorData.Params.Run);

            _currentState = AnimatorData.Params.Run;
        }
    }

    private void ChangeIdleAnimation()
    {
        if (_currentState != AnimatorData.Params.Idle)
        {
            _animator.Play(AnimatorData.Params.Idle);

            _currentState = AnimatorData.Params.Idle;
        }
    }
}