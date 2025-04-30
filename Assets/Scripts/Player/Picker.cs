using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Health))]
public class Picker : MonoBehaviour, IVisitor
{
    [SerializeField] private int _applesCount;
    [SerializeField] private PlayerHand _playerHand;
    
    private Health _health;
    private PlayerInput _inputPlayer;
    private WeaponPreview _weaponPreview;
    private Weapon _newWeapon;

    public void Visit(Apple apple)
    {
        _applesCount++;

        apple.Pick();
    }

    public void Visit(MedKit medKit) =>
        _health.Heal(medKit.GetHealPoint());

    public void Visit(WeaponPreview weaponPreview) =>
            _weaponPreview = weaponPreview;

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
        if (collision.TryGetComponent(out Item item))
            item.Accept(this);
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