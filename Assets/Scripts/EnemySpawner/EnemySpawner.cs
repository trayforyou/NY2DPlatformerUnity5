using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private List<EnemySpawnPoint> _spawnPoints;

    private void Awake() =>
        SpawnEnemies();

    private void SpawnEnemies()
    {
        Enemy tempEnemy;

        foreach(EnemySpawnPoint spawnPoint in _spawnPoints)
        {
            tempEnemy = Instantiate(_enemy);

            spawnPoint.SetEnemy(tempEnemy);
        }
    }
}