using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField, Min(5)] private int _points;

    private int _maxValue;

    public event Action Ended;
    public event Action StateChanged;

    public int Points => _points;


    private void Awake()
    {
        _maxValue = _points;
    }

    public void TakeDamage(int damage)
    {
        int tempValue;

        damage = CheckCorrectNumber(damage);

        tempValue = _points - damage;

        if (tempValue <= 0)
            _points = 0;
        else
            _points = tempValue;

        if (_points <= 0)
            Ended?.Invoke();

        StateChanged?.Invoke();
    }

    public void Heal(int healPoints)
    {
        int tempValue;

        healPoints = CheckCorrectNumber(healPoints);

        tempValue = _points + healPoints;

        if (tempValue > _maxValue)
            _points = _maxValue;
        else
            _points = tempValue;

        StateChanged?.Invoke();
    }

    private int CheckCorrectNumber(int number)
    {
        if (number <= 0)
            number = 0;

        return number;
    }
}