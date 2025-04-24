using UnityEngine;

public class WeaponPreview : MonoBehaviour, Item
{
    [SerializeField] Weapon _weapon;

    public Weapon Weapon => _weapon;

    public void Accept(IVisitor visitor) =>
        visitor.Visit(this);

    public Weapon TakeWeapon()
    {
        Destroy(gameObject);

        return _weapon;
    }
}