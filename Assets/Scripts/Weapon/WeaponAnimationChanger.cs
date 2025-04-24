using UnityEngine;

[RequireComponent(typeof(WeaponAttack))]
[RequireComponent(typeof(Animator))]
public class WeaponAnimationChanger : MonoBehaviour
{
    private Animator _animator;

    public void ChangeAttackAnimation() =>
        _animator.Play(AnimatorData.Params.Attack);

    private void Awake() =>
        _animator = GetComponent<Animator>();
}