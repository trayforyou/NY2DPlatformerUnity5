using UnityEngine;

public class MedKit : MonoBehaviour, Item
{
    [SerializeField] int _healthPoint;

    public void Accept(IVisitor visitor) =>
        visitor.Visit(this);

    public int GetHealPoint()
    {
        Destroy(gameObject);

        return _healthPoint;
    }
}