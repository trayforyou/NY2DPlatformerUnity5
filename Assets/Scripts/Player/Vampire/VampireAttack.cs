using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class VampireAttack : MonoBehaviour
{
    [SerializeField] private float _rechange;
    [SerializeField] private float _duration;
    [SerializeField] private Vampirism _vampirism;

    private WaitForSeconds _waitAttack;
    private WaitForSeconds _waitRechange;
    private bool _readyForActivate;
    private PlayerInput _playerInput;
    private Coroutine _coroutine;

    public float Rechange => _rechange;
    public float Duration => _duration;

    public event Action Activated;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _waitRechange = new WaitForSeconds(_rechange);
        _waitAttack = new WaitForSeconds(_duration);
        _readyForActivate = true;

        _vampirism.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _playerInput.ActivatedVamirism += Activate;
    }

    private void OnDisable()
    {
        _playerInput.ActivatedVamirism += Activate;

        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private void Activate()
    {
        if (_readyForActivate)
            _coroutine = StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        Activated?.Invoke();

        _vampirism.gameObject.SetActive(true);

        _readyForActivate = false;

        yield return _waitAttack;

        _vampirism.gameObject.SetActive(false);

        yield return _waitRechange;

        _readyForActivate = true;

        _coroutine = null;
    }
}