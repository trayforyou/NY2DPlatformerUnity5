using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothBar : StorageVisualisator
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _changeValueSpeed;

    private float _changeStep;
    private Coroutine _coroutine;
    private WaitForSeconds _wait;

    private void Awake()
    {
        _changeStep = 0.5f;
        _wait = new WaitForSeconds(_changeValueSpeed);
    }

    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    protected override void UpdateValue()
    {
        if (_slider.maxValue != _maxValue)
        {
            _slider.maxValue = _maxValue;
            _slider.value = _maxValue;
        }

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ChangeValue(_storage.Points));
    }

    private IEnumerator ChangeValue(int newValue)
    {
        while (_slider.value != newValue)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, newValue, _changeStep);

            yield return _wait;
        }

        _coroutine = null;
    }
}