using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent (typeof(EnemyMover))]
[RequireComponent (typeof(Rigidbody2D))]
public class EnemyAnimationChanger : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private EnemyMover _enemyMover;

    private void Awake()
    {
        _enemyMover = GetComponent<EnemyMover>();
        _animator = GetComponent<Animator>();

        _enemyMover.Ran += Run;
    }

    private void OnDisable() =>
        _enemyMover.Ran -= Run;

    private void Run(bool isRunning) =>
        _animator.SetBool(AnimatorData.Params.IsRunning, isRunning);
}