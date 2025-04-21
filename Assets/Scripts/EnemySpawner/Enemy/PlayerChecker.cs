using System;
using UnityEngine;

public class PlayerChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private Fliper _enemyFliper;
    [SerializeField] private float _lookDistance;

    private RaycastHit2D _hitVision;
    private int _visibleLayers;
    private float _directionLook;
    private float _directionRight;
    private float _directionLeft;

    public event Action SawPlayer;

    private void Awake()
    {
        _directionRight = 1f;
        _directionLeft = -1f;
        _directionLook = _directionRight;

        _enemyFliper.LookedAtRight += ChangeDirection;

        _visibleLayers = _groundLayer.value | _playerLayer;
    }

    private void FixedUpdate()
    {
        _hitVision = Physics2D.Raycast(transform.position, Vector2.right * _directionLook, _lookDistance, _visibleLayers);

        if (_hitVision.collider != null)
        {
            if (_hitVision.collider.TryGetComponent<Player>(out _))
                SawPlayer?.Invoke();
        }
    }

    private void OnDisable() =>
        _enemyFliper.LookedAtRight -= ChangeDirection;

    private void ChangeDirection(bool isLookAtRight)
    {
        if (isLookAtRight)
            _directionLook = _directionRight;
        else
            _directionLook = _directionLeft;
    }
}