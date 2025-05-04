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
        _vampirism.Activated += Reduce;
    }

    private void OnDisable()
    {
        _vampirism.Activated -= Reduce;

        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private void Reduce()
    {
        _coroutine = StartCoroutine(ChangeValue(_slider.minValue,_attackDuration));
    }

    private void Increase()
    {
        _coroutine = StartCoroutine(ChangeValue(_slider.maxValue, _attackRechange));
    }

    private IEnumerator ChangeValue(float targetValue, float duration)
    {
        _speed = _slider.maxValue / duration;

        while (_slider.value != targetValue)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, targetValue, _speed * Time.deltaTime);

            yield return null;
        }
        
        if (_slider.value != _slider.maxValue)
            Increase();
    }
}