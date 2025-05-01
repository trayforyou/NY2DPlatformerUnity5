using TMPro;
using UnityEngine;

public class HealthText : HealthVisualizator
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private string _splitter = "/";

    protected override void UpdateValue() =>
        _text.text = _health.Points + _splitter + _maxValue;
}