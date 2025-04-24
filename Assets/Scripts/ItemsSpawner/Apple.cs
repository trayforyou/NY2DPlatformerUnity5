using UnityEngine;

public class Apple : MonoBehaviour, Item
{
    public void Pick() =>
        Destroy(gameObject);

    public void Accept(IVisitor visitor) =>
        visitor.Visit(this);
}