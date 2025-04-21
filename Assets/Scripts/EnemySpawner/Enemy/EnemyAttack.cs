using System;
using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private PlayerChecker _playerChecker;
    [SerializeField] private WeaponAttack _weaponAttack;
    [SerializeField] private LayerMask _playerLayerMask;
    [SerializeField] private float _attackDelay;
    [SerializeField, Min(1)] private float _rageTime;

    private WaitForSeconds _waitRageTime;
    private WaitForSeconds _waitAttackDelay;
    private Coroutine _coroutineRane;
    private Coroutine _coroutineAttack;
    private bool _isAttacking;

    public event Action<bool> RageStarted;
    public event Action Attacked;

    private void Awake()
    {
        _isAttacking = false;
        _waitRageTime = new WaitForSeconds(_rageTime);
        _waitAttackDelay = new WaitForSeconds(_attackDelay);

        _weaponAttack.SetNewTarget(_playerLayerMask);

        _playerChecker.SawPlayer += StartAttack;
    }

    private void OnDisable()
    {
        if (_coroutineRane != null)
            StopCoroutine(_coroutineRane);   
        
        if (_coroutineAttack != null)
            StopCoroutine(_coroutineAttack);

        _playerChecker.SawPlayer -= StartAttack;
    }

    private void StartAttack()
    {
        if (!_isAttacking)
            _coroutineRane = StartCoroutine(RageAttack());
    }

    private IEnumerator RageAttack()
    {
        _isAttacking = true;

        RageStarted?.Invoke(true);

        _coroutineAttack = StartCoroutine(Attack());

        yield return _waitRageTime;

        _isAttacking = false;

        RageStarted?.Invoke(_isAttacking);

        yield break;
    }

    private IEnumerator Attack()
    {
        while (_isAttacking)
        {
            _weaponAttack.Attack(_isAttacking);

            yield return _waitAttackDelay;
        }

        _weaponAttack.Attack(_isAttacking);

        yield break;
    }
}