using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField, Min(0)] protected int _points;

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

    public int TakeDamage(int damage)
    {
        int tempValue;

        damage = CheckCorrectNumber(damage);

        tempValue = _points - damage;

        if (tempValue <= 0)
        {
            damage += tempValue;
            _points = 0;
        }
        else
            _points = tempValue;

        if (_points <= 0)
            End();

        ChangeState();

        return damage;
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

        ChangeState();
    }

    private int CheckCorrectNumber(int number)
    {
        if (number <= 0)
            number = 0;

        return number;
    }
}