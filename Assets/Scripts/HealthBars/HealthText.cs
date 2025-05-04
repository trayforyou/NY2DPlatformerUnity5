using TMPro;
using UnityEngine;

public class HealthText : StorageVisualisator
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private string _splitter = "/";

    protected override void UpdateValue() =>
        _text.text = _storage.Points + _splitter + _maxValue;
}