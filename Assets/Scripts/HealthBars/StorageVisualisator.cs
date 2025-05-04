using UnityEngine;

public class StorageVisualisator : MonoBehaviour
{
    [SerializeField] protected Storage _storage;
    
    protected int _maxValue;

    private void Start()
    {
        _maxValue = _storage.Points;
        UpdateValue();
    }

    private void OnEnable() =>
        _storage.StateChanged += UpdateValue;

    private void OnDisable() =>
        _storage.StateChanged -= UpdateValue;

    protected virtual void UpdateValue(){}
}
