using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothHealthBar : HealthVisualizator
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _changeValueSpeed;

    private float _changeStep;
    private Coroutine _coroutine;
    private WaitForSeconds wait;

    private void Awake()
    {
        _changeStep = 0.5f;
        wait = new WaitForSeconds(_changeValueSpeed);
    }

    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    protected override void VisualiseNewValue()
    {
        if (_slider.maxValue != _maxValue)
        {
            _slider.maxValue = _maxValue;
            _slider.value = _maxValue;
        }

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(StartChangeValue(_health.Points));
    }

    private IEnumerator StartChangeValue(int newValue)
    {
        while (_slider.value != newValue)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, newValue, _changeStep);

            yield return wait;
        }

        _coroutine = null;

        yield break;
    }
}