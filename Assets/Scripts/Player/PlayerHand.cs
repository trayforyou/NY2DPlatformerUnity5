using System;
using UnityEngine;

public class PlayerHand : Hand
{
    [SerializeField] private Weapon _currentWeapon;

    public event Action<Weapon> ChangedWeapon;

    public void SetWeapon(Weapon weapon)
    {
        if (_currentWeapon != null)
            _currentWeapon.Drop();

        _currentWeapon = Instantiate(weapon, transform);

        ChangedWeapon?.Invoke(_currentWeapon);
    }
}