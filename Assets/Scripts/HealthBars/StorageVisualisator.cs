using UnityEngine;

public class StorageVisualisator : MonoBehaviour
{
    [SerializeField] protected Health _health;

    protected int _maxValue;

    private void Start()
    {
        _maxValue = _health.Points;
        UpdateValue();
    }

    private void OnEnable() =>
        _health.StateChanged += UpdateValue;

    private void OnDisable() =>
        _health.StateChanged -= UpdateValue;

    protected virtual void UpdateValue(){}
}
