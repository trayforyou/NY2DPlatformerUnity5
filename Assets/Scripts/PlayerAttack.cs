using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(InputPlayer))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float _timeBetweenAttacks = 1f;
    [SerializeField] private Transform _attackPosition;
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask _enemiesLayer;
    [SerializeField] private int _blowForce = 1;

    private InputPlayer _inputPlayer;
    private WaitForSeconds _wait;
    private bool _isAttack;
    private bool _isReadyToAttack;

    public event Action Attacked;

    private void Awake()
    {
        _isReadyToAttack = true;
        _isAttack = false;
        _inputPlayer = GetComponent<InputPlayer>();
        _wait = new WaitForSeconds(_timeBetweenAttacks);

        _inputPlayer.Attacked += MoveAttack;
    }

    private void FixedUpdate()
    {
        if (_isAttack)
            Attack();
    }

    private void OnDisable()
    {
        _inputPlayer.Attacked -= MoveAttack;
    }

    private void Attack()
    {
        if (_isReadyToAttack)
        {
            Attacked?.Invoke();
            StartCoroutine(WaitNextAttack());
            Debug.Log("Ударил");

            Collider2D[] enemiesCollider = Physics2D.OverlapCircleAll(_attackPosition.position, _attackRange, _enemiesLayer);

            foreach (Collider2D enemyCollider in enemiesCollider)
                if (enemyCollider.TryGetComponent(out Health enemyHealth))
                    enemyHealth.TakeDamage(_blowForce);
        }

        _isAttack = false;

        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_attackPosition.position, _attackRange);
    }

    private void MoveAttack(bool isAttack) =>
        _isAttack = isAttack;

    private IEnumerator WaitNextAttack()
    {
        _isReadyToAttack = false;

        yield return _wait;

        Attacked?.Invoke();

        _isReadyToAttack = true;

        yield break;
    }
}