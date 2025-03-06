using Unity.Burst.CompilerServices;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField, Min(1)] private int _healthPoint;

    public void TakeDamage(int damage)
    {
        _healthPoint -= damage;

        if(_healthPoint <= 0)
            Destroy(gameObject);
    }
}
