using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EnemyAnimationChanger))]
[RequireComponent(typeof(Fliper))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private List<WayPoint> _movePoints;
    [SerializeField] private float _stayTime;
    [SerializeField] private float _speed;

    private WayPoint _currentWayPoint;
    private Rigidbody2D _rigidbody;
    private Fliper _fliper;
    private WaitForSeconds _wait;
    private Vector2 _currentDirection;
    private Coroutine _coroutine;
    private float _minimalDistance;
    private int _currentIndexWayPoint;
    private bool _isRun;

    public event Action<bool> Ran;

    private void Awake()
    {
        _wait = new WaitForSeconds(_stayTime);
        _fliper = GetComponent<Fliper>();
        _rigidbody = GetComponent<Rigidbody2D>();
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
           
            _fliper.Flip(_currentDirection.x);
           
            _rigidbody.velocity = new Vector2(_speed * _currentDirection.x, _rigidbody.velocity.y);
            _isRun = true;

            Ran?.Invoke(_isRun);
        }
        else if (_isRun)
        {
            _isRun = false;

            Ran?.Invoke(_isRun);

            _rigidbody.velocity = Vector2.zero;

            _coroutine = StartCoroutine(ChangeWayPoint());
        }
    }

    private IEnumerator ChangeWayPoint()
    {
        while (enabled)
        {
            yield return _wait;

            _currentIndexWayPoint++;

            if (_currentIndexWayPoint == _movePoints.Count)
                _currentIndexWayPoint = 0;

            _currentWayPoint = _movePoints[_currentIndexWayPoint];

            StopCoroutine(_coroutine);
        }
    }
}