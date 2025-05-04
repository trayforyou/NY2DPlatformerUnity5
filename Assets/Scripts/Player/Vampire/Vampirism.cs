using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Vampirism : MonoBehaviour
{
    [SerializeField] private List<Health> _healthEnemies = new List<Health>();
    [SerializeField] private Health _playerHealth;
    [SerializeField] private float _suckDelay;

    private Health _enemyHealth;
    private WaitForSeconds _waitSuckDelay;
    private bool _isEnabled;
    private Coroutine _coroutine;
    private int _damagePoints;

    private void Awake()
    {
        _waitSuckDelay = new WaitForSeconds(_suckDelay);
        _damagePoints = 1;
        _isEnabled = true;
    }

    private void OnEnable()
    {
        _coroutine = StartCoroutine(Suck());
    }

    private void OnDisable()
    {
        _enemyHealth = null;

        StopCoroutine(_coroutine);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        AddEnemy(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        RemoveEnemy(collision);
    }

    private void AddEnemy(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Health health))
        {
            if (_healthEnemies.Contains(health) == false)
            {
                _healthEnemies.Add(health);
            }
        }
    }

    private void RemoveEnemy(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Health health))
            _healthEnemies.Remove(health);
    }

    private void EnemyNearbyCheck()
    {
        float sqrNewHealthDistance;
        float sqrOldHealthDistance;

        if (_healthEnemies.Any())
        {
            for (int i = 0; i < _healthEnemies.Count; i++)
            {
                if (_enemyHealth == null)
                    _enemyHealth = _healthEnemies[i];

                sqrNewHealthDistance = transform.position.SqrDistance(_healthEnemies[i].transform.position);
                sqrOldHealthDistance = transform.position.SqrDistance(_enemyHealth.transform.position);

                if (sqrNewHealthDistance < sqrOldHealthDistance)
                {
                    _enemyHealth = _healthEnemies[i];
                }
            }
        }
        else
        {
            _enemyHealth = null;
        }
    }

    private IEnumerator Suck()
    {
        while (_isEnabled)
        {
            EnemyNearbyCheck();

            if (_enemyHealth != null)
            {
                _enemyHealth.TakeDamage(_damagePoints);
                _playerHealth.Heal(_damagePoints);
            }

            yield return _waitSuckDelay;
        }
    }
}