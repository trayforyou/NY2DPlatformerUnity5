using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(EnemyNearbyFinder))]
public class Vampirism : MonoBehaviour
{
    [SerializeField] private Health _playerHealth;
    [SerializeField] private float _suckDelay;
    [SerializeField] private int _damagePoints;

    private EnemyNearbyFinder _enemyNearbyFinder;
    private Health _enemyHealth;
    private WaitForSeconds _waitSuckDelay;

    private void Awake()
    {
        _enemyNearbyFinder = GetComponent<EnemyNearbyFinder>();
        _waitSuckDelay = new WaitForSeconds(_suckDelay);
        _damagePoints = 1;
    }

    private void OnEnable() => 
        StartCoroutine(Suck());

    private IEnumerator Suck()
    {
        while (enabled)
        {
            int currentDamage = _damagePoints;
            _enemyHealth = _enemyNearbyFinder.GetHealth();

            if (_enemyHealth != null)
            {
                currentDamage = _enemyHealth.TakeDamage(currentDamage);
                _playerHealth.Heal(_damagePoints);
            }

            yield return _waitSuckDelay;
        }
    }
}