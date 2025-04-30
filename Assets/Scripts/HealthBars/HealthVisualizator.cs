using UnityEngine;

public class HealthVisualizator : MonoBehaviour
{
    [SerializeField] protected Health _health;
    
    protected int _maxValue;

    private void Start()
    {
        _maxValue = _health.Points;
        VisualiseNewValue();
    }

    private void OnEnable() =>
        _health.StateChanged += VisualiseNewValue;

    private void OnDisable() =>
        _health.StateChanged -= VisualiseNewValue;

    protected virtual void VisualiseNewValue(){}
}
