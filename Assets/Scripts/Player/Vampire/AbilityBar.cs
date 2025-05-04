using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private VampireAttack _vampirism;

    private Coroutine _coroutine;
    private float _attackDuration;
    private float _attackRechange;
    private float _speed;

    private void Awake()
    {
        _attackDuration = _vampirism.Duration;
        _attackRechange = _vampirism.Rechange;
    }

    private void OnEnable()
    {
        _vampirism.Activated += StartChangeValue;
    }

    private void OnDisable()
    {
        _vampirism.Activated -= StartChangeValue;

        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private void StartChangeValue()
    {
        _coroutine = StartCoroutine(ChangeValue());
    }

    private IEnumerator ChangeValue()
    {
        _speed = _slider.maxValue / _attackDuration;

        while (_slider.value != _slider.minValue)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _slider.minValue, _speed * Time.deltaTime);

            yield return null;
        }

        _speed = _slider.maxValue / _attackRechange;

        while (_slider.value != _slider.maxValue)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _slider.maxValue, _speed * Time.deltaTime);

            yield return null;
        }

        _coroutine = null;
    }
}