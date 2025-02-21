using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EnemyAnimationChanger))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private List<WayPoint> _movePoints;
    [SerializeField] private float _stayTime;
    [SerializeField] private float _speed;

    [SerializeField]private WayPoint _currentWayPoint;
    private Rigidbody2D _rigidbody;
    private Vector2 _currentDirection;
    private Vector3 _seeOnLeft;
    private Vector3 _seeOnRight;
    private float _minimalDistance;
    private int _currentIndexWayPoint;
    private bool _isRun;

    public event Action<bool> Ran;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _seeOnLeft = new Vector3(0, 180, 0);
        _seeOnRight = Vector3.zero;
        _minimalDistance = 0.1f;
        _currentIndexWayPoint = 0;
        _currentWayPoint = _movePoints[_currentIndexWayPoint];
        _currentDirection = (_currentWayPoint.transform.position - transform.position).normalized;
    }

    private void Update()
    {
        Run();
    }

    private void Run()
    {
        if (transform.position.IsEnoughClose(_currentWayPoint.transform.position, _minimalDistance))
        {
            _currentDirection = (_currentWayPoint.transform.position - transform.position).normalized;
            _rigidbody.velocity = new Vector2(_speed * _currentDirection.x, _rigidbody.velocity.y);
            _isRun = true;

            Ran?.Invoke(_isRun);
        }
        else if (_isRun)
        {
            _isRun = false;

            Ran?.Invoke(_isRun);

            _rigidbody.velocity = Vector2.zero;

            Invoke("ChangeWayPoint", _stayTime);
        }

        if (_rigidbody.velocity.x > 0)
            transform.rotation = Quaternion.Euler(_seeOnRight);

        if (_rigidbody.velocity.x < 0)
            transform.rotation = Quaternion.Euler(_seeOnLeft);
    }

    private void ChangeWayPoint()
    {
        _currentIndexWayPoint++;

        if (_currentIndexWayPoint == _movePoints.Count)
        {
            _currentIndexWayPoint = 0;
        }

        _currentWayPoint = _movePoints[_currentIndexWayPoint];
    }
}