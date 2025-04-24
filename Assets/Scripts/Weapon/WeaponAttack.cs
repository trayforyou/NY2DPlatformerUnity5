using System;
using UnityEngine;

[RequireComponent(typeof(WeaponAnimationChanger))]
public class WeaponAttack : MonoBehaviour
{
    [SerializeField] private int _blowForce = 1;
    [SerializeField] private LayerMask _currentTargetMask;

    private WeaponAnimationChanger _weaponAnimationChanger;
    private bool _isBlow;

    public event Action WeaponReady;

    public void Attack(bool CanBlow)
    {
        _isBlow = CanBlow;

        if (CanBlow)
            _weaponAnimationChanger.ChangeAttackAnimation();
        else
            WeaponReady?.Invoke();
    }

    public void SetNewTarget(LayerMask _newTargetMask) =>
        _currentTargetMask = _newTargetMask;

    private void Awake()
    {
        _weaponAnimationChanger = GetComponent<WeaponAnimationChanger>();
        _isBlow = false;
    }

    private void OnTriggerStay2D(Collider2D collision) =>
        CauseDamage(collision);

    private void OnTriggerEnter2D(Collider2D collision) =>
        CauseDamage(collision);

    private void CauseDamage(Collider2D targetCollider)
    {
        if (_currentTargetMask.value == (1 << targetCollider.gameObject.layer) && _isBlow)
        {
            if (targetCollider.gameObject.TryGetComponent(out Health health))
            {
                health.TakeDamage(_blowForce);

                _isBlow = false;
            }
        }
    }
}