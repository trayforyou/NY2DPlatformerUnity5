using UnityEngine;
using UnityEngine.UI;

public class HealthBar : StorageVisualisator
{
    [SerializeField] private Slider _slider;

    protected override void UpdateValue()
    {
        if (_slider.maxValue != _maxValue)
            _slider.maxValue = _maxValue;

        _slider.value = _storage.Points;
    }
}