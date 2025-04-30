using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField, Min(5)] private int _points;

    private int _maxHealth;

    public event Action HealthEnded;
    public event Action StateChanged;

    public int Points => _points;


    private void Awake()
    {
        _maxHealth = _points;
    }

    public void TakeDamage(int damage)
    {
        int tempHealth;

        damage = CheckCorrectNumber(damage);

        tempHealth = _points - damage;

        if (tempHealth <= 0)
            _points = 0;
        else
            _points = tempHealth;

        if (_points <= 0)
            HealthEnded?.Invoke();

        StateChanged?.Invoke();
    }

    public void Heal(int healPoints)
    {
        int tempHealth;

        healPoints = CheckCorrectNumber(healPoints);

        tempHealth = _points + healPoints;

        if (tempHealth > _maxHealth)
            _points = _maxHealth;
        else
            _points = tempHealth;

        StateChanged?.Invoke();
    }

    private int CheckCorrectNumber(int number)
    {
        if (number <= 0)
            number = 0;

        return number;
    }
}