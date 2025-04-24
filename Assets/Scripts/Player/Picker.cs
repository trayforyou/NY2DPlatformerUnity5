using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Health))]
public class Picker : MonoBehaviour
{
    [SerializeField] private int _applesCount;
    [SerializeField] private PlayerHand _playerHand;
    
    private Health _health;
    private PlayerInput _inputPlayer;
    private WeaponPreview _weaponPreview;
    private Weapon _newWeapon;

    private void Awake()
    {
        _inputPlayer = GetComponent<PlayerInput>();
        _health = GetComponent<Health>();

        _applesCount = 0;
    }

    private void OnEnable() =>
        _inputPlayer.Picked += PickWeapon;

    private void OnDisable() =>
        _inputPlayer.Picked -= PickWeapon;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Apple apple))
        {
            _applesCount++;

            apple.Pick();
        }

        if (collision.TryGetComponent(out MedKit medKit))
            _health.Heal(medKit.GetHealPoint());

        if (collision.TryGetComponent(out WeaponPreview weaponPreview))
            _weaponPreview = weaponPreview;
        else
            _weaponPreview = null;
    }

    private void PickWeapon()
    {
        if (_weaponPreview != null)
        {
            _newWeapon = _weaponPreview.TakeWeapon();

            _playerHand.SetWeapon(_newWeapon);
        }
    }
}