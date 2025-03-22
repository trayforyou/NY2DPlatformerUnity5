using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

[RequireComponent(typeof(InputPlayer))]
[RequireComponent(typeof(Animator))]
public class WeaponAttack : MonoBehaviour
{
    [SerializeField] private LayerMask _enemiesLayer;
    [SerializeField] private int _blowForce = 1;
    [SerializeField] private float _timeBetweenAttacks = 0.1f;

    private InputPlayer _inputPlayer;
    private WaitForSeconds _wait;
    private Coroutine _coroutine;
    private bool _isReadyToAttack;

    public event Action Attacked;

    private void Awake()
    {
        _isReadyToAttack = true;
        _wait = new WaitForSeconds(_timeBetweenAttacks);

        _inputPlayer = GetComponent<InputPlayer>();

        _inputPlayer.Attacked += Attack;
    }

    private void OnDisable()
    {
        _inputPlayer.Attacked -= Attack;

        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private void Attack()
    {
        if (_isReadyToAttack)
        {
            Debug.Log("PAU");
            Attacked?.Invoke();

            _coroutine = StartCoroutine(WaitNextAttack());
        }
    }

    private IEnumerator WaitNextAttack()
    {
        _isReadyToAttack = false;

        yield return _wait;

        _isReadyToAttack = true;

        yield break;
    }


}
