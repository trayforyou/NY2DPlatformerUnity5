using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(EnemyAnimationChanger))]
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    private EnemyMover _enemyMover;

    private void Awake() =>
        _enemyMover = GetComponent<EnemyMover>();

    public void SetRoute(List<WayPoint> wayPoints) =>
        _enemyMover.SetRoute(wayPoints);
}