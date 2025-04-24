using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EnemyAnimationChanger))]
[RequireComponent(typeof(Fliper))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private List<WayPoint> _movePoints;
    [SerializeField] private EnemyAttack _enemyAttack;
    [SerializeField] private float _stayTime;
    [SerializeField] private float _speed;
    [SerializeField] private float _rageSpeed;

    private WayPoint _currentWayPoint;
    private EnemyAnimationChanger _enemyAnimationChanger;
    private Rigidbody2D _rigidbody;
    private Fliper _fliper;
    private WaitForSeconds _waitChangeWaiPoint;
    private Vector2 _currentDirection;
    private Coroutine _coroutine;
    private float _minimalDistance;
    private int _currentIndexWayPoint;
    private bool _isRun;
    private bool _isAttack;

    private void Awake()
    {
        _waitChangeWaiPoint = new WaitForSeconds(_stayTime);
        _fliper = GetComponent<Fliper>();
        _enemyAnimationChanger = GetComponent<EnemyAnimationChanger>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _minimalDistance = 0.5f;
        _isAttack = false;

        _enemyAttack.RageStarted += StartRageRun;

        if (_movePoints.Count > 1)
            SetBeginRoute();
    }

    private void OnEnable() =>
        _currentIndexWayPoint = 0;

    private void OnDisable() =>
        _enemyAttack.RageStarted -= StartRageRun;

    private void FixedUpdate()
    {
        if (_movePoints.Count > 1 && !_isAttack)
            Run();
        else if (_isAttack)
            RunFoward();
    }

    private void SetBeginRoute()
    {
        _currentWayPoint = _movePoints[_currentIndexWayPoint];
        _currentDirection = (_currentWayPoint.transform.position - transform.position).normalized;
    }

    private void Run()
    {
        if (transform.position.IsEnoughClose(_currentWayPoint.transform.position, _minimalDistance))
        {
            _currentDirection = (_currentWayPoint.transform.position - transform.position).normalized;

            _fliper.Flip(_currentDirection.x);

            _rigidbody.velocity = new Vector2(_speed * _currentDirection.x, _rigidbody.velocity.y);
            _isRun = true;

            _enemyAnimationChanger.ChangeRunAnimation();
        }
        else if (_isRun)
        {
            _isRun = false;

            _enemyAnimationChanger.ChangeIdleAnimation();

            _rigidbody.velocity = Vector2.zero;

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(ChangeWayPoint());
        }
    }

    private void RunFoward() =>
        _rigidbody.velocity = new Vector2(_rageSpeed * _currentDirection.x, _rigidbody.velocity.y);   

    private void StartRageRun(bool isAttack) =>
        _isAttack = isAttack;

    private IEnumerator ChangeWayPoint()
    {
        while (enabled)
        {
            yield return _waitChangeWaiPoint;

            _currentIndexWayPoint++;

            if (_currentIndexWayPoint == _movePoints.Count)
                _currentIndexWayPoint = 0;

            _currentWayPoint = _movePoints[_currentIndexWayPoint];

            StopCoroutine(_coroutine);
        }
    }

    internal void SetRoute(List<WayPoint> wayPoints)
    {
        _movePoints = wayPoints;

        SetBeginRoute();
    }
}