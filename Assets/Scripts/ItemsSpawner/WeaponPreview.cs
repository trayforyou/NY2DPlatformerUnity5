using UnityEngine;

public class WeaponPreview : MonoBehaviour
{
    [SerializeField] Weapon _weapon;

    public Weapon Weapon => _weapon;

    internal Weapon TakeWeapon()
    {
        Destroy(gameObject);

        return _weapon;
    }
}