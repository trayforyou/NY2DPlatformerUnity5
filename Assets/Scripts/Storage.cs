using System;
using UnityEngine;

public class Storage : MonoBehaviour
{
    [SerializeField, Min(5)] protected int _points;

    protected int _maxValue;

    public event Action Ended;
    public event Action StateChanged;

    public int Points => _points;

    protected void Awake()
    {
        _maxValue = _points;
    }

    protected void ChangeState() =>
        StateChanged?.Invoke();
        
    protected void End() =>
        Ended?.Invoke();
}