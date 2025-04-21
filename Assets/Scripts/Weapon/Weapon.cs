using UnityEngine;

[RequireComponent(typeof(WeaponAnimationChanger))]
[RequireComponent(typeof(WeaponAttack))]
public class Weapon : MonoBehaviour
{
    [SerializeField] WeaponPreview _weaponPreview;

    internal void Drop()
    {
        Instantiate(_weaponPreview, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    public WeaponAttack GetWeaponAttack()
    {
        return GetComponent<WeaponAttack>();
    }
}