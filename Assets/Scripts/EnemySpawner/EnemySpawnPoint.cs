using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] private List<WayPoint> _wayPoints;

    private Enemy _enemy;

    public Enemy CurrentEnemy => _enemy;

    public void SetEnemy(Enemy enemy)
    {
        _enemy = enemy;
        _enemy.transform.position = transform.position;

        _enemy.SetRoute(_wayPoints);
    }
}