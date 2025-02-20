using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private List<Transform> _movePoints;
    [SerializeField] private float _stayTime;
    [SerializeField] private float _speed;

    [SerializeField] private Transform _currentWayPoint;
    private Vector2 _currentDirection;
    private Rigidbody2D _rigidbody;
    private float _minimalDistance;
    private int _currentIndexWayPoint;
    private bool _isRun;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private void Awake()
    {
        _minimalDistance = 0.1f;
        _currentIndexWayPoint = 0;
        _currentWayPoint = _movePoints[_currentIndexWayPoint];
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _currentDirection = (_currentWayPoint.position - transform.position).normalized;
    }

    private void Update()
    {
        TryRun();
    }

    private void TryRun()
    {
        if (Vector2.Distance(transform.position, _currentWayPoint.position) > _minimalDistance)
        {
            _currentDirection = (_currentWayPoint.position - transform.position).normalized;
            _rigidbody.velocity = new Vector2(_speed * _currentDirection.x, _rigidbody.velocity.y);
            _isRun = true;

            _animator.SetBool("IsRun", _isRun);
        }
        else if (_isRun)
        {
            _isRun = false;

            _animator.SetBool("IsRun", _isRun);

            _rigidbody.velocity = Vector2.zero;

            Invoke("ChangeWayPoint", _stayTime);
        }

        if (_rigidbody.velocity.x < 0)
            _spriteRenderer.flipX = true;
        if (_rigidbody.velocity.x > 0)
            _spriteRenderer.flipX = false;
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