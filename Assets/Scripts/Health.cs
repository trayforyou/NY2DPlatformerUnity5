using System;
using UnityEngine;

[RequireComponent(typeof(Destroyer))]
public class Health : MonoBehaviour
{
    [SerializeField, Min(1)] private int _healthPoint;

    public event Action HealthEnded;

    public void TakeDamage(int damage)
    {
        _healthPoint -= damage;

        if (_healthPoint <= 0)
            HealthEnded?.Invoke();
    }

    public void Heal(int healPoints) =>
        _healthPoint += healPoints;
}