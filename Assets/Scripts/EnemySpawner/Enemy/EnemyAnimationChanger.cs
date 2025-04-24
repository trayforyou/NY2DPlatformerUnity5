using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimationChanger : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private int _currentState;

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

    private void Awake() =>
        _animator = GetComponent<Animator>();
}