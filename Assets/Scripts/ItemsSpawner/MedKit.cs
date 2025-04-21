using UnityEngine;

[RequireComponent(typeof(Item))]
public class MedKit : MonoBehaviour
{
    [SerializeField] int _healthPoint;

    public int GetHealPoint()
    {
        Destroy(gameObject);

        return _healthPoint;
    }
}