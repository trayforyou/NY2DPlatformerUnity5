using UnityEngine;

[RequireComponent(typeof(WeaponAttack))]
[RequireComponent(typeof(Animator))]
public class WeaponAnimationChanger : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private WeaponAttack _weaponAttack;

    private void Awake()
    {
        _weaponAttack = GetComponent<WeaponAttack>();

        _weaponAttack.Attacked += ChangeAttackAnimation;
    }

    private void OnDisable() =>
        _weaponAttack.Attacked -= ChangeAttackAnimation;


    private void ChangeAttackAnimation() =>
        _animator.Play(AnimatorData.Params.Attack);
}