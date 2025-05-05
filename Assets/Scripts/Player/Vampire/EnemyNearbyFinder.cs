using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyNearbyFinder : MonoBehaviour
{
    private List<Health> _healthEnemies = new List<Health>();
    private Health _health;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Health health))
        {
            if (_healthEnemies.Contains(health) == false)
            {
                _healthEnemies.Add(health);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Health health))
            _healthEnemies.Remove(health);
    }

    private void OnDisable() =>
        _health = null;

    public Health GetHealth()
    {
        float sqrNewHealthDistance;
        float sqrOldHealthDistance;

        if (_healthEnemies.Any())
        {
            for (int i = 0; i < _healthEnemies.Count; i++)
            {
                if (_health == null)
                    _health = _healthEnemies[i];

                sqrNewHealthDistance = transform.position.SqrDistance(_healthEnemies[i].transform.position);
                sqrOldHealthDistance = transform.position.SqrDistance(_health.transform.position);

                if (sqrNewHealthDistance < sqrOldHealthDistance)
                {
                    _health = _healthEnemies[i];
                }
            }
        }
        else
        {
            _health = null;
        }

        return _health;
    }
}