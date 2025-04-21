using System;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    [SerializeField] private int _blowForce = 1;
    [SerializeField] private LayerMask _currentTargetMask;

    private Coroutine _coroutine;
    private bool _isBlow;

    public event Action WeaponReady;
    public event Action Attacked;

    private void Awake() =>
        _isBlow = false;

    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private void OnTriggerStay2D(Collider2D collision) =>
        TakeDamage(collision);

    private void OnTriggerEnter2D(Collider2D collision) =>
        TakeDamage(collision);

    public void SetNewTarget(LayerMask _newTargetMask) =>
    _currentTargetMask = _newTargetMask;

    private void TakeDamage(Collider2D targetCollider)
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

    public void Attack(bool CanBlow)
    {
        _isBlow = CanBlow;

        if (CanBlow)
            Attacked?.Invoke();
        else
            WeaponReady?.Invoke();
    }
}