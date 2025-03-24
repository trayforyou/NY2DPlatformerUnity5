using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WeaponAttack))]
[RequireComponent(typeof(Animator))]
public class WeaponAnimationChanger : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private WeaponAttack _weaponAttack;
    private int _currentState;
    private Coroutine _coroutine;

    private void Awake()
    {
        _weaponAttack = GetComponent<WeaponAttack>();

        _weaponAttack.Attacked += ChangeAttackAnimation;

        _currentState = AnimatorData.Params.Idle;
    }

    private void ChangeAttackAnimation()
    {
        if (_currentState == AnimatorData.Params.Attack)
            _animator.Play(AnimatorData.Params.Idle);

        _animator.Play(AnimatorData.Params.Attack);

        _currentState = AnimatorData.Params.Attack;
    }

    private void ChangeIdleAnimation()
    {
        _animator.Play(AnimatorData.Params.Idle);

        _currentState = AnimatorData.Params.Idle;
    }
}
